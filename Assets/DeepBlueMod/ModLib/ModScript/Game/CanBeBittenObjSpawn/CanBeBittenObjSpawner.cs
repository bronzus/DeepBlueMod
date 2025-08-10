using UnityEngine;
using Mirror;
using Random = UnityEngine.Random;
using System.Collections.Generic;


namespace kz
{
    public class CanBeBittenObjSpawner : MonoBehaviour
    {
        public Bounds spawnerBounds;
        
        public int maxCanBeBittenObjCount;
        public AnimationCurve canBeBittenObjCountMulCurveByLevel;
        public float spawnCanBeBittenObjInterval; //seconds
        public int numberOfEachSpawn;
    }
}