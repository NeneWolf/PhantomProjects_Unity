using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_2_PlayerDetectedState : PlayerDetectedState
{
    protected Mini_Boss_ME16 mBoss;

    public MB_2_PlayerDetectedState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, D_PlayerDetectedState stateData, Mini_Boss_ME16 mBoss) : base(stateMachine, entity, animBoolName, stateData)
    {
        this.mBoss = mBoss;
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

        if (performLongRangeAction)
        {
            stateMachine.ChangeState(mBoss.rangeAttackState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(mBoss.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
