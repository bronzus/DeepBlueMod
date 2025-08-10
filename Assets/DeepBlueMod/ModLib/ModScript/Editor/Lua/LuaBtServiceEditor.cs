using kz.lua;
using UnityEngine;
using UnityEditor;

namespace kz.editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(LuaBtService))]
    public class LuaBtServiceEditor:InjectVariablesBaseEditor
    {
        public override void OnInspectorGUI()
        {
            LuaBtService luaBehaviour = (LuaBtService)target;
            SerializedObject serializedObject = new SerializedObject(target);
            
            SerializedProperty scriptProp = serializedObject.FindProperty("m_Script");
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(scriptProp);
            EditorGUI.EndDisabledGroup();
            
            
            this.SetInjectVariablesUI(serializedObject, luaBehaviour.luaScriptInjectVariables, serializedObject.FindProperty("luaScriptInjectVariables"));
            
            DrawPropertiesExcluding(serializedObject, new [] {"m_Script", "luaScriptInjectVariables"});
            if (EditorGUI.EndChangeCheck()) {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}