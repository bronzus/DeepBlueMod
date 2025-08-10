using System;
using kz.config;
using UnityEngine;
using Mirror;

namespace kz
{
    public class Butt:NetworkBehaviour
    {
        public CharacterCore characterCore;
        public bool eggLaying;
        public GameObject eggPrefab; // 如果为空，则为默认的egg prefab
    }
}