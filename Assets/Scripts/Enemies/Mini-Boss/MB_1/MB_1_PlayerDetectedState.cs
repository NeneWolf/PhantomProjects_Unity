using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_1_PlayerDetectedState : PlayerDetectedState
{
    protected Mini_Boss_Eye mBoss;

    public MB_1_PlayerDetectedState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, D_PlayerDetectedState stateData, Mini_Boss_Eye mBoss) : base(stateMachine, entity, animBoolName, stateData)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
