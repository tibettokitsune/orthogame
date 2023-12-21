using UnityEngine;
using UnityHFSM;

namespace Game.Scripts.Metagameplay.Player
{
    public class SimpleMovementState : State
    {

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("OnEnter");
        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("OnExit");
        }
    }
}