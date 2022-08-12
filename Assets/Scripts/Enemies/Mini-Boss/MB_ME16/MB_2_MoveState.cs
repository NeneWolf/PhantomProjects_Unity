using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_2_MoveState : MoveState
{
    protected Mini_Boss_ME16 mBoss;

    public MB_2_MoveState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, D_MoveState stateData, Mini_Boss_ME16 mBoss) : base(stateMachine, entity, animBoolName, stateData)
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

        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(mBoss.playerDetectedState);
        }
        else if (isDetectingWall || !isDetectingLedge)
        {
            mBoss.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(mBoss.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
