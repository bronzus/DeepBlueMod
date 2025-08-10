using Mirror;

namespace kz
{
    public class AttackResult
    {
        public bool success;
        public bool die;
    }

    public enum BiteType
    {
        PowerfulBite,
        WeakBite,
        PowerfulCatch,
        WeakCatch
    }

    public class BittenBehaviour: NetworkBehaviour
    {
    }
}