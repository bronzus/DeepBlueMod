using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using kz;
using UnityEditorInternal;
using UnityEngine;
using UnityEditor.IMGUI.Controls;

namespace kz.editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(FoodSpawner))]
    public class FoodSpawnerEditor: CanBeBittenObjSpawnerEditor
    {
        private readonly Dictionary<string, string> propertiesNameMap = new Dictionary<string, string>()
        {
            { "maxCanBeBittenObjCount", "Max Food Count"},
            { "canBeBittenObjCountMulCurveByLevel", "Food Count Mul Curve By Level"},
            { "spawnCanBeBittenObjInterval", "Spawn Food Interval" },
        };
        
        public override void OnInspectorGUI()
        {
            FoodSpawner canBeBittenObjSpawner = (FoodSpawner)target;
            
            SerializedProperty scriptProp = serializedObject.FindProperty("m_Script");
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(scriptProp);
            EditorGUI.EndDisabledGroup();
            
            EditMode.DoEditModeInspectorModeButton(EditMode.SceneViewEditMode.Collider, "Edit Bounds",
                EditorGUIUtility.IconContent("EditCollider"), GetBounds(), this);
            
            // serializedObject.Update();
            foreach(var kv in propertiesNameMap){
                SerializedProperty parentProp = serializedObject.FindProperty(kv.Key);
    
                // 绘制自定义名称
                EditorGUILayout.PropertyField(parentProp, new GUIContent(kv.Value), true);
            }

            var unDrawKeys = propertiesNameMap.Keys.ToList();
            unDrawKeys.Add("m_Script");
            DrawPropertiesExcluding(serializedObject, unDrawKeys.ToArray());
            serializedObject.ApplyModifiedProperties();
            // base.OnInspectorGUI(); 
        }
    }
}