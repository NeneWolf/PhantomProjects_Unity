using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackState : AttackState
{
    protected D_RangeAttackState stateData;

    protected GameObject projectile;
    protected Projectile projectileScript;

    protected GameObject lazer;
    protected bool useLazer;


    public RangeAttackState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, Transform attackPosition, D_RangeAttackState stateData) : base(stateMachine, entity, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        this.useLazer = stateData.useLazer;
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        if (useLazer == false)
        {
            projectile = GameObject.Instantiate(stateData.projectile, attackPosition.position, attackPosition.rotation);
            projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.FireProjectile(stateData.projectileSpeed, stateData.projectileDamage);
        }
        else 
        {
            lazer = GameObject.Instantiate(stateData.lazer, attackPosition.position, attackPosition.rotation);
        }
    }
}
