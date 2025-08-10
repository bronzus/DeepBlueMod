using UnityEngine;

namespace kz.config
{
    
    [CreateAssetMenu(fileName = "BaseSkillConfig", menuName = "ScriptableObject/skill/BaseSkillConfig", order = 100)]
    public class BaseSkillConfig:ScriptableObject
    {
        public float initialCdSeconds = 10f;
        public float cdSecondsGrowRatio = 0f;
        public int initialCoinsForLevelUp = 10;
        public float coinsForLevelUpGrowRatio = 0.1f;
        public int maxLevel = 10;
    }
}