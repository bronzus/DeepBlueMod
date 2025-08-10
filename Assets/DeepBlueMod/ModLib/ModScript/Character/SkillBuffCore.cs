using kz.uitls;
using Mirror;
using System.Collections.Generic;
using UnityEngine;

namespace kz
{
    public class SkillBuffCore:NetworkBehaviour
    {
        public List<GameObject> skillPrefabList = new List<GameObject>();
        [HideInInspector]public InjectVariables injectVariables = new();
    }
}