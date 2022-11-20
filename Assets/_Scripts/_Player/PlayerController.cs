using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Project.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region sfields
        [SerializeField] float MAX_MOVE_SPEED = 0f;
        [SerializeField] float BRAKING_TIME = 0f;
        [SerializeField] private float jumpForce;
        [SerializeField] private float brakingForce;
        [SerializeField] private float jumpFallForce;
        [SerializeField] private float jumpTimer;
        [SerializeField] private Camera playerCam;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius;
        [SerializeField] private LayerMask walkableLayer;
        #endregion

        #region fields
        private PlayerActionMaps m_playerInputActions;
        private Vector2 m_input;
        private Rigidbody m_rb;
        private float m_currentMoveSpeed;
        private float m_curentBrakingTime;
        private float m_currentJumpTimer;
        private bool m_isMoving;
        private bool m_isGrounded;
        #endregion

        [Inject]
        public void Injection(PlayerActionMaps playerInputActions)
        {
            m_playerInputActions = playerInputActions;
        }
        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        private void OnEnable()
        {
            m_playerInputActions.PlayerMovement.Enable();
            m_playerInputActions.PlayerMovement.Jump.performed += JumpPerformed;
            m_playerInputActions.PlayerMovement.Walk.performed += WalkPerformed;
            m_playerInputActions.PlayerMovement.Walk.canceled += WalkPerformed;
        }
        private void Start()
        {
            m_rb = gameObject.GetComponent<Rigidbody>();
            m_currentMoveSpeed = MAX_MOVE_SPEED;
        }
        private void OnDisable()
        {
            m_playerInputActions.PlayerMovement.Disable();
        }
    
        private void Update()
        {
            m_isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, walkableLayer);
            if (jumpTimer >= 0)
            {
                jumpTimer -= Time.fixedDeltaTime;
            }
            var rotation = gameObject.transform.rotation;
            rotation = new Quaternion(rotation.x, playerCam.transform.rotation.y, rotation.z, rotation.w);
            gameObject.transform.rotation = rotation;
        
            m_input = m_playerInputActions.PlayerMovement.Movement.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            Movement();
            JumpLogic();
        }
    
        private void JumpPerformed(InputAction.CallbackContext context)
        {
            if (!m_isGrounded) return;

            m_rb.AddForce(0f, jumpForce, 0f, ForceMode.Impulse);
            m_currentJumpTimer = jumpTimer;
        }

        private void JumpLogic()
        {
            if (m_currentJumpTimer < 0f && !m_isGrounded && m_rb.velocity.y <= 0f)
            {
                m_rb.AddForce(0f, -jumpFallForce, 0f, ForceMode.Force);
            }
        }
    
        private void Movement()
        {
            var forward = gameObject.transform.forward;
            var right = gameObject.transform.right;
        
            if (m_input.magnitude >= 0.1f)
            {
                m_isMoving = true;
                m_curentBrakingTime = BRAKING_TIME;
                m_rb.velocity = m_currentMoveSpeed * Time.fixedDeltaTime * m_input.y * forward + m_currentMoveSpeed * Time.fixedDeltaTime * m_input.x * right + m_rb.velocity.y * Vector3.up;
            }
            else
            {
                m_isMoving = false;
            }

            if (m_curentBrakingTime >= -0.1f && !m_isMoving)
            {
                m_curentBrakingTime -= Time.fixedDeltaTime;
                var dir = m_rb.velocity.normalized;
                m_rb.AddForce(-(dir) * brakingForce, ForceMode.Force);
            }
        }
    
        private void WalkPerformed(InputAction.CallbackContext context)
        {
            // Debug.Log("WALK");
            if (context.performed)
            {
                // Debug.Log("Shift pressed");
                m_currentMoveSpeed /= 2;
            }
            else if (context.canceled)
            { 
                // Debug.Log("Shift released");
                m_currentMoveSpeed = MAX_MOVE_SPEED;
            }
        }
    }
}

    
