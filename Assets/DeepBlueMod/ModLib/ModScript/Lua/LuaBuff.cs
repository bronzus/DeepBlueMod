using System.Collections.Generic;
using UnityEngine;
using kz.uitls;
using Mirror;


namespace kz.lua
{
    public class LuaBuff:BaseBuff
    {
        public TextAsset luaScript;
        public InjectVariables luaScriptInjectVariables = new();
    }
}