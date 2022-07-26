using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_Boss_Eye : Entity
{
    public MB_1_IdleState idleState { get; private set; }
    public MB_1_MoveState moveState { get; private set; }
    public MB_1_PlayerDetectedState playerDetectedState { get; private set; }
    public MB_1_LookForPlayerState lookForPlayerState { get; private set; }
    public MB_1_MeleeAttackState meleeAttackState { get; private set; }

    [Header("StateData")]
    [Space]
    [SerializeField] D_IdleState idleStateData;
    [SerializeField] D_MoveState moveStateData;
    [SerializeField] D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] D_MeleeAttack meleeAttackStateData;
    [SerializeField] D_LookForPlayerState lookForPlayerStateData;

    [Header("Attacks")]
    [Space]
    [SerializeField] private Transform meleeAttackPosition;
    [SerializeField] private Transform RangeAttackPosition;

    public override void Start()
    {
        base.Start();

        idleState = new MB_1_IdleState(stateMachine, this, "idle", idleStateData, this);
        moveState = new MB_1_MoveState(stateMachine, this, "move", moveStateData, this);
        playerDetectedState = new MB_1_PlayerDetectedState(stateMachine, this, "playerDetected", playerDetectedStateData, this);
        lookForPlayerState = new MB_1_LookForPlayerState(stateMachine, this, "lookForPlayer", lookForPlayerStateData,this);
        meleeAttackState = new MB_1_MeleeAttackState(stateMachine, this, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);

        stateMachine.Initialize(moveState);
    }

    public override void Damage(float dmg)
    {
        base.Damage(dmg);

        if (isDead)
        {
            //stateMachine.ChangeState(deadState);
            StartCoroutine("WaitToDisable");
        }

    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        //TODO: Draw Melee Wire Sphere
    }

    private IEnumerator WaitToDisable()
    {
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }
}
