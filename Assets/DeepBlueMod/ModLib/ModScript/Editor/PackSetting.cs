using System;
using System.IO;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

[Serializable]
public class ModInfo
{
    public enum ModType
    {
        Normal, Config
    }

    public string id;
    public string name;
    public string modContentAbPath;
    public string description;
    public ModType modType;
    public ulong buildTime;
    public ulong publishedFileID;
    public string modAPIVersion = "v2";
}

public class PackSetting: EditorWindow
{
    [Serializable]
    public class ModMeta
    {
        public string modDir;
        public string modBuildDir;
        public string id;
        public string name;
        public string modContentAbPath;
        public string description;
        public ulong publishedFileID;
        private static string modMetaJsonPath = Path.Join("Assets", "ModMeta.json");
        
        public static ModMeta Init()
        {
            if (File.Exists(modMetaJsonPath))
            {
                return JsonUtility.FromJson<ModMeta>(File.ReadAllText(modMetaJsonPath));
            }

            return new ModMeta();
        }

        public void Save()
        {
            var jsonStr = JsonUtility.ToJson(this, true);
            File.WriteAllText(modMetaJsonPath, jsonStr);
        }
    }
    
    private string modInfoJsonName = "ModInfo.json";
    private string previewImageName = "preview.png";
    private string modLibResourcesPath = "Assets/DeepBlueMod/ModLib/ModResources";
    private string ModLibShaderPath = "Assets/DeepBlueMod/ModLib/ModShader";

    private ModMeta _modMeta;

    private ModMeta modMeta {
        get
        {
            if (_modMeta == null)
            {
                _modMeta = ModMeta.Init();
            }

            return _modMeta;
        }
    }

    [MenuItem("Tools/PackSetting")]
    public static void ShowWindow()
    {
        // 显示一个窗口
        GetWindow(typeof(PackSetting));
    }
    
    void OnGUI()
    {
        EditorGUILayout.LabelField("mod id", modMeta.id);
        modMeta.modDir = EditorGUILayout.TextField("mod dir",modMeta.modDir);
        modMeta.modBuildDir = EditorGUILayout.TextField("mod build dir",modMeta.modBuildDir);
        modMeta.name = EditorGUILayout.TextField("mod name",modMeta.name);
        modMeta.modContentAbPath = EditorGUILayout.TextField("modContent AB Path",modMeta.modContentAbPath);
        modMeta.description = EditorGUILayout.TextField("mod description", modMeta.description);
        modMeta.publishedFileID = (ulong)EditorGUILayout.LongField("publishedFileID", (long)modMeta.publishedFileID);
        if (GUILayout.Button("copy mod id"))
        {
            GUIUtility.systemCopyBuffer = modMeta.id;
        }
        
        if (GUILayout.Button("Save"))
        {
            modMeta.Save();
        }
        
        if (GUILayout.Button("Generate Mod Id"))
        {
            if (String.IsNullOrEmpty(modMeta.id))
            {
                modMeta.id = GenerateUuid();
                modMeta.Save();
            }
        }

        if (GUILayout.Button("Pack"))
        {
            if (String.IsNullOrEmpty(modMeta.modBuildDir))
            {
                Debug.LogError("mod build dir cannot be empty.");
                return;
            }
            
            if (String.IsNullOrEmpty(modMeta.name))
            {
                Debug.LogError("mod name cannot be empty.");
                return;
            }

            if (String.IsNullOrEmpty(modMeta.id))
            {
                modMeta.id = GenerateUuid();
                modMeta.Save();
            }

            var dir = CreateExportDir();
            BuildAllAssetBundles(dir);
            CreateModInfoJson(Path.Combine(dir, modInfoJsonName));
            var previewImagePath = GetPreviewImagePath();
            if (File.Exists(previewImagePath))
            {
                File.Copy(previewImagePath, Path.Combine(dir, previewImageName), true);
            }
        }
    }

    private void CreateModInfoJson(string filePath)
    {
        var fishModInfo = new ModInfo()
        {
            id = modMeta.id,
            name = modMeta.name,
            modContentAbPath = modMeta.modContentAbPath,
            description = modMeta.description,
            modType = ModInfo.ModType.Normal,
            buildTime = GetTimeStampMilliseconds(),
            publishedFileID = modMeta.publishedFileID
        };
        
        string json = JsonUtility.ToJson(fishModInfo,true);
        File.WriteAllText(filePath, json);
    }

    private string GetPreviewImagePath()
    {
        return Path.Combine(modMeta.modDir, previewImageName);
    }

    private string CreateExportDir()
    {
        var exportPath = Path.Combine(modMeta.modBuildDir, modMeta.name);
        if (Directory.Exists(exportPath))
        {
            Directory.Delete(exportPath, true);
        }
        Directory.CreateDirectory(exportPath);
        return exportPath;
    }
    
    
    
