using System;
using Mirror;
using UnityEngine;
using kz.uitls;
using kz.config;
using MBT;

namespace kz
{
    public class BaseSkill:SkillBuffBase
    {
        public string skillNameLocalizedKey;
        public BaseSkillConfig baseConfig;
        public Sprite skillIcon;
        public bool passiveSkill;
    }
}