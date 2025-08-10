using kz;
using Mirror;
using UnityEngine;
using UnityEditor;


namespace kz.editor
{
    public class AddComponentAndObjectToCharacterModel : EditorWindow
    {
        private GameObject toothPrefab;
        private GameObject mouthPrefab;

        private GameObject characterPreviewPrefab;

        [MenuItem("Tools/kz/Character/Add Components and game object to character model")]
        public static void ShowWindow()
        {
            // 显示一个窗口
            GetWindow(typeof(AddComponentAndObjectToCharacterModel));
        }

        void OnGUI()
        {
            GUILayout.Label("Select Prefab to Add", EditorStyles.boldLabel);

            // 让用户选择一个预制件
            toothPrefab =
                (GameObject)EditorGUILayout.ObjectField("toothPrefab", toothPrefab, typeof(GameObject), false);
            mouthPrefab =
                (GameObject)EditorGUILayout.ObjectField("mouthPrefab", mouthPrefab, typeof(GameObject), false);
            characterPreviewPrefab = (GameObject)EditorGUILayout.ObjectField("characterPreviewPrefab",
                characterPreviewPrefab, typeof(GameObject), false);

            if (GUILayout.Button("Add Prefab to Selected GameObject"))
            {
                AddComponentsAndGameObjectToCharacterModel();
            }
        }



        private void AddComponentsAndGameObjectToCharacterModel()
        {
            if (this.toothPrefab == null)
            {
                Debug.LogError("No toothPrefab selected!");
                return;
            }

            if (this.mouthPrefab == null)
            {
                Debug.LogError("No mouthPrefab selected!");
                return;
            }

            foreach (GameObject obj in Selection.gameObjects)
            {
                if (obj == null) continue;

                obj.layer = LayerMask.NameToLayer("Player");

                var rb = ForceAddComponent<Rigidbody>(obj);
                rb.mass = 30;
                rb.freezeRotation = true;


                ForceAddComponent<NetworkIdentity>(obj);

                var nrbu = ForceAddComponent<BittenNetworkRigidbodyReliable>(obj);
                nrbu.syncDirection = SyncDirection.ClientToServer;
                // nrbu.syncScale = true;
                nrbu.coordinateSpace = CoordinateSpace.World;

                var nAnim = ForceAddComponent<NetworkAnimator>(obj);
                nAnim.clientAuthority = true;
                nAnim.animator = obj.GetComponent<Animator>();
                nAnim.syncDirection = SyncDirection.ClientToServer;

                // 这个每一个角色都不一样，自己要调
                AddComponentIfNotExists<CapsuleCollider>(obj);

                var charCore = ForceAddComponent<CharacterCore>(obj);
                charCore.characterPreviewPrefab = this.characterPreviewPrefab;
                ForceAddComponent<CharacterAnimator>(obj);
                ForceAddComponent<CharacterDieAction>(obj);
                
                ForceAddComponent<CharacterSwimBehaviour>(obj);
                ForceAddComponent<CharacterInput>(obj);

                // AI
                ForceAddComponent<BoidsCore>(obj);
                ForceAddComponent<AIControllerCore>(obj);

                // GrowCore
                var growCore = ForceAddComponent<GrowCore>(obj);

                ForceAddComponent<SkillBuffCore>(obj);

                // 添加一个牙齿part
                RemoveGameObjectIfExists(obj, this.toothPrefab.name);
                GameObject toothInstance = (GameObject)PrefabUtility.InstantiatePrefab(this.toothPrefab);
                toothInstance.transform.SetParent(obj.transform, false);
                var toothScript = toothInstance.GetComponent<Tooth>();
                toothScript.characterCore = charCore;

                // 添加一个嘴巴
                RemoveGameObjectIfExists(obj, this.mouthPrefab.name);
                GameObject mouthInstance = (GameObject)PrefabUtility.InstantiatePrefab(this.mouthPrefab);
                mouthInstance.transform.SetParent(obj.transform, false);
                var mouthScript = mouthInstance.GetComponent<Mouth>();
                mouthScript.characterCore = charCore;
                
                charCore.tooth = toothInstance.GetComponent<Tooth>();
                charCore.mouth = mouthInstance.GetComponent<Mouth>();
            }
        }

        private static GameObject CreateEmpty(string name)
        {
            GameObject emptyObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            DestroyImmediate(emptyObject.GetComponent<MeshRenderer>());
            DestroyImmediate(emptyObject.GetComponent<Collider>());
            emptyObject.name = name;
            return emptyObject;
        }

        private static T AddComponentIfNotExists<T>(GameObject obj) where T : Component
        {
            var c = obj.GetComponent<T>();
            if (c != null) return null;
            return obj.AddComponent<T>();
        }

        private static T ForceAddComponent<T>(GameObject obj) where T : Component
        {
            var c = obj.GetComponent<T>();
            if (c != null)
            {
                // 在编辑模式下，使用DestroyImmediate来移除组件
                DestroyImmediate(c);
            }

            return obj.AddComponent<T>();
        }

        private static void RemoveGameObjectIfExists(GameObject obj, string name)
        {
            var childObject = obj.transform.Find(name);
            if (childObject != null)
            {
                DestroyImmediate(childObject.gameObject);
            }
        }
    }
}
