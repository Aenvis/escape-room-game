using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls
{
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private PlayerInputActions m_playerInputActions;
    private Vector2 m_input;
    private Rigidbody rb;

    private void Awake()
    {
        m_playerInputActions = new PlayerInputActions();
        Cursor.visible = false;
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

    private void Start()
    {
        rb = gameObject.GetComponentInChildren<Rigidbody>();
    }

    private void Update()
    {
        m_input = m_playerInputActions.PlayerMovement.Movement.ReadValue<Vector2>();
        var dir = new Vector3(m_input.x, 0f, m_input.y).normalized;
        
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(m_input.x * moveSpeed * Time.fixedDeltaTime, rb.velocity.y, m_input.y * moveSpeed * Time.fixedDeltaTime);
    }
    
    private void JumpPerformed(InputAction.CallbackContext context)
    {
        rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
    }
}
}
    
