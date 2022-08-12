using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_2_DeadState : DeadState
{
    protected Mini_Boss_ME16 mBoss;

    public MB_2_DeadState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, D_DeadState stateData, Mini_Boss_ME16 mBoss) : base(stateMachine, entity, animBoolName, stateData)
    {
        this.mBoss = mBoss;
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
