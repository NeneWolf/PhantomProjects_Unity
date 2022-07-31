using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackState : AttackState
{
    protected D_RangeAttackState stateData;

    //Projectile
    protected bool useProjectile;
    protected GameObject projectile;
    protected Projectile projectileScript;

    //Lazer
    protected GameObject lazer;
    protected bool useLazer;

    //Fireball
    protected GameObject fireBall;
    protected bool useFireBall;
    protected float minRange;
    protected float maxRange;

    public RangeAttackState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, Transform attackPosition, D_RangeAttackState stateData) : base(stateMachine, entity, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        this.useProjectile = stateData.useProjectile;
        this.useLazer = stateData.useLazer;
        this.useFireBall = stateData.useFireBall;
        this.minRange = stateData.minRange;
        this.maxRange = stateData.maxRange;
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

        if (useProjectile)
        {
            projectile = GameObject.Instantiate(stateData.projectile, attackPosition.position, attackPosition.rotation);
            projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.FireProjectile(stateData.projectileSpeed, stateData.projectileDamage);
        }
        else if(useLazer)
        {
            lazer = GameObject.Instantiate(stateData.lazer, attackPosition.position, attackPosition.rotation);
        }
        else if (useFireBall)
        {
            fireBall = GameObject.Instantiate(stateData.fireBall, 
                new Vector3(Random.Range(attackPosition.position.x - minRange, 
                attackPosition.position.x + maxRange), Random.Range(attackPosition.position.y - 0.5f, attackPosition.position.y + 0.5f), 0f), 
                attackPosition.rotation);
        }
    }
}
