using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    //Different States

    public E1_IdleState idleState { get; private set; }
    public E1_MoveState moveState { get; private set; }
    public E1_PlayerDetectedState playerDetectedState { get; private set; }
    public E1_ChargeState chargeState { get; private set; }
    public E1_LookForPlayerState lookForPlayerState { get; private set; }
    public E1_MeleeAttackState meleeAttackState { get; private set; }
    public E1_DeadState deadState { get; private set; }

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

        moveState = new E1_MoveState(stateMachine,this,"move", moveStateData, this);
        idleState = new E1_IdleState(stateMachine, this, "idle", idleStateData, this);
        playerDetectedState = new E1_PlayerDetectedState(stateMachine, this, "playerDetected", playerDetectedStateData, this);
        chargeState = new E1_ChargeState(stateMachine, this, "charge", chargeStateData, this);
        lookForPlayerState = new E1_LookForPlayerState(stateMachine,this, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new E1_MeleeAttackState(stateMachine, this, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        deadState = new E1_DeadState(stateMachine, this, "dead", deadStateData, this);

        stateMachine.Initialize(moveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }

    public override void Damage(float dmg)
    {
        base.Damage(dmg);

        if (isDead)
        {
            stateMachine.ChangeState(deadState);
            StartCoroutine("WaitToDisable");
        }
    }

    private IEnumerator WaitToDisable()
    {
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }
}
