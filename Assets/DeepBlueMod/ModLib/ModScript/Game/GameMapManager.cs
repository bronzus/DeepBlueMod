using System;
using UnityEngine;
using System.Collections.Generic;
using kz.config;

namespace kz
{
    public class GameMapManager:MonoBehaviour
    {
        [Serializable]
        public class BeginnerGuide
        {
            public Vector3 guidePos;
            public List<string> guideTeachKeys;
        }
        public GameMapConfig gameMapConfig;
        [Tooltip("Set the initial position and rotation of the in-game camera upon map loading.\n 将设置游戏里的相机在刚加载地图时的位置和旋转")]
        public Transform cameraInitTransform;
        public BeginnerGuide beginnerGuide;
        public GameObject mapTipPanelPrefab;
    }
}