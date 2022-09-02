using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_DodgeState : DodgeState
{
    private Enemy2 enemy;

    public E2_DodgeState(FiniteStateMachine stateMachine,Entity entity,string animBoolName, D_DodgeState stateData, Enemy2 enemy) : base(stateMachine, entity, animBoolName, stateData)
    {
        this.enemy = enemy;
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

        if (isDodgeOver)
        {
            //if(isPlayerInMaxAgroRange && performCloseRangeAction)
            //{
            //    stateMachine.ChangeState(enemy.meleeAttackState);
            //}
             if (isPlayerInMaxAgroRange && !performCloseRangeAction)
            {
                stateMachine.ChangeState(enemy.rangeAttackState);
            }
            else if (!isPlayerInMaxAgroRange)
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }

            //TODO:RangeAttack State
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
