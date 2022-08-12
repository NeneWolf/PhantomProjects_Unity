using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_1_M_DeadState : DeadState
{
    protected MB_1_Minions mBMinions;

    public MB_1_M_DeadState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, D_DeadState stateData, MB_1_Minions mBMinions) : base(stateMachine, entity, animBoolName, stateData)
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

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
