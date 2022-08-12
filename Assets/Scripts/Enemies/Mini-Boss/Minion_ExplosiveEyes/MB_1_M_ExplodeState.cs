using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_1_M_ExplodeState : ExplodeState
{
    protected MB_1_Minions mBMinions;

    public MB_1_M_ExplodeState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, Transform attackPosition, D_ExplodeState stateData, MB_1_Minions mBMinions) : base(stateMachine, entity, animBoolName, attackPosition, stateData)
    {
        this.mBMinions = mBMinions;
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
            if(entity.hasBeenDamage == true)
            {
                entity.GiveRewards(entity.mutationPoints);
            }

            mBMinions.gameObject.SetActive(false);
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
