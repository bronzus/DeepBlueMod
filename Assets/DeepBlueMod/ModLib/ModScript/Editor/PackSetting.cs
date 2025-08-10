using System;
using System.IO;
using System.Collections.Generic;
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
    
    private string modResourceDir = "";
    private string modSceneDir = "";
    private string modInfoJsonName = "ModInfo.json";
    private string previewImageName = "preview.png";

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
        modResourceDir = Path.Join(modMeta.modDir, "Resources");
        modSceneDir = Path.Join(modMeta.modDir, "Scenes");
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
            PackResource(dir);
            BuildScenes(dir);
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
    
    private void PackResource(string buildDir)
    { 
        List<AssetBundleBuild> builds = new List<AssetBundleBuild>();
        
        var build =  new AssetBundleBuild();
        build.assetBundleName =$"{modMeta.name}.ab";
        List<string> _assetDir = new List<string>();
        _assetDir.Add(modResourceDir);

        //2.setName.
        build.assetNames = _assetDir.ToArray();
        builds.Add(build);

        var exportPath = Path.Combine(buildDir, "Resources");

        if (!Directory.Exists(exportPath))
        {
            Directory.CreateDirectory(exportPath);
        }
        BuildPipeline.BuildAssetBundles(exportPath, builds.ToArray(),
            BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        
        AssetDatabase.Refresh();
    }

    private void BuildScenes(string buildDir)
    {
        var exportPath = Path.Combine(buildDir, "Scenes");
        if (!Directory.Exists(exportPath))
        {
            Directory.CreateDirectory(exportPath);
        }
        if (Directory.Exists(modSceneDir))
        {
            // 查找指定目录下的场景文件
            string[] scenes = GetAllFiles(modSceneDir, "*.unity");
            for (int i = 0; i < scenes.Length; i++)
            {
                string url = scenes[i].Replace("\\", "/");
                int index = url.LastIndexOf("/");
                string scene = url.Substring(index + 1, url.Length - index - 1);
                scene = scene.Replace(".unity", ".scene");
                BuildPipeline.BuildPlayer(scenes, Path.Combine(exportPath, scene), BuildTarget.StandaloneWindows, BuildOptions.BuildAdditionalStreamedScenes);
                AssetDatabase.Refresh();
            }
            // EditorUtility.ClearProgressBar();
            Debug.Log("所有场景打包完毕");
        }
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
}
