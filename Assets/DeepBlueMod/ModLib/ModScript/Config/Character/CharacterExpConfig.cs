using UnityEngine;

namespace kz.config
{
    [CreateAssetMenu(fileName = "CharacterExpConfig", menuName = "ScriptableObject/角色提供经验值配置", order = 10)]
    public class CharacterExpConfig:ScriptableObject
    {
        [Tooltip("初始升级所需经验值")]
        public float initialExp;
        [Tooltip("初始升级所需经验值")]
        public float expExponentialGrowRatio = 1.2f;

        public float expBeProvidedProportion = 0.45f;
    }
}