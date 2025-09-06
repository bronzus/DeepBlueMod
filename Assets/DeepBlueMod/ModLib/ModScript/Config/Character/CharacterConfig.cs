using UnityEngine;
using System;
using System.Collections.Generic;
using kz.uitls;
using Newtonsoft.Json;

namespace kz.config
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "ScriptableObject/CharacterConfig", order = 1)]
    public class CharacterConfig:ScriptableObject
    {
        [Tooltip("生物的层级，层级越高生物越高级，这与进化有关")]
        public int characterRank = 0;
        [Tooltip("初始速度")]
        public float initialSpeed = 4.0f;
        [Tooltip("初始加速的系数， 加速后的速度 = 速度 * 加速系数")]
        public float initialSprintFactor = 2.0f;
        // public float sprintFOV = 100f; //the FOV to use on the camera when player is sprinting.
        [Tooltip("转弯速度")]
        public float initialTurnSmoothing = 0.06f;
        [Tooltip("咬住character的最长时间")]
        public float biteCharacterMaxSeconds = 2f;
        [Tooltip("咬住character时的伤害间隔")]
        public float biteCharacterDamageInterval = 0.3f;
        // [Tooltip("初始吸引肉的半径")]
        // public float initialAttractFoodRadius = 2f;
        [Tooltip("初始最大生命值")]
        public int initialMaxHp = 16;
        [Tooltip("初始攻击力")]
        public int initialAttackPower = 1;
        [Tooltip("初始的体力值上限")]
        public float initialMaxSp = 6f;
        [Tooltip("初始体力值减少的速度")]
        public float initialSpDecreaseSpeed = 1;
        [Tooltip("初始体力值增加的速度")]
        public float initialSpIncreaseSpeed = 2;
        [Tooltip("暴击概率")]
        public float criticalChance = 0.1f;
        [Tooltip("暴击加成")]
        public float criticalMul = 1.5f;

        [Tooltip("理论上的大小，和是否能咬住并控制住对手有关")]
        public float initialTheoreticalSize = 1f;

        public List<string> characterTags = new List<string>() { "SeaFish" };
        
        public CharacterCoinsObtainedAfterKilled coinsObtainedAfterKilled;
        public CharacterGrowConfig growConfig;
        public CharacterAiConfig characterAiConfig;
        public CharacterUnlockCondition UnlockCondition = new CharacterUnlockCondition();
        public CharacterExpConfig expConfig;
        public CharacterMouthConfig mouthConfig;
        public CharacterDieActionConfig characterDieActionConfig;
        public CharacterSwimBehaviourConfig swimBehaviourConfig;
        public CharacterWalkBehaviourConfig walkBehaviourConfig; 
        
        public CharacterAmphibiousBehaviourConfig amphibiousBehaviourConfig;
    }
}