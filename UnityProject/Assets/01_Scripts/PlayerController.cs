using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerCondition _status;
    [SerializeField] private Rigidbody _rb;

    public float moveSpeed => _status.moveSpeed;
    private Vector3 _curMovementInput = Vector3.zero;

    public Transform cameraContainer;
    [ReadOnly, SerializeField] private Quaternion _initialRotation;
    [ReadOnly, SerializeField] private Vector3 _initialPosition;

    [HideInInspector]
    public bool camLock = false;

    private void Start()
    {
        _initialRotation = cameraContainer.transform.rotation;
        _initialPosition = cameraContainer.transform.position;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (!camLock) CameraLook();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            _curMovementInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            _curMovementInput = Vector2.zero;
        }
    }

    private void Move()
    {
        Vector3 dir = Vector3.forward * _curMovementInput.y + Vector3.right * _curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rb.velocity.y;
        _rb.velocity = dir;
    }

    private void CameraLook()
    {
        cameraContainer.transform.position = transform.position;
    }
}
