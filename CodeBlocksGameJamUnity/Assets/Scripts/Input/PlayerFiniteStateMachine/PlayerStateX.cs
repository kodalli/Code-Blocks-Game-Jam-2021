using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateX
{
    protected PlayerX player;
    protected PlayerStateMachineX stateMachine;
    protected PlayerData playerData;

    protected float startTime;

    private string animBoolName;

    public PlayerStateX(PlayerX player, PlayerStateMachineX stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        player.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
    }
    public virtual void Exit()
    {
        player.Anim.SetBool(animBoolName, false);
    }
    public virtual void LogicUpdate()
    {
    }
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    public virtual void DoChecks()
    {
    }
}
