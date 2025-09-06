using UnityEngine;

namespace kz.config
{
    [CreateAssetMenu(fileName = "CharacterWalkBehaviourConfig", menuName = "ScriptableObject/CharacterWalkBehaviourConfig", order = 50)]
    public class CharacterWalkBehaviourConfig:ScriptableObject
    {
        public float aiJumpInterval = 1f;
        public float initialHeightAboveGround = 0f;
        public float jumpHeight = 10f;
        public float maxWalkSlope = 45f;
        
        public int multiJumpCount = 2;
        public bool onlyMultiJumpInWater = true;
    }
}