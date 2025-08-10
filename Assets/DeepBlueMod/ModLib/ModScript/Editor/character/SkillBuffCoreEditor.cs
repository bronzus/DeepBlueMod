using UnityEditor;

namespace kz.editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(SkillBuffCore))]
    public class SkillBuffCoreEditor:InjectVariablesBaseEditor
    {
        public override void OnInspectorGUI()
        {
            SkillBuffCore skillBuffCore = (SkillBuffCore)target;
            SerializedObject serializedObject = new SerializedObject(target);
            
            this.SetInjectVariablesUI(serializedObject, skillBuffCore.injectVariables, serializedObject.FindProperty("injectVariables"));

            base.OnInspectorGUI();
        }
    }
}