using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_IdleState : IdleState
{
    protected B1_Dreadbrood boss;

    public B1_IdleState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, D_IdleState stateData, B1_Dreadbrood boss) : base(stateMachine, entity, animBoolName, stateData)
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

        if (isPlayerInMinAgroRange)
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
