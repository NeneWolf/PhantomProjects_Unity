using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_1_M_PlayerDetectedState : PlayerDetectedState
{
    protected MB_1_Minions mBMinions;

    public MB_1_M_PlayerDetectedState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, D_PlayerDetectedState stateData, MB_1_Minions mBMinions) : base(stateMachine, entity, animBoolName, stateData)
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
            stateMachine.ChangeState(mBMinions.meleeAttackState);
        }
        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(mBMinions.chargeState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(mBMinions.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
