using UnityEngine;
using System.Collections.Generic;

namespace kz.config
{
    [CreateAssetMenu(fileName = "CharacterDieActionConfig", menuName = "ScriptableObject/角色死亡生成配置文件", order = 2)]
    public class CharacterDieActionConfig:ScriptableObject
    {
        public List<FoodSpawner.FoodInfo> dieGenFoods;
    }
}