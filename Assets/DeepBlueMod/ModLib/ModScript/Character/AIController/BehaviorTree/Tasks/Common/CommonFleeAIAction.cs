using MBT;

namespace kz
{
    [MBTNode("fishAI/CommonFleeAIAction")]
    public class CommonFleeAIAction:Leaf
    {
        // public CharacterCore enemy;
        public GameObjectReference enemyToFlee = new GameObjectReference(VarRefMode.DisableConstant);
        
        public override NodeResult Execute()
        {
            return NodeResult.success;
        }
    }
}