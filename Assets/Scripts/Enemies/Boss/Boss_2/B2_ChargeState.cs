using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2_ChargeState : ChargeState
{
    protected B2_Fetiddeviation boss;

    public B2_ChargeState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, D_ChargeState stateData, B2_Fetiddeviation boss) : base(stateMachine, entity, animBoolName, stateData)
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
        else if (!isDetectingLedge || isDetectingWall)
        {
            stateMachine.ChangeState(boss.lookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            if (isPlayerMinAgroRange)
            {
                stateMachine.ChangeState(boss.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(boss.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
