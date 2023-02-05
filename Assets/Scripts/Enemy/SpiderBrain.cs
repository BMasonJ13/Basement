using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

namespace Ben.AI
{

    /// <summary>
    /// This is responsible for enemy behavior
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
    public class SpiderBrain : MonoBehaviour
    {

        [Header("Settings")]
        [SerializeField]
        private GameObject player;
        [SerializeField]
        private LayerMask whatIsCeiling;
        [SerializeField]
        private Transform feet;
        [SerializeField]
        private float checkDistance;
        [SerializeField]
        private float maxDistance = 100f;
        [SerializeField]
        private float speed = 5f;
        [SerializeField]
        private float rotationSpeed = 3f;
        [SerializeField]
        private float rotationEpsilon = 0.5f;
        //Components
        private new Rigidbody2D rigidbody;

        //Variables
        private bool canSeePlayer = false;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {

            if (canSeePlayer && !IsOnCeiling())
                JumpToCeiling();
            else if (canSeePlayer)
            {
                MoveOverPlayer();
            }

        }

        private void JumpToCeiling()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, maxDistance, whatIsCeiling);

            if (!hit)
                return;


            Vector2 movementVector = hit.point - (Vector2)transform.position;

            Debug.Log(movementVector);

            rigidbody.AddForce(movementVector);

            rigidbody.rotation += Vector2.Angle(transform.up, hit.normal) * Time.deltaTime * rotationSpeed;

        }

        private void MoveOverPlayer()
        {
            Vector2 movementVector = player.transform.position - transform.position;

            rigidbody.velocity = new Vector2(movementVector.x,0) * Time.deltaTime * speed;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
                canSeePlayer = true;
        }

        private bool IsOnCeiling()
        {
            RaycastHit2D ceiling;
            if (ceiling = Physics2D.Raycast(feet.position, Vector2.down, checkDistance, whatIsCeiling))
            {
                if(rigidbody.rotation != Vector2.Angle(transform.up, ceiling.normal))
                rigidbody.rotation = Vector2.Angle(transform.up, ceiling.normal);
                return true;
            }
            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawSphere(feet.position, checkDistance);

        }

    }
}