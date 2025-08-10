using System.Collections.Generic;
using UnityEngine;
using kz.uitls;
using Mirror;

namespace kz.lua
{
    public class LuaSkill : BaseSkill
    {
        public TextAsset luaScript;
        public InjectVariables luaScriptInjectVariables = new();
    }
}