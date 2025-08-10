using UnityEngine;

namespace kz.config
{
    [CreateAssetMenu(fileName = "Food", menuName = "ScriptableObject/Food/FoodConfig", order = 1)]
    public class FoodConfig:ScriptableObject
    {
        [Header("食物刚体属性")]
        public bool floatInWater = false;

        public float dragInWater = 10f;
        public float angularDragInWater = 15f;
        public float dragOutWater = 0.1f;
        public float angularDragOutWater = 0.05f;
        
        [Header("直接吃")]
        public int defaultFoodLevel = int.MaxValue; // 如果 ai的level大于food的level，就不会选择去吃
        public float wholeFoodSize = 1f;
        public float defaultAddCharacterExperience = 1;
        public int addCharacterHP = 0;
        public int eatTimes = 1;
        
        [Header("食物被切分")] 
        public int splitHp;
    }
}