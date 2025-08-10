using Mirror;
using UnityEngine;
using Random = UnityEngine.Random;

namespace kz
{
    public class CharacterAnimator:NetworkBehaviour
    {
        public bool moveUseShader;
        public float SpineYawFreqWhenIdle;
        public float SpineYawFreqSpeedMultiplier;
        public float maxTurnFactorWhenZIsPositive;
        public float maxTurnFactorWhenZIsNegative;
    }
}