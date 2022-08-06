using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2_DeadState : DeadState
{
    protected B2_Fetiddeviation boss;

    public B2_DeadState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, D_DeadState stateData, B2_Fetiddeviation boss) : base(stateMachine, entity, animBoolName, stateData)
    {
        this.boss = boss;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
