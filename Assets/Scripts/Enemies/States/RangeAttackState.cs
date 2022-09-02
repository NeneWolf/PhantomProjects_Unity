using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackState : AttackState
{
    protected D_RangeAttackState stateData;

    //Projectile
    protected GameObject projectile;
    protected Projectile projectileScript;

    float bulletDamage;

    //Lazer
    protected GameObject laser;

    //Fireball
    protected GameObject fireBall;

    public RangeAttackState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, Transform attackPosition, D_RangeAttackState stateData) : base(stateMachine, entity, animBoolName, attackPosition)
    {
        this.stateData = stateData;
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        if (stateData.useProjectile)
        {
            projectile = GameObject.Instantiate(stateData.projectile, attackPosition.position, attackPosition.rotation);
            projectileScript = projectile.GetComponent<Projectile>();
            bulletDamage = stateData.projectileDamage * entity.difficulty;
            projectileScript.FireProjectile(stateData.projectileSpeed, bulletDamage);
        }
        else if(stateData.useLaser)
        {
            laser = GameObject.Instantiate(stateData.laser, attackPosition.position, attackPosition.rotation);
        }
        else if (stateData.useFireBall)
        {
            fireBall = GameObject.Instantiate(stateData.fireBall, 
                new Vector3(Random.Range(attackPosition.position.x - stateData.minRange, 
                attackPosition.position.x + stateData.maxRange), Random.Range(attackPosition.position.y - 0.5f, attackPosition.position.y + 0.5f), 0f), 
                attackPosition.rotation);

            fireBall = GameObject.Instantiate(stateData.fireBall,
                new Vector3(Random.Range(attackPosition.position.x - stateData.minRange,
                attackPosition.position.x + stateData.maxRange), Random.Range(attackPosition.position.y - 0.5f, attackPosition.position.y + 0.5f), 0f),
                attackPosition.rotation);
        }
    }
}
