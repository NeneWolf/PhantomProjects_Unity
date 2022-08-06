using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2_IdleState : IdleState
{
    protected B2_Fetiddeviation boss;

    public B2_IdleState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, D_IdleState stateData, B2_Fetiddeviation boss) : base(stateMachine, entity, animBoolName, stateData)
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

        if (isPlayerInMaxAgroRange || isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(boss.playerDetectedState);
        }
        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(boss.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
