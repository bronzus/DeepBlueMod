using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace kz.config
{
    [CreateAssetMenu(fileName = "GameMapConfig", menuName = "ScriptableObject/Game/游戏地图设置", order = 6)]
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

        public Vector2 mapHeightRange = new Vector2(-30, 10);
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