    private static string[] GetAllFiles(string directory, params string[] types)
    {
        if (!Directory.Exists(directory)) return new string[0];
        string searchTypes = (types == null || types.Length == 0) ? "*.*" : string.Join("|", types);
        string[] names = Directory.GetFiles(directory, searchTypes, SearchOption.AllDirectories);
        return names;
    }
    
    public static ulong GetTimeStampMilliseconds()
    {
        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        TimeSpan timeSpan = DateTime.UtcNow - epochStart;
        return (ulong)timeSpan.TotalMilliseconds;
    }
    
    public static string GenerateUuid()
    {
        Guid uuid = Guid.NewGuid(); // 生成一个新的 UUID
        return uuid.ToString(); // 转换为字符串格式
    }
    
    
    // -------------------
    private void AssignAssetBundles()
    {
        // 清除所有现有的AssetBundle分配
        ClearAllAssetBundleNames();
        AssignResourcesAssetBundleName();
        AssetDatabase.RemoveUnusedAssetBundleNames();
        Debug.Log("AssetBundle分配完成！");
    }
    
    private void ClearAllAssetBundleNames()
    {
        // 获取所有AssetBundle名称
        string[] bundleNames = AssetDatabase.GetAllAssetBundleNames();
        
        // 清除所有AssetBundle分配
        foreach (string bundleName in bundleNames)
        {
            AssetDatabase.RemoveAssetBundleName(bundleName, true);
        }
        
        Debug.Log("已清除所有AssetBundle分配");
    }

    private List<string> GetAssetPaths()
    {
        var assetPaths = new List<string>();
        assetPaths.AddRange(Directory.GetFiles(modMeta.modDir, "*", SearchOption.AllDirectories));
        assetPaths.AddRange(Directory.GetFiles(modLibResourcesPath, "*", SearchOption.AllDirectories));
        assetPaths.AddRange(Directory.GetFiles(ModLibShaderPath, "*", SearchOption.AllDirectories));
        return assetPaths;
    }

    private void AssignResourcesAssetBundleName()
    {
        if (!Directory.Exists(modMeta.modDir))
        {
            Debug.LogWarning($"Resources文件夹不存在: {modMeta.modDir}");
            return;
        }
        
        // 获取所有资源路径（递归）
        var assetPaths = GetAssetPaths();
        
        foreach (string assetPath in assetPaths)
        {
            // 跳过.meta文件和场景文件
            if (assetPath.EndsWith(".meta") || assetPath.EndsWith(".cs"))
                continue;
            AssetImporter importer = AssetImporter.GetAtPath(assetPath);
            if (importer != null)
            {
                if (assetPath.EndsWith(".unity"))
                {
                    string bundleName = Path.GetFileNameWithoutExtension(assetPath).ToLower();
                    importer.assetBundleName = $"{bundleName}.scene";
                }
                else
                {
                    // 分配到base.ab
                    importer.assetBundleName = "base.ab";
                }
            }
        }
        
        Debug.Log($"已将{modMeta.modDir}文件夹下的{assetPaths.Count}个资源分配到base.ab");
    }
    
    private void ClearResourcesAssetBundleName()
    {
        if (!Directory.Exists(modMeta.modDir))
        {
            Debug.LogWarning($"Resources文件夹不存在: {modMeta.modDir}");
            return;
        }
        
        // 获取所有资源路径（递归）
        var assetPaths = GetAssetPaths();
        
        foreach (string assetPath in assetPaths)
        {
            // 跳过.meta文件和场景文件
            if (assetPath.EndsWith(".meta") || assetPath.EndsWith(".cs"))
                continue;
            AssetImporter importer = AssetImporter.GetAtPath(assetPath);
            if (importer != null)
            {
                importer.assetBundleName = "";
            }
        }
    }
    
    
    public void BuildAllAssetBundles(string exportDir)
    {
        // 先分配AssetBundle
        AssignAssetBundles();
        
        string[] assetBundleNames = AssetDatabase.GetAllAssetBundleNames();
        if (assetBundleNames.Length == 0)
        {
            Debug.LogError("没有设置任何AssetBundle名称，请检查资源分配！");
            return;
        }
        
        // 构建AssetBundle
        BuildPipeline.BuildAssetBundles(
            exportDir, 
            BuildAssetBundleOptions.None, 
            BuildTarget.StandaloneWindows64
        );
        
        Debug.Log($"AssetBundle打包完成！输出目录: {exportDir}");
        ClearResourcesAssetBundleName();
        ClearAllAssetBundleNames();
    }
}
