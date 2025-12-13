using UnityEngine;
using System;

namespace kz.config
{
    [Serializable]
    public class CharacterAutoRecoverHpConfig
    {
        [Tooltip("自动回血的比例")] 
        public float autoRecoverHpRatio = 0.01f;
        [Tooltip("自动回血的间隔")]
        public float autoRecoverHpInterval = 1f;
        [Tooltip("自动回血的延迟")] 
        public float autoRecoverHpDelay = 5f;
    }
}