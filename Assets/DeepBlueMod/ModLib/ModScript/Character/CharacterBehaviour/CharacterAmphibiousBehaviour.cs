using UnityEngine;

namespace kz
{
    public class CharacterAmphibiousBehaviour:CharacterBehaviour
    {
        public bool changeColliderShape = false;
        public Mesh walkColliderMesh;
        public Mesh swimColliderMesh;
    }
}