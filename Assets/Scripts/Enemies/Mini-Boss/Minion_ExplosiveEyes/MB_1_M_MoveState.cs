using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_1_M_MoveState : MoveState
{
    protected MB_1_Minions mBMinions;

    public MB_1_M_MoveState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, D_MoveState stateData, MB_1_Minions mBMinions) : base(stateMachine, entity, animBoolName, stateData)
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

        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(mBMinions.playerDetectedState);
        }
        else if (isDetectingWall || !isDetectingLedge)
        {
            mBMinions.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(mBMinions.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
