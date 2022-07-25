using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity
{
    public E2_MoveState moveState { get; private set; }
    public E2_IdleState idleState { get; private set; }
    public E2_PlayerDetectedState playerDetectedState { get; private set; }
    public E2_MeleeAttackState meleeAttackState { get; private set; }
    public E2_LookForPlayerState lookForPlayerState { get; private set; }
    public E2_DeadState deadState { get; private set; }
    public E2_DodgeState dodgeState { get; private set; }   
    public E2_RangeAttackState rangeAttackState { get; private set; }   

    [Header("State Data")]
    [Space]
    [SerializeField] D_MoveState moveStateData;
    [SerializeField] D_IdleState idleStateData;
    [SerializeField] D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] D_MeleeAttack meleeAttackStateData;
    [SerializeField] D_LookForPlayerState lookForPlayerStateData;
    [SerializeField] D_DeadState deadStateData;
    [SerializeField] public D_DodgeState dodgeStateData;
    [SerializeField] D_RangeAttackState RangeAttackStateData;

    [Header("Attacks")]
    [Space]
    [SerializeField] private Transform meleeAttackPosition;
    [SerializeField] private Transform RangeAttackPosition;

    public override void Start()
    {
        base.Start();

        moveState = new E2_MoveState(stateMachine, this, "move", moveStateData, this);
        idleState = new E2_IdleState(stateMachine, this, "idle", idleStateData, this);
        playerDetectedState = new E2_PlayerDetectedState(stateMachine,this,"playerDetected",playerDetectedStateData, this);
        meleeAttackState = new E2_MeleeAttackState(stateMachine, this, "meleeAttack", meleeAttackPosition,meleeAttackStateData, this);
        lookForPlayerState = new E2_LookForPlayerState(stateMachine,this,"lookForPlayer", lookForPlayerStateData, this);
        deadState = new E2_DeadState(stateMachine, this, "dead", deadStateData, this);
        dodgeState = new E2_DodgeState(stateMachine, this, "dodge", dodgeStateData, this);
        rangeAttackState = new E2_RangeAttackState(stateMachine, this, "rangeAttack", RangeAttackPosition, RangeAttackStateData, this);

        stateMachine.Initialize(moveState);
    }

    public override void Damage(float dmg)
    {
        base.Damage(dmg);

        if (isDead)
        {
            stateMachine.ChangeState(deadState);
            StartCoroutine("WaitToDisable");
        }
        else if (CheckPlayerInMinAgroRange())
        {
            stateMachine.ChangeState(rangeAttackState);
        }
        else if (!CheckPlayerInMinAgroRange())
        {
            lookForPlayerState.SetTurnImmediatly(true);
            stateMachine.ChangeState(lookForPlayerState);
        }
    }

    private IEnumerator WaitToDisable()
    {
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
