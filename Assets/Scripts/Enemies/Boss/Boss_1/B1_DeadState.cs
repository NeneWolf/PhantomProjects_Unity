using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_DeadState : DeadState
{
    protected B1_Dreadbrood boss;

    public B1_DeadState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, D_DeadState stateData, B1_Dreadbrood boss) : base(stateMachine, entity, animBoolName, stateData)
    {
        this.boss = boss;
    }

    public override void DoChecks()
    {
        base.DoChecks();
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
