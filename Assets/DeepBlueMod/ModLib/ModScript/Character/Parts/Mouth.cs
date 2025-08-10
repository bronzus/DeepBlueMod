using Mirror;
using UnityEngine;

namespace kz
{
    public class Mouth : NetworkBehaviour
    {
        public CharacterCore characterCore;
        public AudioSource eatFoodSound;
        public ParticleSystem mouthSuctionEffect;
    }
}