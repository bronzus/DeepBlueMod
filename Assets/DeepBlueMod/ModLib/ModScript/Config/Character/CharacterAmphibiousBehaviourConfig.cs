using UnityEngine;

namespace kz.config
{
    [CreateAssetMenu(fileName = "CharacterAmphibiousBehaviourConfig", menuName = "ScriptableObject/CharacterAmphibiousBehaviourConfig", order = 60)]
    public class CharacterAmphibiousBehaviourConfig:ScriptableObject
    {
        public float aiJumpInterval = 1f;
        public float initialHeightAboveGround = 0f;
        public float jumpHeight = 10f;
        public float maxWalkSlope = 45f;
        public float walkSpeedMul = 1f;
    }
}