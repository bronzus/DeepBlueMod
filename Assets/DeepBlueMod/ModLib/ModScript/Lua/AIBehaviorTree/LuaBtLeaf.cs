using MBT;
using UnityEngine;
using kz.uitls;

namespace kz.lua
{
    [MBTNode("fishLuaAI/LuaBtLeaf")]
    public class LuaBtLeaf:Leaf
    {
        public TextAsset luaScript;
        public InjectVariables luaScriptInjectVariables = new();
        
        public override NodeResult Execute()
        {
            return NodeResult.success;
        }
    }
}