using UnityEngine;

namespace kz.config
{
    [CreateAssetMenu(fileName = "CharacterEvolveConfig", menuName = "ScriptableObject/进化配置文件", order = 3)]
    public class CharacterEvolveConfig:ScriptableObject
    {
        [Tooltip("一开始进化能提升生物级别的概率")]
        public float initialEvolveRankUpChance = 0.05f; // 0-1
        [Tooltip("提升生物级别的概率的提升系数")]
        public float evolveRankUpChanceIncreaseRatio = 0.02f;
        public Vector2Int evolveLevelIntervalRange = new Vector2Int(5, 8);
    }
}