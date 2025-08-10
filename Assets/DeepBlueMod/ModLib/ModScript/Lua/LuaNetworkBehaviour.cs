using Mirror;
using System.Collections.Generic;
using UnityEngine;
using kz.uitls;

namespace kz.lua
{
    public class LuaNetworkBehaviour:NetworkBehaviour
    {
        public TextAsset luaScript;
        public InjectVariables luaScriptInjectVariables = new();
    }
}