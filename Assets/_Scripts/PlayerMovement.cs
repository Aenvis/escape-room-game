using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls
{
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    
    private PlayerInputActions m_playerInputActions;
    private Vector2 m_input;

    private void Awake()
    {
        m_playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        m_playerInputActions.PlayerMovement.Enable();
        m_playerInputActions.PlayerMovement.Jump.performed += JumpPerformed;
    }


    private void OnDisable()
    {
        m_playerInputActions.PlayerMovement.Disable();
    }

    private void Update()
    {
        m_input = m_playerInputActions.PlayerMovement.Movement.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(m_input.x * moveSpeed * Time.fixedDeltaTime, rb.velocity.y, m_input.y * moveSpeed * Time.fixedDeltaTime);
    }
    private void JumpPerformed(InputAction.CallbackContext conext)
    {
        rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Force);
    }
}
}
    
