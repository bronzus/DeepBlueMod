using UnityEngine;
using System.Collections.Generic;

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
        [Tooltip("吸引攻击的属性")] 
        public float aggroIndex = 2;
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
        public float spIncreaseSpeedMulWhenJog = 0.3f;
        [Tooltip("暴击概率")]
        public float criticalChance = 0.1f;
        [Tooltip("暴击加成")]
        public float criticalMul = 1.5f;
        [Tooltip("理论上的大小，和是否能咬住并控制住对手有关")]
        public float initialTheoreticalSize = 1f;
        [Tooltip("生物的标签，例如如果生物的标签为 SeaFish, 那么如果一张地图有SeaFish的标签，那这个生物就可以在这张地图上游玩")]
        public List<string> characterTags = new List<string>() { "SeaFish" };
        [Tooltip("生物被击杀之后提供的金币配置")]
        public CharacterCoinsObtainedAfterKilled coinsObtainedAfterKilled;
        [Tooltip("生物的成长")]
        public CharacterGrowConfig growConfig;
        [Tooltip("生物的AI配置")]
        public CharacterAiConfig characterAiConfig;
        [Tooltip("生物的解锁条件")]
        public CharacterUnlockCondition UnlockCondition = new CharacterUnlockCondition();
        [Tooltip("生物的经验值条件")]
        public CharacterExpConfig expConfig;
        [Tooltip("生物的经验值条件")]
        public CharacterMouthConfig mouthConfig;
        [Tooltip("生物在死亡之后的配置")]
        public CharacterDieActionConfig characterDieActionConfig;
        [Tooltip("SwimBehaviour 的配置, 如果生物没有CharacterSwimBehaviour组件,那么这个配置无效")]
        public CharacterSwimBehaviourConfig swimBehaviourConfig;
        [Tooltip("walkBehaviour 的配置, 如果生物没有CharacterWalkBehaviour组件,那么这个配置无效")]
        public CharacterWalkBehaviourConfig walkBehaviourConfig; 
        [Tooltip("walkBehaviour 的配置, 如果生物没有CharacterAmphibiousBehaviour组件,那么这个配置无效")]
        public CharacterAmphibiousBehaviourConfig amphibiousBehaviourConfig;
        public CharacterAutoRecoverHpConfig autoRecoverHpConfig = new();
    }
}