using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_Boss_ME16 : Entity
{
    public MB_2_MoveState moveState { get; private set; }
    public MB_2_IdleState idleState { get; private set; }
    public MB_2_PlayerDetectedState playerDetectedState { get; private set; }
    public MB_2_LookForPlayerState lookForPlayerState { get; private set; }
    public MB_2_DeadState deadState { get; private set; }
    public MB_2_RangeAttackState rangeAttackState { get; private set; }

    [Header("State Data")]
    [Space]
    [SerializeField] D_MoveState moveStateData;
    [SerializeField] D_IdleState idleStateData;
    [SerializeField] D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] D_LookForPlayerState lookForPlayerStateData;
    [SerializeField] D_DeadState deadStateData;
    [SerializeField] D_RangeAttackState rangeAttackStateData;

    [Header("Attacks")]
    [Space]
    [SerializeField] private Transform RangeAttackPosition;

    public override void Start()
    {
        base.Start();

        idleState = new MB_2_IdleState(stateMachine, this, "idle", idleStateData, this);
        moveState = new MB_2_MoveState(stateMachine, this, "move", moveStateData, this);
        playerDetectedState = new MB_2_PlayerDetectedState(stateMachine, this, "playerDetected", playerDetectedStateData, this);
        lookForPlayerState = new MB_2_LookForPlayerState(stateMachine, this, "lookForPlayer", lookForPlayerStateData, this);
        deadState = new MB_2_DeadState(stateMachine, this, "dead", deadStateData, this);
        rangeAttackState = new MB_2_RangeAttackState(stateMachine, this, "rangeAttack", RangeAttackPosition, rangeAttackStateData, this);

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
    }

    private IEnumerator WaitToDisable()
    {
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }
}
