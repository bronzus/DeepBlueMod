using UnityEngine;
using Mirror;

namespace kz
{
    public class BoidsManager : MonoBehaviour
    {
        public enum BoidsType
        {
            Boids,
            OnlyAvoid
        }
        
        public BoidsType boidsType;

        public float initialPerceptionRadius = 3.5f;
        public float initialAvoidanceRadius = 2f;
    }
}