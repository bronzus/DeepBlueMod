using UnityEngine;
using System.Collections.Generic;

namespace kz.config
{
    [CreateAssetMenu(fileName = "CharacterDieActionConfig", menuName = "ScriptableObject/CharacterDieActionConfig", order = 2)]
    public class CharacterDieActionConfig:ScriptableObject
    {
        public List<FoodSpawner.FoodInfo> dieGenFoods;
    }
}