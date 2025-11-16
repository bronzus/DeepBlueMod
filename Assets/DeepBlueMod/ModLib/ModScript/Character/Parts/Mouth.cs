using Mirror;
using UnityEngine;

namespace kz
{
    public class Mouth : NetworkBehaviour
    {
        public CharacterCore characterCore;
        public AudioClip eatFoodSound;
        public ParticleSystem mouthSuctionEffect;
    }
}