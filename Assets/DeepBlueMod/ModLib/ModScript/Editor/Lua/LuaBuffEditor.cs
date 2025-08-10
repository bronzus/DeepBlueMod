using UnityEditor;
using UnityEngine;
using kz.lua;

namespace kz.editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(LuaBuff))]
    public class LuaBuffEditor:InjectVariablesBaseEditor
    {
        public override void OnInspectorGUI()
        {
            LuaBuff luaBehaviour = (LuaBuff)target;
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