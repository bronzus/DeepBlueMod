using UnityEngine;
using UnityEditor;

namespace kz.editor
{
    public class GenerateCharacterPrefabPreviewImg:EditorWindow
    {
        private GameObject prefab;

        private int width = 512;
        private int height = 512;

        [MenuItem("Tools/kz/Character/Generate Character Prefab Preview Img")]
        public static void ShowWindow()
        {
            GetWindow(typeof(GenerateCharacterPrefabPreviewImg));
        }

        void OnGUI()
        {
            GUILayout.Label("Select Prefab to Generate", EditorStyles.boldLabel);
            prefab = (GameObject)EditorGUILayout.ObjectField("prefab", prefab, typeof(GameObject), false);
            
            if (GUILayout.Button("Generate"))
            {
                Generate();
            }
        }


        
        public void Generate()
        {
            // 获取当前选中的 Prefab
            if (prefab == null)
            {
                Debug.LogError("Please select a Prefab");
                return;
            }

            // 获取 Prefab 的预览图
            var previewTexture = GetAssetPreview(prefab);
            if (previewTexture == null)
            {
                Debug.LogError("Failed to generate preview for the selected Prefab.");
                return;
            }

            // 保存预览图为文件
            byte[] pngData = previewTexture.EncodeToPNG();
            if (pngData != null)
            {
                string prefabPath = AssetDatabase.GetAssetPath(prefab);
                string previewPath = prefabPath.Replace(".prefab", "_PreviewImg.png"); // 设置预览图的文件路径
                System.IO.File.WriteAllBytes(previewPath, pngData);
                AssetDatabase.Refresh();

                Debug.Log("Prefab preview generated and saved successfully at: " + previewPath);
            }
            else
            {
                Debug.LogError("Failed to encode preview texture to PNG format.");
            }
        }
        
        
        /// <summary>
        /// 获取预览图象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private Texture2D GetAssetPreview(GameObject obj)
        {
            GameObject clone = GameObject.Instantiate(obj);
            Camera renderCamera = obj.GetComponentInChildren<Camera>();
            
            // 创建一个RenderTexture用于捕获渲染结果
            RenderTexture renderTexture = new RenderTexture(width, height, 24);
            renderCamera.targetTexture = renderTexture;
            
            // 渲染对象
            renderCamera.Render();
            
            RenderTexture.active = renderTexture;
            var previewTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            previewTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            previewTexture.Apply();
            RenderTexture.active = null;

            return previewTexture;
        }
    }

}