using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ben.Player
{

    /// <summary>
    /// This script handles input.
    /// </summary>
    public class InputHandler : MonoBehaviour
    {
        public float HorizontalRaw { get; private set; }
        public bool Jump { get; private set; }
        public bool DashLeft { get; private set; }
        public bool DashRight { get; private set; }

        private bool lPrev = false;
        private bool rPrev = false;

        [SerializeField]
        private float tapSuccessionTime = 0.14f;
        private float realTCT;

        private void Start()
        {
            realTCT = tapSuccessionTime;
        }

        private void Update()
        {
            HorizontalRaw = Input.GetAxisRaw("Horizontal");
            Jump = Input.GetKey(KeyCode.Space);

            if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (lPrev && realTCT > 0)
                {
                    DashLeft = true;
                    lPrev = false;
                    Debug.Log("Left Dash");
                }
                lPrev = true;
                rPrev = false;
                realTCT = tapSuccessionTime;
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (rPrev && realTCT > 0)
                {
                    DashRight = true;
                    rPrev = false;
                    Debug.Log("Right Dash");
                }
                rPrev = true;
                lPrev = false;
                realTCT = tapSuccessionTime;
            }
            else if(realTCT <= 0.0)
            {
                DashLeft = false;
                DashRight = false;
            }

            realTCT -= Time.deltaTime;
        }
    }
}