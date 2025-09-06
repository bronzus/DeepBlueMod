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

        public float expBeProvidedProportion = 0.45f;
    }
}