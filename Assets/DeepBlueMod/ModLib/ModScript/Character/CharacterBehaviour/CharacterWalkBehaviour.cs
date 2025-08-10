using System;
using UnityEngine;
using Mirror;
using System.Collections;

namespace kz
{
    public class CharacterWalkBehaviour:CharacterBehaviour
    {
        public float aiJumpInterval = 1f;
        public float jumpHeight = 10f;
        
        public GameObject multiJumpEffectPrefab;
        public float multiJumpEffectPrefabRadius = 1f;
        public int multiJumpCount = 2;
        public bool onlyMultiJumpInWater = true;
    }
}