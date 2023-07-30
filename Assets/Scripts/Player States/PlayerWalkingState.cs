
using UnityEngine;

public class PlayerWalkingState : PlayerState
{
    public PlayerWalkingState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        base.LogicUpdate();
        if (player.InputHandler.JumpInput && player.jumpCD == 0.2f)
            player.Jump();
        if (MoveHorizontal == 0 && MoveVertical == 0)
            stateMachine.ChangeState(player.IdleState);
        if (AttackPressed)
        {
            stateMachine.ChangeState(player.AttackState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.Walk();
        if (!player.GroundCheck())
            stateMachine.ChangeState(player.AirState);
    }
}
