using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Mirror;
using UnityEngine;

namespace kz
{
    public class CharacterSoundManager:MonoBehaviour
    {
        [Serializable]
        public class SprintSoundConfig
        {
            public AudioSource audio;
            public float useAudioSize;
        }
        
        public List<SprintSoundConfig> sprintSoundList = new List<SprintSoundConfig>();
        
        public AudioSource jumpAudio;
        public AudioSource multiJumpAudio;

        public List<AudioSource> stepAudioList;
        public float walkStepSoundInterval = 0.1f;
    }
}