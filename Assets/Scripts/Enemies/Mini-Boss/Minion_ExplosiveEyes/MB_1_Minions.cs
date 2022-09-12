using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_1_Minions : Entity
{
    public MB_1_M_IdleState idleState { get; set; }
    public MB_1_M_MoveState moveState { get; set; }
    public MB_1_M_PlayerDetectedState playerDetectedState { get; private set; }
    public MB_1_M_ChargeState chargeState { get; private set; }
    public MB_1_M_LookForPlayerState lookForPlayerState { get; private set; }
    public MB_1_M_MeleeAttackState meleeAttackState { get; private set; }
    public MB_1_M_DeadState deadState { get; private set; }
    public MB_1_M_ExplodeState explodeState { get; private set; }

    [Header("State Data")]
    [Space]
    [SerializeField] D_IdleState idleStateData;
    [SerializeField] D_MeleeAttack meleeAttackStateData;
    [SerializeField] D_MoveState moveStateData;
    [SerializeField] D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] D_ChargeState chargeStateData;
    [SerializeField] D_LookForPlayerState lookForPlayerStateData;
    [SerializeField] D_ExplodeState explodeStateData;
    [SerializeField] D_DeadState deadStateData;
    


    [Header("Attacks")]
    [Space]
    [SerializeField] private Transform meleeAttackPosition;
    [SerializeField] private Transform explosionPosition;

    public override void Start()
    {
        base.Start();

        moveState = new MB_1_M_MoveState(stateMachine, this, "move", moveStateData, this);
        idleState = new MB_1_M_IdleState(stateMachine, this, "idle", idleStateData, this);
        playerDetectedState = new MB_1_M_PlayerDetectedState(stateMachine, this, "playerDetected", playerDetectedStateData, this);
        chargeState = new MB_1_M_ChargeState(stateMachine, this, "charge", chargeStateData, this);
        lookForPlayerState = new MB_1_M_LookForPlayerState(stateMachine, this, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new MB_1_M_MeleeAttackState(stateMachine, this, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        deadState = new MB_1_M_DeadState(stateMachine, this, "dead", deadStateData, this);
        explodeState = new MB_1_M_ExplodeState(stateMachine, this, "explode", explosionPosition, explodeStateData, this);

        stateMachine.Initialize(lookForPlayerState);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
        Gizmos.DrawWireSphere(explosionPosition.position, explodeStateData.attackRadius);
    }

    public override void Damage(float dmg)
    {
        base.Damage(dmg);

        if (isDead)
        {
            stateMachine.ChangeState(deadState);
            StartCoroutine("WaitToDisable");
        }
        else if (!CheckPlayerInMinAgroRange())
        {
            lookForPlayerState.SetTurnImmediatly(true);
            stateMachine.ChangeState(lookForPlayerState);
        }
    }

    private IEnumerator WaitToDisable()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
