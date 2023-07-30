using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }


    public override void EnterState()
    {
        base.EnterState();
        player.ResetVelocity();
        player.AnimUmbrella.SetBool("hold", AimPressed);

        //player.ResetVelocity();
    }

    public override void ExitState()
    {
        player.AnimUmbrella.SetBool("attack", false);
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!AttackPressed)
            if (player.GroundCheck())
            {
                if (MoveHorizontal == 0 && MoveVertical == 0)
                    stateMachine.ChangeState(player.IdleState);
                else stateMachine.ChangeState(player.WalkingState);
            }
            else
                stateMachine.ChangeState(player.AirState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        }
}
