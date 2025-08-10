using System.Linq;
using UnityEditor;
using UnityEngine;
using kz.uitls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditorInternal;

namespace kz.editor
{
    public class InjectVariablesBaseEditor:Editor
    {
        public enum InjectVariableType
        {
            GameObject,
            Int,
            Bool,
            Float,
            String,
            Vector2,
            Vector3,
            Quaternion,
            GameObjectList,
            IntList,
            BoolList,
            FloatList,
            StringList,
            Vector2List,
            Vector3List,
            QuaternionList
        }
        
        private InjectVariableType selectedVariableType = 0;
        private string newVariableKey = "";
        private bool showVariables = true;
        readonly string[] varOptions = new string[]{"Copy Key", "Delete"};

        protected void SetInjectVariablesUI(SerializedObject serializedObject,InjectVariables variables,SerializedProperty variablesProperty)
        {
            EditorGUILayout.LabelField("Create Variable", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Key", GUILayout.MaxWidth(80));
            newVariableKey = EditorGUILayout.TextField(newVariableKey);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Type", GUILayout.MaxWidth(80));
            
            selectedVariableType = (InjectVariableType)EditorGUILayout.Popup((int)selectedVariableType, Enum.GetNames(typeof(InjectVariableType)));
            GUI.SetNextControlName("AddButton");
            if (GUILayout.Button("Add", EditorStyles.miniButton)) {
                CreateVariableAndResetInput(variables,variablesProperty);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
            
            serializedObject.Update();
            
            this.GetAllInjectVariables(variables,variablesProperty,out List<InjectVariables.BaseInjectVariable> allVariables, out List<SerializedProperty> variablesPropertyVariables);
            showVariables = EditorGUILayout.BeginFoldoutHeaderGroup(showVariables, "InjectVariables");
            if (showVariables)
            {
                for(int i=0;i<allVariables.Count;i++)
                {
                    int popupOption = -1;
                    var variable = allVariables[i];
                    var variableProperty = variablesPropertyVariables[i];
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel(
                        new GUIContent(variable.key)
                    );
                    int v = EditorGUILayout.Popup(popupOption, varOptions, new GUIStyle(GUI.skin.GetStyle("PaneOptions")), GUILayout.MaxWidth(20));
                    var valueField = variable.GetType().GetField("value");
                    if (IsGenericList(valueField.FieldType))
                    {
                        SerializedProperty listProperty = variableProperty.FindPropertyRelative("value");
                        var reorderableList = new ReorderableList(serializedObject, listProperty, true, false, true, true);
                        reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                        {
                            SerializedProperty element = listProperty.GetArrayElementAtIndex(index);
                            EditorGUI.PropertyField(rect, element, GUIContent.none);
                        };
                        reorderableList.DoLayoutList();
                    }
                    else
                    {
                        EditorGUILayout.PropertyField(variableProperty.FindPropertyRelative("value"), GUIContent.none,true);
                    }

                    EditorGUILayout.EndHorizontal();
                    if (EditorGUI.EndChangeCheck()) {
                        serializedObject.ApplyModifiedProperties();
                    }
                    if (v != popupOption) {
                        if (v == 0)
                        {
                            EditorGUIUtility.systemCopyBuffer = variable.key;
                        }
                        else if (v == 1)
                        {
                            DeleteVariabe(variables,variable.key);
                        }
                    }
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
        }
        
        private void DeleteVariabe(InjectVariables injectVariables, string key)
        {
            Undo.RecordObject(target, "Delete Blackboard Variable");
            FieldInfo[] fields = injectVariables.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
        
            foreach (FieldInfo field in fields)
            {
                // 检查字段类型是否为泛型List
                if (field.FieldType.IsGenericType && 
                    field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    // 获取列表的值（IList接口便于操作）
                    IList list = (IList)field.GetValue(injectVariables);
                    if (list == null) continue;

                    // 遍历列表（倒序避免索引错位）
                    for (int i = list.Count - 1; i >= 0; i--)
                    {
                        // 转换为基类获取key
                        InjectVariables.BaseInjectVariable item = (InjectVariables.BaseInjectVariable)list[i];
                        if (item.key == key)
                        {
                            list.RemoveAt(i);
                        }
                    }
                }
            }
        }
        
        public static bool IsGenericList(Type type)
        {
            
            // Debug.Log(type);
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
        }
        
        private void GetAllInjectVariables(InjectVariables injectVariables, SerializedProperty variablesProperty , out List<InjectVariables.BaseInjectVariable> allVariables, out List<SerializedProperty> variablesPropertyVariables)
        {
            allVariables = new List<InjectVariables.BaseInjectVariable>();
            variablesPropertyVariables = new List<SerializedProperty>();
            
            FieldInfo[] fields = injectVariables.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
        
            foreach (FieldInfo field in fields)
            {
                // 检查字段类型是否为泛型List
                if (field.FieldType.IsGenericType && 
                    field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    // 获取列表的值（IList接口便于操作）
                    IList list = (IList)field.GetValue(injectVariables);
                    if (list == null) continue;

                    // 遍历列表（倒序避免索引错位）
                    for (int i = 0; i < list.Count; i++)
                    {
                        // 转换为基类获取key
                        InjectVariables.BaseInjectVariable item = (InjectVariables.BaseInjectVariable)list[i];
                        allVariables.Add(item);
                    }
                    variablesPropertyVariables.AddRange(GetPropertyListByName(field.Name,variablesProperty));
                }
            }
        }

        private List<SerializedProperty> GetPropertyListByName(string propertyName,SerializedProperty variablesProperty)
        {
            var ret = new List<SerializedProperty>();
            var itemsProperty = variablesProperty.FindPropertyRelative(propertyName);
            // if (!itemsProperty.isArray) Debug.LogError("error");
            // Debug.Log(itemsProperty.arraySize);
            for (int i = 0; i < itemsProperty.arraySize; i++)
            {
                SerializedProperty item = itemsProperty.GetArrayElementAtIndex(i);
                ret.Add(item);
            }

            return ret;
        }

        private void CreateVariableAndResetInput(InjectVariables variables, SerializedProperty variablesProperty)
        {
            // Validate field. Key "None" is not allowed.
            if (string.IsNullOrEmpty(newVariableKey) || newVariableKey.Equals("None")) {
                return;
            }
            string k = new string( newVariableKey.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray() );
            this.GetAllInjectVariables(variables,variablesProperty,out List<InjectVariables.BaseInjectVariable> allVariables, out List<SerializedProperty> variablesPropertyVariables);
            foreach (var v in allVariables)
            {
                if (v.key == k)
                {
                    Debug.LogError("Variable '"+k+"' already exists.");
                    return;
                }
            }

            // Add variable
            Undo.RecordObject(target, "Create Blackboard Variable");
            switch (selectedVariableType)
            {
                case InjectVariableType.GameObject:
                    variables.gameObjectVars.Add(new InjectVariables.InjectVariable<GameObject>()
                    {
                        key = k,
                    });
                    break;
                case InjectVariableType.GameObjectList:
                    variables.gameObjectListVars.Add(new InjectVariables.InjectVariable<List<GameObject>>()
                    {
                        key = k,
                    });
                    break;
                case InjectVariableType.Int:
                    variables.intVars.Add(new InjectVariables.InjectVariable<int>()
                    {
                        key = k,
                    });
                    break;
                case InjectVariableType.Float:
                    variables.floatVars.Add(new InjectVariables.InjectVariable<float>()
                    {
                        key = k,
                    });
                    break;
                case InjectVariableType.Bool:
                    variables.boolVars.Add(new InjectVariables.InjectVariable<bool>()
                    {
                        key = k,
                    });
                    break;
                case InjectVariableType.String:
                    variables.stringVars.Add(new InjectVariables.InjectVariable<String>()
                    {
                        key = k,
                    });
                    break;
                case InjectVariableType.Vector2:
                    variables.vector2Vars.Add(new InjectVariables.InjectVariable<Vector2>()
                    {
                        key = k,
                    });
                    break;
                case InjectVariableType.Vector3:
                    variables.vector3Vars.Add(new InjectVariables.InjectVariable<Vector3>()
                    {
                        key = k,
                    });
                    break;
                case InjectVariableType.Quaternion:
                    variables.quaternionVars.Add(new InjectVariables.InjectVariable<Quaternion>()
                    {
                        key = k,
                    });
                    break;
                case InjectVariableType.BoolList:
                    variables.boolListVars.Add(new InjectVariables.InjectVariable<List<bool>>()
                    {
                        key = k,
                    });
                    break;
                case InjectVariableType.IntList:
                    variables.intListVars.Add(new InjectVariables.InjectVariable<List<int>>()
                    {
                        key = k,
                    });
                    break;
                case InjectVariableType.FloatList:
                    variables.floatListVars.Add(new InjectVariables.InjectVariable<List<float>>()
                    {
                        key = k,
                    });
                    break;
                case InjectVariableType.StringList:
                    variables.stringListVars.Add(new InjectVariables.InjectVariable<List<string>>()
                    {
                        key = k,
                    });
                    break;
                case InjectVariableType.Vector2List:
                    variables.vector2ListVars.Add(new InjectVariables.InjectVariable<List<Vector2>>()
                    {
                        key = k,
                    });
                    break;
                case InjectVariableType.Vector3List:
                    variables.vector3ListVars.Add(new InjectVariables.InjectVariable<List<Vector3>>()
                    {
                        key = k,
                    });
                    break;
                case InjectVariableType.QuaternionList:
                    variables.quaternionListVars.Add(new InjectVariables.InjectVariable<List<Quaternion>>()
                    {
                        key = k,
                    });
                    break;
            }

            newVariableKey = "";
            GUI.FocusControl("Clear");
        }
    }
}