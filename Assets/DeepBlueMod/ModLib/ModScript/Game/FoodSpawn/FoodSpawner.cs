using System;
using UnityEngine;

namespace kz
{
    public class FoodSpawner:CanBeBittenObjSpawner
    {
        [Serializable]
        public class FoodInfo
        {
            public string modId;
            public string foodName;
        }
        
        public FoodInfo foodInfo; // for mod
    }
}