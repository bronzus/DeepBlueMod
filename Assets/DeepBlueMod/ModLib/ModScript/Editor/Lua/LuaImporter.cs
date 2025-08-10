using System.IO;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace kz.editor
{
    [ScriptedImporter(1, new[] { "lua", "wxlua" })]
    public class LuaImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            // 1. 读取Lua文件内容
            string luaContent = File.ReadAllText(ctx.assetPath);

            // 2. 创建TextAsset对象
            TextAsset luaAsset = new TextAsset(luaContent);
            luaAsset.name = Path.GetFileNameWithoutExtension(ctx.assetPath);
        
            // 3. 添加为主要资产
            ctx.AddObjectToAsset("main", luaAsset);
            ctx.SetMainObject(luaAsset);
        }
    }
}