using UnityEngine;

namespace kz.config
{
    [CreateAssetMenu(fileName = "CharacterExpConfig", menuName = "ScriptableObject/CharacterExpConfig", order = 10)]
    public class CharacterExpConfig:ScriptableObject
    {
        [Tooltip("初始升级所需经验值")]
        public float initialExp;
        [Tooltip("初始升级所需经验值")]
        public float expExponentialGrowRatio = 1.2f;
        [Tooltip("生物被击杀之后所提供的经验值的比例")]
        public float initialExpBeProvidedProportion = 0.45f;

        public float expBeProvidedProportionGrowRatio = 1f;
        public float expBeProvidedProportionGrowExponent = 1.3f;
    }
}