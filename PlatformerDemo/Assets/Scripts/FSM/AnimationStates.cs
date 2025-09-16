namespace AnimationFSM
{
    public class JumpState : State
    {
        public JumpState(string animationName) : base(animationName) { }
        public override bool IsMatchingConditions(Conditions conditions)
        {
            return !conditions.isOnGround;
        }
    }

    public class WalkState : State
    {
        public WalkState(string animationName) : base(animationName) { }
        public override bool IsMatchingConditions(Conditions conditions)
        {
            return conditions.isOnGround && conditions.movingX;
        }
    }

    public class IdleState : State
    {
        public IdleState(string animationName) : base(animationName) { }
        public override bool IsMatchingConditions(Conditions conditions)
        {
            return true;
        }
    }
}
