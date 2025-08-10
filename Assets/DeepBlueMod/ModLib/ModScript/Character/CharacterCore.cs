using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Mirror;
using Random = UnityEngine.Random;

namespace kz
{
    [Serializable]
    public class CharacterSkin
    {
        [Serializable]
        public class CharacterRender
        {
            public Renderer renderer;
            public List<Material> materials;
        }
        public List<CharacterRender> characterRenders;
    }

    public class CharacterCore : BittenBehaviour
    {
        public kz.config.CharacterConfig defaultCharacterConfig;
        public Tooth tooth;
        public Mouth mouth;
        public Butt butt;
        public float maxTurnAngle = 40f;
        public GameObject characterPreviewPrefab;
        public List<CharacterSkin> characterSkins = new List<CharacterSkin>(); // 第一个皮肤必须是人物的默认皮肤，如果为空，默认只有当前这套皮肤，并尝试获取这套皮肤
        public Transform rootBone; // 如果为空，就尝试从第一套皮肤中自动获取，当然也可能没有任何骨骼。
        public GameObject beHurtEffect;
        public float beHurtEffectSize = 1f;
        
        public GameObject playerPrefab; // 指向让玩家玩的 prefab
    }
}