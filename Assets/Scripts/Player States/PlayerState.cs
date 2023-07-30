using UnityEngine;

public abstract class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    
    protected float startTime;    //time when entered state

    protected float MoveHorizontal, MoveVertical;
    protected bool JumpPressed, AttackPressed, AimPressed;

    private string animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        this.playerData = playerData;
    }

    public virtual void EnterState()
    {
        //player.Anim.CrossFade(animBoolName,0);
        player.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        Debug.Log(animBoolName);
    }

    public virtual void ExitState()
    {
        player.Anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {
        MoveHorizontal = player.InputHandler.MovementInput.x;
        MoveVertical = player.InputHandler.MovementInput.y;
        JumpPressed = player.InputHandler.AttackInput;
        AttackPressed = player.InputHandler.AttackInput;
        AimPressed = player.InputHandler.AimInput;
        if (stateMachine.CurrentState == player.IdleState || stateMachine.CurrentState == player.WalkingState
            || stateMachine.CurrentState == player.AirState)
            if (AimPressed)
            {
                if (player.Anim.GetLayerWeight(1) < 1)
                    player.Anim.SetLayerWeight(1, player.Anim.GetLayerWeight(1) + 0.05f);
                //player.Anim.SetLayerWeight(1, 1);
            }
            else
            {
                if (player.Anim.GetLayerWeight(1) > 0)
                    player.Anim.SetLayerWeight(1, player.Anim.GetLayerWeight(1) - 0.1f);
                //player.Anim.SetLayerWeight(1, 0);
            }
        //ShootPressed = player.InputHandler.RotorActive;
    }

    public virtual void PhysicsUpdate()
    {
        //Debug.Log(player.GroundCheck());
        player.Anim.SetBool("grounded", player.GroundCheck());
        if (!player.GroundCheck())
        {
            if (AimPressed)
                player.MyRB.AddForce(-Vector3.up * 15);
            else
                player.MyRB.AddForce(-Vector3.up);
            if (AttackPressed)
                player.InputHandler.UseAttackInput();
        }
        else
        {
            if (player.numberOfJumps != 1)
                player.numberOfJumps = 1;
            if (player.numberOfDashes != 1)
                player.numberOfDashes = 1;

        }
    }


}
