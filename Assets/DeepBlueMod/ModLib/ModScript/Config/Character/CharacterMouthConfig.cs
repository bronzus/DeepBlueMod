using UnityEngine;

namespace kz.config
{
    [CreateAssetMenu(fileName = "CharacterMouthConfig", menuName = "ScriptableObject/CharacterMouthConfig", order = 20)]
    public class CharacterMouthConfig:ScriptableObject
    {
        [Tooltip("初始的嘴巴大小")]
        public float initialMouthSize = 1f;
        [Tooltip("嘴巴大小的成长系数")]
        public float mouthSizeGrowRatio = 0.15f;
        [Tooltip("吃肉的间隔")]
        public float eatFoodDuration = 0.5f;
        [Tooltip("初始吸肉的半径")]
        public float initialSuckRadius;
        [Tooltip("吸肉半径的成长系数")]
        public float suckRadiusGrowRatio;
        [Tooltip("直接吃的系数")] 
        public float directlyEatCharacterRatio = 0.9f;
    }
}