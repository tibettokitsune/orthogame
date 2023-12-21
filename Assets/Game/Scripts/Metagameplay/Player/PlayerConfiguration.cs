using UnityEngine;

namespace Game.Scripts.Metagameplay.Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/Player")]
    public class PlayerConfiguration : ScriptableObject
    {
        [Range(1, 10)] public float maxVelocity;
        [Range(1, 1000)] public float acceleration;
        [Range(1, 1000)] public float forceGravity;
        [Range(1, 1000)] public float antigravityMovementCoefficient;

    }
}