using UnityEngine;

public class PlayerDoubleJumpState : PlayerState
{
    public PlayerDoubleJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }


    public override void EnterState()
    {
        player.Jump();
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!player.GroundCheck())
        {
            stateMachine.ChangeState(player.AirState);
        }
        else
        {
            if (MoveHorizontal == 0 && MoveVertical == 0)
                stateMachine.ChangeState(player.IdleState);
            else stateMachine.ChangeState(player.WalkingState);
        } 
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
