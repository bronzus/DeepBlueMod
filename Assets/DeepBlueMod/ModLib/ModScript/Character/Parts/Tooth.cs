using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;


namespace kz
{
    public class Tooth : NetworkBehaviour
    {
        public float openSpeed = 3.0f;
        public float closeSpeed = 9.0f;

        public Color colorWhenNoPlayerToHit = Color.white;
        public Color colorWhenHasPlayerToHit = Color.yellow;
        public CharacterCore characterCore;
        public Transform bitePoint = null;
        
        public AudioSource hitSound;
        public ParticleSystem hitEffect;

        public GameObject biteToothSpriteParentObj;
        public GameObject catchToothSpriteParentObj;

        public Sprite biteToothIcon;
        public Sprite catchToothIcon;
        
        public BoxCollider hitBox;
        public BoxCollider findEnemyBox;
    }
}