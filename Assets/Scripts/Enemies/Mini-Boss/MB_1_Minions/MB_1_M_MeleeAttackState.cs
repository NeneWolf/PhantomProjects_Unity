using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_1_M_MeleeAttackState : MeleeAttackState
{
    protected MB_1_Minions mBMinions;

    public MB_1_M_MeleeAttackState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, Transform attackPosition, D_MeleeAttack stateData, MB_1_Minions mBMinions) : base(stateMachine, entity, animBoolName, attackPosition, stateData)
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
                stateMachine.ChangeState(mBMinions.playerDetectedState);
            }
            else if (!isPlayerInMinAgroRange)
                stateMachine.ChangeState(mBMinions.lookForPlayerState);
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
