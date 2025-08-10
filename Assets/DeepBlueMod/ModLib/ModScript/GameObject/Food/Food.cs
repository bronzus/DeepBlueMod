using System;
using System.Collections.Generic;
using UnityEngine;
using kz.config;

namespace kz
{
    [Serializable]
    public class FoodPartConfig
    {
        public GameObject foodPrefab;
        public Vector3 foodLocalPosition;
    }
    
    public class Food : BittenBehaviour
    {
        public FoodConfig foodConfig;
        [Header("食物被切分")] 
        public bool canSplit;
        public List<FoodPartConfig> foodPartList;
        public AudioClip foodSplitAudio;
        public float foodSplitAudioVolume = 1f;
    }
}
