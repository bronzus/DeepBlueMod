using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace kz.mod
{
    [CreateAssetMenu(fileName = "ModContent", menuName = "ScriptableObject/ModContent", order = 1)]
    public class ModContent: ScriptableObject
    {
        public List<MapInfo> modMapInfos = new List<MapInfo>();
        public List<GameObject> nonPlayableCharacters = new List<GameObject>();
        public List<GameObject> playableCharacters = new List<GameObject>();
        public List<GameObject> foods = new List<GameObject>();
        public List<GameObject> skills = new List<GameObject>();
        public List<GameObject> buffs = new List<GameObject>();
    }
    
    [System.Serializable]
    public class ModInfo
    {
        public string modName;
        public string modContentPath;
        public string modVersion;
    }
}