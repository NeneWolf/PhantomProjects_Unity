using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_2_RangeAttackState : RangeAttackState
{
    protected Mini_Boss_ME16 mBoss;

    public MB_2_RangeAttackState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, Transform attackPosition, D_RangeAttackState stateData, Mini_Boss_ME16 mBoss) : base(stateMachine, entity, animBoolName, attackPosition, stateData)
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinish)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(mBoss.playerDetectedState);
            }
            else
                stateMachine.ChangeState(mBoss.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
