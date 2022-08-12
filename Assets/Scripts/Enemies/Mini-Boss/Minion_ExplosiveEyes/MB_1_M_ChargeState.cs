using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_1_M_ChargeState : ChargeState
{
    protected MB_1_Minions mBMinions;

    public MB_1_M_ChargeState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, D_ChargeState stateData, MB_1_Minions mBMinions) : base(stateMachine, entity, animBoolName, stateData)
    {
        this.mBMinions = mBMinions;
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
            var random = Random.Range(-1f, 1f);

            if (random <= 0)
                stateMachine.ChangeState(mBMinions.meleeAttackState);
            else if(random > 0)
                stateMachine.ChangeState(mBMinions.explodeState);
        }
        else if (!isDetectingLedge || isDetectingWall)
        {
            stateMachine.ChangeState(mBMinions.lookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            if (isPlayerMinAgroRange)
            {
                stateMachine.ChangeState(mBMinions.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(mBMinions.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
