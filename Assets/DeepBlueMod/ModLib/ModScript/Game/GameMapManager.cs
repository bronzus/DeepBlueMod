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
        public Transform cameraInitTransform;
        public BeginnerGuide beginnerGuide;
        public GameObject mapTipPanelPrefab;
    }
}