using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2_Fetiddeviation : Entity
{
    public B2_MoveState moveState { get; private set; }
    public B2_IdleState idleState { get; private set; }
    public B2_PlayerDetectedState playerDetectedState { get; private set; }
    public B2_LookForPlayerState lookForPlayerState { get; private set; }
    public B2_DeadState deadState { get; private set; }
    public B2_MeleeAttackState meleeAttackState { get; private set; }
    public B2_ChargeState chargeState { get; private set; }

    [Header("State Data")]
    [Space]
    [SerializeField] D_MoveState moveStateData;
    [SerializeField] D_IdleState idleStateData;
    [SerializeField] D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] D_LookForPlayerState lookForPlayerStateData;
    [SerializeField] D_DeadState deadStateData;
    [SerializeField] D_MeleeAttack meleeAttackStateData;
    [SerializeField] D_ChargeState chargeStateData;

    [Header("Attacks")]
    [Space]
    [SerializeField] private Transform MeleeAttackPosition;

    [SerializeField] GameObject minionsGroup;
    public bool minionsDead { get; private set; }

    public override void Start()
    {
        base.Start();
        minionsDead = false;

        idleState = new B2_IdleState(stateMachine, this, "idle", idleStateData, this);
        moveState = new B2_MoveState(stateMachine, this, "move", moveStateData, this);
        playerDetectedState = new B2_PlayerDetectedState(stateMachine, this, "playerDetected", playerDetectedStateData, this);
        lookForPlayerState = new B2_LookForPlayerState(stateMachine, this, "lookForPlayer", lookForPlayerStateData, this);
        deadState = new B2_DeadState(stateMachine, this, "dead", deadStateData, this);
        meleeAttackState = new B2_MeleeAttackState(stateMachine, this, "meleeAttack", MeleeAttackPosition, meleeAttackStateData, this);
        chargeState = new B2_ChargeState(stateMachine, this, "charge", chargeStateData, this);

        stateMachine.Initialize(moveState);

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (minionsGroup.activeInHierarchy == false && !minionsDead)
        {
            minionsDead = true;
        }
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

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(MeleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }

    public override void Update()
    {
        base.Update();
    }

    private IEnumerator WaitToDisable()
    {
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }
}
