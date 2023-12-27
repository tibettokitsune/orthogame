using UnityEngine;

namespace Infrastructure
{
    public class PCPlayerInput : IPlayerInput
    {
        public float Horizontal() => Input.GetAxis("Horizontal");

        public float Vertical() => Input.GetAxis("Vertical");
        public float Height() => Input.GetAxis("Height");

        public bool RPressed() => Input.GetKeyDown(KeyCode.R);

        public bool EPressed() => Input.GetKeyDown(KeyCode.E);

        public bool JumpPressed() => Input.GetKeyDown(KeyCode.Space);
        public bool AttackPressed() => Input.GetMouseButton(0);
    }
}