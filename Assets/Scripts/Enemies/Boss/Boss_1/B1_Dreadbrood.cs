using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_Dreadbrood : Entity
{
    public B1_IdleState idleState { get; private set; }
    public B1_MoveState moveState { get; private set; }
    public B1_PlayerDetectedState playerDetectedState { get; private set; }
    public B1_LookForPlayerState lookForPlayerState { get; private set; }
    public B1_MeleeAttackState meleeAttackState { get; private set; }
    public B1_RangeAttackState rangeAttackState { get; private set; }
    public B1_DeadState deadState { get; private set; }

    [Header("State Data")]
    [Space]
    [SerializeField] D_IdleState idleStateData;
    [SerializeField] D_MoveState moveStateData;
    [SerializeField] D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] D_LookForPlayerState lookForPlayerStateData;
    [SerializeField] D_MeleeAttack meleeAttackStateData;
    [SerializeField] D_RangeAttackState rangeAttackStateData;
    [SerializeField] D_DeadState deadStateData;

    [Header("Attack")]
    [Space]
    [SerializeField] private Transform meleeAttackPosition;
    [SerializeField] private Transform rangeAttackPosition;

    public override void Start()
    {
        base.Start();

        idleState = new B1_IdleState(stateMachine, this, "idle", idleStateData, this);
        moveState = new B1_MoveState(stateMachine, this, "move", moveStateData, this);
        playerDetectedState = new B1_PlayerDetectedState(stateMachine, this, "playerDetected", playerDetectedStateData, this);
        lookForPlayerState = new B1_LookForPlayerState(stateMachine, this, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new B1_MeleeAttackState(stateMachine, this, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        rangeAttackState = new B1_RangeAttackState(stateMachine, this, "rangeAttack", rangeAttackPosition, rangeAttackStateData, this);
        deadState = new B1_DeadState(stateMachine, this, "dead", deadStateData, this);

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

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }

    private IEnumerator WaitToDisable()
    {
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }
}
