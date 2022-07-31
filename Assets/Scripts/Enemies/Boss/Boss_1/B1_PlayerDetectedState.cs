using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_PlayerDetectedState : PlayerDetectedState
{
    protected B1_Dreadbrood boss;

    public B1_PlayerDetectedState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, D_PlayerDetectedState stateData, B1_Dreadbrood boss) : base(stateMachine, entity, animBoolName, stateData)
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

        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(boss.meleeAttackState);
        }
        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(boss.rangeAttackState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(boss.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
