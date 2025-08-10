using UnityEngine;

namespace kz.config
{
    [CreateAssetMenu(fileName = "DefaultSkillUseConfig", menuName = "ScriptableObject/skillUseConfig/DefaultSkillUseConfig", order = 200)]

    public class DefaultAISkillUseConfig:ScriptableObject
    {
        public bool isDistanceLimit;
        // public float initialDistanceLimit = 10f;
        [Tooltip("初始的距离限制，这个和体型有关")]
        public Vector2 initialDistanceLimitRange = new Vector2(2f, 20f);

        public bool isCustomDistanceLimitRangeGrowRatio = false;
        public Vector2 customDistanceLimitRangeGrowRatio = Vector2.zero;
        
        public bool isAngleLimit;
        public float angleLimit = 180;
    }
}