using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public LayerMask groundLayerMask;
    public LayerMask chargingJumpingPadLayerMask;
    public LayerMask wallLayerMask;

    private PlayerCondition _status;
    public InteractableObjectDetector interactableObjectDetector;

    // Movement related
    public Rigidbody body;
    public float deceleration = 1f;
    public float acceleration = 1f;
    private Vector3 _curMovement = Vector3.zero;
    private Vector3 _curMovementInput = Vector3.zero;
    private bool _isDecel = false;

    // Look related
    public Transform _look;
    public float lookSensitivity;
    private float _lookCurXRot;
    private float _lookCurYRot;
    private Vector2 _mouseDelta;

    // Space related
    private bool _isSpaceDown = false;
    private float _chargingJumpPower = 0f;

    //Inventory related
    public Action inventory;

    public void OnStart()
    {
        _status = CharacterManager.Instance.Player.status;
    }

    public void OnFixedUpdate()
    {
        if (CheckGroundLayer() == groundLayerMask || CheckGroundLayer() == chargingJumpingPadLayerMask) 
            Move();

        if (_isSpaceDown)
        {
            if (CheckGroundLayer() == groundLayerMask)
            {
                Jump();
            }
            else if (CheckGroundLayer() == chargingJumpingPadLayerMask)
            {
                _chargingJumpPower += 0.1f;
            }
            else if (IsWallAttached())
            {
                WallSlide();
            }
        }
        else
        {
            if (_chargingJumpPower > 0)
            {
                SuperJump();
                _chargingJumpPower = 0;
            }
        }
    }

    public void OnLateUpdate()
    {
        Look();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            _isDecel = false;
            _curMovementInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            _isDecel = true;
            _curMovementInput = Vector3.zero;
        }
    }

    private void Move()
    {
        if (_isDecel)
        {
            _curMovement = Vector3.Lerp(_curMovement, Vector3.zero, deceleration * Time.fixedDeltaTime);

            if (_curMovement.magnitude < 0.05f)
            {
                _curMovement = Vector3.zero;
                _isDecel = false;
            }
        }

        if (_curMovementInput != Vector3.zero)
        {
            _curMovement = Vector3.Lerp(_curMovement, _curMovementInput, acceleration * Time.fixedDeltaTime);
        }

        Vector3 dir = _look.forward * _curMovement.y + _look.right * _curMovement.x;
        dir *= _status.moveSpeed;
        dir.y = body.velocity.y;
        body.velocity = dir;
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        _mouseDelta = context.ReadValue<Vector2>();
    }

    private void Look()
    {
        _look.transform.position = body.transform.position;

        _lookCurXRot += _mouseDelta.y * lookSensitivity;
        _lookCurYRot += _mouseDelta.x * lookSensitivity;

        _look.localEulerAngles = new Vector3(-_lookCurXRot, _lookCurYRot, 0);
    }

    public void OnSpaceInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _isSpaceDown = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _isSpaceDown = false;
        }
    }

    private void Jump()
    {
       body.AddForce(Vector2.up * 8f, ForceMode.Impulse);
    }

    private void SuperJump()
    {
        body.AddForce(_look.transform.forward * _chargingJumpPower, ForceMode.Impulse);
    }

    private void WallSlide()
    {
        body.velocity = new Vector3(body.velocity.x, Mathf.Max(body.velocity.y, -0.1f), body.velocity.z);
    }

    public void OnTabInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            inventory?.Invoke();
        }
    }

    private LayerMask CheckGroundLayer()
    {
        Bounds bound = body.GetComponent<Collider>().bounds;
        Vector3 origin = new(bound.center.x, bound.center.y - bound.extents.y + 0.005f, bound.center.z);

        if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, 0.01f))
        {
            return 1 << hit.transform.gameObject.layer;
        }

        return 0;
    }

    private bool IsWallAttached()
    {
        Bounds bound = body.GetComponent<Collider>().bounds;
        Vector3 origin = _look.transform.position;
        Debug.DrawRay(origin, _look.forward);
        return Physics.Raycast(origin, _look.forward, 1f, wallLayerMask);
    }
}
