using UnityEngine;
using kz.uitls;

namespace kz.lua
{
    public class LuaBehaviour: MonoBehaviour
    {
        public TextAsset luaScript;
        public InjectVariables luaScriptInjectVariables = new();
    }
}