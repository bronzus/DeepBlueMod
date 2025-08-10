using UnityEngine;
using System.Collections.Generic;
using System;

namespace kz
{
    public class NpcSpawner:MonoBehaviour
    {

        [Serializable]
        public class SpawnNpcStrengthConfig
        {
            public int limitCount;
            public AnimationCurve countMulCurveByLevel;

            public Vector2Int initialSpawnNpcLevelRange;
            public float npcGrowRatio = 0f;
        }

        [Serializable]
        public class NpcInfo
        {
            public string modId;
            public bool playAble;
            public string npcName;
        }

        [Serializable]
        public class NpcSkinConfig
        {
            public int skinIndex;
            public float weight;
        }

        [Serializable]
        public class NpcSpawnPoint
        {
            public Vector3 localPos;
            public float radius;
        }

        public bool showSpawnPreview = true;

        public NpcInfo npcInfo; // for mod
        public List<SpawnNpcStrengthConfig> spawnNpcStrengthConfigList = new List<SpawnNpcStrengthConfig>();
        
        public float spawnNpcInterval; //seconds
        public int numberOfEachSpawn;
        public List<NpcSpawnPoint> npcSpawnPoints = new List<NpcSpawnPoint>()
        {
            new NpcSpawnPoint()
            {
                localPos = Vector3.zero,
                radius = 10f
            }
        };
        public float patrolRadius = 20f;
        public List<NpcSkinConfig> npcSkinConfigs = new List<NpcSkinConfig>();

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (Application.isPlaying || !showSpawnPreview) return;
            
            GameObject[] selectedObjects = UnityEditor.Selection.gameObjects;

            // 检查当前对象是否在选中的 GameObject 列表中
            bool isSelected = System.Array.Exists(selectedObjects, obj => obj == gameObject);

            if (isSelected)
            {
                Gizmos.color = new Color(Color.blue.r,Color.blue.g,Color.blue.b, 0.5f);
                Gizmos.DrawSphere(transform.position, this.patrolRadius);
                
                Gizmos.color = new Color(Color.red.r,Color.red.g,Color.red.b, 0.5f);
                foreach (var point in this.npcSpawnPoints)
                {
                    Gizmos.DrawSphere(transform.position + point.localPos, point.radius);
                }
            }
            else
            {
                Gizmos.color = new Color(Color.cyan.r,Color.cyan.g,Color.cyan.b, 0.5f);
                Gizmos.DrawSphere(transform.position, this.patrolRadius);
            }
        }
#endif
    }
}