using kz.uitls;
using Mirror;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace kz
{
    [Serializable]
    public class SkillInfo
    {
        public string modId;
        public string skillName;
    }
    
    public class SkillBuffCore:NetworkBehaviour
    {
        public List<GameObject> skillPrefabList = new List<GameObject>();
        public List<SkillInfo> skillInfoList = new List<SkillInfo>();
        [HideInInspector]public InjectVariables injectVariables = new();
    }
}