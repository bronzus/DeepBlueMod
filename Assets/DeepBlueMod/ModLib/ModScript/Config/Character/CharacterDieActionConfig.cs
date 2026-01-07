using UnityEngine;
using System.Collections.Generic;

namespace kz.config
{
    public enum CharacterDieGenFoodType
    {
        Default,
        Line,
    }

    [CreateAssetMenu(fileName = "CharacterDieActionConfig", menuName = "ScriptableObject/CharacterDieActionConfig", order = 2)]
    public class CharacterDieActionConfig:ScriptableObject
    {
        public CharacterDieGenFoodType dieGenFoodType = CharacterDieGenFoodType.Default;
        public float genFoodNumMul = 1f;
        public float genFoodInterval = 2f;
        public List<FoodSpawner.FoodInfo> dieGenFoods;
    }
}