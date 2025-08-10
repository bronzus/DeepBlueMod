using System;
using UnityEngine;
using Mirror;
using System.Collections.Generic;

namespace kz
{
    [Serializable]
    public class MapInfo
    {
        [Serializable]
        public class ModMapExtraInfo
        {
            public bool useOriginalWater = false;
            public string waterPrefabName = "SeaWater";
        }

        [Scene] public string scenePath;
        public Sprite sceneIcon;
        public string mapNameKey;
        public List<string> characterInMapTagCondition = new List<string>();
        public ModMapExtraInfo modMapExtraInfo;
    }
}