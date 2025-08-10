using MBT;
using UnityEngine;
using kz.uitls;

namespace kz.lua
{
    [MBTNode("fishLuaAI/LuaBtService")]
    public class LuaBtService:Service
    {
        public TextAsset luaScript;
        public InjectVariables luaScriptInjectVariables = new();

        public override void Task()
        {
        }
    }
}