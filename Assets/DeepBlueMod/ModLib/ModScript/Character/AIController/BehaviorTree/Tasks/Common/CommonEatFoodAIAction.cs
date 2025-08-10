using MBT;

namespace kz
{
    [MBTNode("fishAI/CommonEatFoodAIAction")]
    public class CommonEatFoodAIAction:Leaf
    {
        public GameObjectReference foodTarget = new GameObjectReference(VarRefMode.DisableConstant);
        public BoolReference spiltFood;
        
        public override NodeResult Execute()
        {
            return NodeResult.success;
        }
    }
}