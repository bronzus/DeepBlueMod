using UnityEngine;

namespace kz
{
    public enum AudioMixerGroupType
    {
        Ambience,
        UI,
        Music
    }
    
    public enum BuildInAudioClip
    {
        Eat, ToothHit, FoodSplit
    }


    public class AudioSourceHelper:MonoBehaviour
    {
        public AudioMixerGroupType audioSourceOutput;
        public BuildInAudioClip buildInAudioClip;
    }
}