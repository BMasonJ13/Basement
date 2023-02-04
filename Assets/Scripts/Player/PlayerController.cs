using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ben.Player
{
    /// <summary>
    /// This is script that controls the players movement
    /// </summary>
    [RequireComponent(typeof(InputHandler), typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        private float speed;

        [Header("Jump Settings")]
        [SerializeField]
        private Transform feet;
        [SerializeField]
        private LayerMask whatIsGround;
        [SerializeField]
        private float checkDistance = 0.2f;
        [SerializeField]
        private float jumpHeight;
        [SerializeField]
        private float gravityMultiplier = 2f;
        [SerializeField]
        private float coyoteTime;


        [Header("Dash Settings")]
        [SerializeField]
        private float dashSpeed = 12f;
        [SerializeField]
        private float initialDashDuration = 0.4f;
        [SerializeField]
        private float initialTimeTillDash = 0.3f;

        //Components
        private InputHandler handler;
        private new Rigidbody2D rigidbody;

        //Variables
        private Vector2 movementVector = Vector2.zero;
        private bool isGrounded = false;
        private float timeSinceGrounded = 0f;
        private float dashDuration;
        private float timeTillDash;
        
        private void Start()
        {
            handler = GetComponent<InputHandler>();
            rigidbody = GetComponent<Rigidbody2D>();

            dashDuration = initialDashDuration;
            timeTillDash = initialTimeTillDash;
        }

        private void FixedUpdate()
        {
            isGrounded = CheckIsGrounded();

            if (isGrounded)
            {
                GroundLocomotion();
                timeSinceGrounded = 0f;
                timeTillDash = initialTimeTillDash;
                dashDuration = initialDashDuration;
            }
            else
            {
                AiredLocomotion();
                timeSinceGrounded += Time.deltaTime;
            }

        }

        private void GroundLocomotion()
        {
            movementVector = new Vector2(handler.HorizontalRaw * speed, handler.Jump ? jumpHeight : 0);

            rigidbody.velocity = movementVector;

        }

        private void AiredLocomotion()
        {

            if (timeSinceGrounded > 0.4f)
                movementVector = new Vector2(rigidbody.velocity.x, -jumpHeight * gravityMultiplier);

            if ((handler.DashLeft || handler.DashRight) && timeSinceGrounded > timeTillDash && dashDuration > 0f)
            {
                movementVector = new Vector2(handler.HorizontalRaw * dashSpeed, 0);
                dashDuration -= Time.deltaTime;
            }

            rigidbody.velocity = movementVector;
        }

        private bool CheckIsGrounded()
        {
            if (Physics2D.OverlapCircle(feet.position, checkDistance, whatIsGround))
                return true;
            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawSphere(feet.position, checkDistance);

        }

    }
}