using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float maxMoveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Camera playerCam;
    [SerializeField] private Transform groundCheck;
    

    private float m_currentMoveSpeed;
    private PlayerInputActions m_playerInputActions;
    private Vector2 m_input;
    private Rigidbody m_rb;

    private void Awake()
    {
        m_playerInputActions = new PlayerInputActions();
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        m_playerInputActions.PlayerMovement.Enable();
        m_playerInputActions.PlayerMovement.Jump.performed += JumpPerformed;
        m_playerInputActions.PlayerMovement.Walk.performed += Walk;
        m_playerInputActions.PlayerMovement.Walk.canceled += Walk;
    }


    private void OnDisable()
    {
        m_playerInputActions.PlayerMovement.Disable();
    }

    private void Start()
    {
        m_rb = gameObject.GetComponent<Rigidbody>();
        m_currentMoveSpeed = maxMoveSpeed;
    }
    
    private void Update()
    {
        var rotation = gameObject.transform.rotation;
        rotation = new Quaternion(rotation.x, playerCam.transform.rotation.y, rotation.z, rotation.w);
        gameObject.transform.rotation = rotation;
        
        m_input = m_playerInputActions.PlayerMovement.Movement.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        var forward = gameObject.transform.forward;
        var right = gameObject.transform.right;
        
        if (m_input.magnitude >= 0.1f)
        {
            m_rb.velocity = m_currentMoveSpeed * Time.fixedDeltaTime * m_input.y * forward + m_currentMoveSpeed * Time.fixedDeltaTime * m_input.x * right + m_rb.velocity.y * Vector3.up;
        }
    }
    
    private void JumpPerformed(InputAction.CallbackContext context)
    {
        m_rb.AddForce(0f, jumpForce, 0f, ForceMode.Impulse);
    }
    
    private void Walk(InputAction.CallbackContext context)
    {
        Debug.Log("WALK");
        if (context.performed)
        {
            Debug.Log("Shift pressed");
            m_currentMoveSpeed /= 2;
        }
        else if (context.canceled)
        {
            Debug.Log("Shift released");
            m_currentMoveSpeed = maxMoveSpeed;
        }
    }
}

    
