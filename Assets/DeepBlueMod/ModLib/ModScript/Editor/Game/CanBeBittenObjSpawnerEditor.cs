using UnityEditor;
using kz;
using UnityEditorInternal;
using UnityEngine;
using UnityEditor.IMGUI.Controls;

namespace kz.editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(CanBeBittenObjSpawner))]
    public class CanBeBittenObjSpawnerEditor:Editor
    {
        private static Color _handleColor = new Color(127f, 214f, 244f, 100f) / 255;
        private static Color _handleColorSelected = new Color(127f, 214f, 244f, 210f) / 255;
        private static Color _handleColorDisabled = new Color(127f * 0.75f, 214f * 0.75f, 244f * 0.75f, 100f) / 255;
        
        private BoxBoundsHandle _boundsHandle = new BoxBoundsHandle();
        
        private bool EditingCollider => EditMode.editMode == EditMode.SceneViewEditMode.Collider && EditMode.IsOwner(this);
        
        public override void OnInspectorGUI()
        {
            CanBeBittenObjSpawner canBeBittenObjSpawner = (CanBeBittenObjSpawner)target;
            
            EditMode.DoEditModeInspectorModeButton(EditMode.SceneViewEditMode.Collider, "Edit Bounds",
                EditorGUIUtility.IconContent("EditCollider"), GetBounds(), this);
            
            //继承基类方法
            base.OnInspectorGUI();

            //获取要执行方法的类
            
        }
        
        protected Bounds GetBounds() => ((CanBeBittenObjSpawner) target).spawnerBounds;
        
        [DrawGizmo(GizmoType.Selected | GizmoType.Active | GizmoType.Pickable)]
        private static void RenderBoxGizmoSelected(CanBeBittenObjSpawner volume, GizmoType gizmoType)
        {
            // Draw the bounds editor gizmo.
            RenderBoxGizmo(volume, gizmoType, true);
        }

        [DrawGizmo(GizmoType.NotInSelectionHierarchy | GizmoType.Pickable)]
        private static void RenderBoxGizmoNotSelected(CanBeBittenObjSpawner volume, GizmoType gizmoType)
        {
            RenderBoxGizmo(volume, gizmoType, false);
        }
        
        // Draw the bounds editor gizmo.
        private static void RenderBoxGizmo(CanBeBittenObjSpawner volume, GizmoType gizmoType, bool selected)
        {
            Color color = selected ? _handleColorSelected : _handleColor;
            if (!volume.enabled)
                color = _handleColorDisabled;

            Color oldColor = Gizmos.color;
            Matrix4x4 oldMatrix = Gizmos.matrix;

            // Use the unscaled matrix for the NavMeshSurface
            Matrix4x4 localToWorld = Matrix4x4.TRS(volume.transform.position, volume.transform.rotation, Vector3.one);
            Gizmos.matrix = localToWorld;

            Bounds bounds = volume.spawnerBounds;
            
            // Draw wireframe bounds.
            Gizmos.color = color;
            Gizmos.DrawWireCube(bounds.center, bounds.size);

            // If selected, draw filled bounds.
            if (selected && volume.enabled)
            {
                Color colorTrans = new Color(color.r * 0.75f, color.g * 0.75f, color.b * 0.75f, color.a * 0.15f);
                Gizmos.color = colorTrans;
                Gizmos.DrawCube(bounds.center, bounds.size);
            }
            
            Gizmos.matrix = oldMatrix;
            Gizmos.color = oldColor;
        }

        protected void OnSceneGUI()
        {
            CanBeBittenObjSpawner volume = (CanBeBittenObjSpawner)target;
            // Draw collider editing handle.
            if (EditingCollider)
            {
                Bounds bounds = volume.spawnerBounds;
                Color color = volume.enabled ? _handleColor : _handleColorDisabled;
                Matrix4x4 localToWorld =
                    Matrix4x4.TRS(volume.transform.position, volume.transform.rotation, Vector3.one);
                using (new Handles.DrawingScope(color, localToWorld))
                {
                    _boundsHandle.center = bounds.center;
                    _boundsHandle.size = bounds.size;

                    EditorGUI.BeginChangeCheck();
                    _boundsHandle.DrawHandle();
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(volume, "Modified");
                        Vector3 center = _boundsHandle.center;
                        Vector3 size = _boundsHandle.size;
                        bounds.center = center;
                        bounds.size = size;
                        volume.spawnerBounds = bounds;
                        EditorUtility.SetDirty(target);
                    }
                }
            }
        }
    }
}