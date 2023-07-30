using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }


    public override void EnterState()
    {
        base.EnterState();
        player.ResetVelocity();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.InputHandler.JumpInput && player.jumpCD == 0.2f)
            player.Jump();
        if (MoveHorizontal != 0 || MoveVertical != 0)
            stateMachine.ChangeState(player.WalkingState);
        if (AttackPressed)
        {
            stateMachine.ChangeState(player.AttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
