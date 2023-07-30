using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }


    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        if (player.InputHandler.JumpInput && player.jumpCD == 0.2f)
                player.Jump();
        else player.InputHandler.UseJumpInput();
        base.LogicUpdate();
        if (AttackPressed)
        {
            stateMachine.ChangeState(player.AttackState);
        }
        if (player.GroundCheck())
        {
            if (MoveHorizontal == 0 && MoveVertical == 0)
                stateMachine.ChangeState(player.IdleState);
            else stateMachine.ChangeState(player.WalkingState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.Walk();
    }
}
