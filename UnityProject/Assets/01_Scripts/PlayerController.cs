using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerCondition _status;

    // Movement related
    [SerializeField] private Rigidbody _rb;
    private Vector3 _curMovement = Vector3.zero;
    private Vector3 _curMovementInput = Vector3.zero;
    public float deceleration = 1f;
    public float acceleration = 1f;
    private bool isDecel = false;
    public LayerMask groundLayerMask;

    // Look related
    [SerializeField] private Transform look;
    [HideInInspector] public bool camLock = false;
    public float minXLook;
    public float maxXLook;
    private float lookCurXRot;
    private float lookCurYRot;
    public float lookSensitivity;

    private Vector2 mouseDelta;

    public void OnFixedUpdate()
    {
        if (IsGrounded()) Move();
    }

    public void OnLateUpdate()
    {
        if (!camLock) Look();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            isDecel = false;
            _curMovementInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            isDecel = true;
            _curMovementInput = Vector3.zero;
        }
    }

    private void Move()
    {
        if (isDecel)
        {
            _curMovement = Vector3.Lerp(_curMovement, Vector3.zero, deceleration * Time.fixedDeltaTime);

            if (_curMovement.magnitude < 0.05f)
            {
                _curMovement = Vector3.zero;
                isDecel = false;
            }
        }

        if (_curMovementInput != Vector3.zero)
        {
            _curMovement = Vector3.Lerp(_curMovement, _curMovementInput, acceleration * Time.fixedDeltaTime);
        }

        Vector3 dir = look.forward * _curMovement.y + look.right * _curMovement.x;
        dir *= _status.moveSpeed;
        dir.y = _rb.velocity.y;
        _rb.velocity = dir;
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    private void Look()
    {
        look.transform.position = _rb.transform.position;

        lookCurXRot += mouseDelta.y * lookSensitivity;
        lookCurYRot += mouseDelta.x * lookSensitivity;

        look.localEulerAngles = new Vector3(-lookCurXRot, lookCurYRot, 0);
    }
    private bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(_rb.transform.position + (_rb.transform.forward * 0.2f) + (_rb.transform.up * 0.01f), Vector3.down),
            new Ray(_rb.transform.position + (-_rb.transform.forward * 0.2f) + (_rb.transform.up * 0.01f), Vector3.down),
            new Ray(_rb.transform.position + (_rb.transform.right * 0.2f) + (_rb.transform.up * 0.01f), Vector3.down),
            new Ray(_rb.transform.position + (-_rb.transform.right * 0.2f) +(_rb.transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.6f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }
}
