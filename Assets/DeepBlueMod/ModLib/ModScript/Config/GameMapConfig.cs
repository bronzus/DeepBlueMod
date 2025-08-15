using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace kz.config
{
    [CreateAssetMenu(fileName = "GameMapConfig", menuName = "ScriptableObject/Game/GameMapConfig", order = 6)]
    public class GameMapConfig:ScriptableObject
    {
        [Serializable]
        public class LayerCullDistanceConfig
        {
            public string layerName;
            public float highDetailExtraDst;
        }

        [Serializable]
        public class MiniMapConfig
        {
            public Sprite mapImage;
            public Vector3 mapStartPos;
            public Vector3 mapEndPos;
        }

        public enum FollowerGenerateType
        {
            GiveBirthToAChild,
            AutoGenerateFollower
        }
        [Tooltip("Map height range (地图的高度范围)")]
        public Vector2 mapHeightRange = new Vector2(-200, 200);
        public float waterLevel = 0f;
        public float closeSyncSqrDst = 2500f;
        public float mediumSyncSqrDst = 8100f;
        public float showObjSqrDst = 20000f;
        public float npcUnSpawnExtraSqrDst = 400f;
        public float canBeBittenObjUnSpawnExtraSqrDst = 400f;
        public MiniMapConfig miniMapConfig;
        public List<LayerCullDistanceConfig> allLayerCullDistanceConfig;
        public bool canSameKindCharacterAttackEachOther = false; // 
        public bool canDieChangeToFollower = false;

        public FollowerGenerateType followerGenerateType;
        
        public int maxAutoGenerateFollowerNumber = 5;
        public int autoGenerateFollowerGenerateInterval = 100;
        
        public int characterCanGiveBirthToAChildLevel = 25;
        public int characterGiveBirthToAChildLoseExpRatio = 23;
    }
}