using MBT;


namespace kz
{
    [MBTNode("fishAI/CommonAttackAIAction")]
    public class CommonAttackAIAction:Leaf
    {
        public GameObjectReference enemyToAttack = new GameObjectReference(VarRefMode.DisableConstant);
        
        public override NodeResult Execute()
        {
            return NodeResult.success;
        }
    }
}