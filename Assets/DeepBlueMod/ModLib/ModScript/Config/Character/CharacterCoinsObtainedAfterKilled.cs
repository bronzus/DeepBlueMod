using UnityEngine;

namespace kz.config
{
    [CreateAssetMenu(fileName = "CharacterCoinsObtainedAfterKilled", menuName = "ScriptableObject/CharacterCoinsObtainedAfterKilled", order = 30)]
    public class CharacterCoinsObtainedAfterKilled:ScriptableObject
    {
        public int initialCoins = 80;
        public float gradientRatio = -0.2f; // 梯度系数
        public int minCoins;
    }
}