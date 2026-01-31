using Mirror;
using UnityEngine;

namespace kz
{
    public class Portal : NetworkBehaviour
    {
        [Scene] public string changeMapPath;

        public ParticleSystem portalEffect;
        public float portalEffectShowSqrDst = 6000f;
    }
}