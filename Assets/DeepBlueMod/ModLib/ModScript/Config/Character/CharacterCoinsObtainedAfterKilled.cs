using UnityEngine;

namespace kz.config
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "ScriptableObject/角色死完后金币配置文件", order = 30)]
    public class CharacterCoinsObtainedAfterKilled:ScriptableObject
    {
        public int initialCoins = 80;
        public float gradientRatio = -0.2f; // 梯度系数
        public int minCoins;
    }
}