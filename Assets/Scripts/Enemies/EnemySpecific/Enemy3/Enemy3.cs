using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Entity
{
    //Different States

    public E3_IdleState idleState { get; private set; }
    public E3_MoveState moveState { get; private set; }
    public E3_PlayerDetectedState playerDetectedState { get; private set; }
    public E3_ChargeState chargeState { get; private set; }
    public E3_LookForPlayerState lookForPlayerState { get; private set; }
    public E3_MeleeAttackState meleeAttackState { get; private set; }
    public E3_DeadState deadState { get; private set; }

    [Header("State Data")]
    [Space]
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] private D_ChargeState chargeStateData;
    [SerializeField] private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField] private D_MeleeAttack meleeAttackStateData;
    [SerializeField] private D_DeadState deadStateData;

    [Header("Attacks")]
    [Space]
    [SerializeField] private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();

        dropPondDead = true;

        moveState = new E3_MoveState(stateMachine, this, "move", moveStateData, this);
        idleState = new E3_IdleState(stateMachine, this, "idle", idleStateData, this);
        playerDetectedState = new E3_PlayerDetectedState(stateMachine, this, "playerDetected", playerDetectedStateData, this);
        chargeState = new E3_ChargeState(stateMachine, this, "charge", chargeStateData, this);
        lookForPlayerState = new E3_LookForPlayerState(stateMachine, this, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new E3_MeleeAttackState(stateMachine, this, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        deadState = new E3_DeadState(stateMachine, this, "dead", deadStateData, this);

        stateMachine.Initialize(moveState);
    }

    public override void Damage(float dmg)
    {
        base.Damage(dmg);

        if (isDead)
        {
            rb2d.gravityScale = 1f;
            stateMachine.ChangeState(deadState);
            StartCoroutine("WaitToDisable");
        }
        else if (!CheckPlayerInMinAgroRange())
        {
            lookForPlayerState.SetTurnImmediatly(true);
            stateMachine.ChangeState(lookForPlayerState);
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }

    private IEnumerator WaitToDisable()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
