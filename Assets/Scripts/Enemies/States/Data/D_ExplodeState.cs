using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newExplodeStateData", menuName = "Data/State Data/Explode Data")]
public class D_ExplodeState : ScriptableObject
{
    public float attackRadius = 0.5f;
    public float attackDamage = 10f;
    public LayerMask whatIsPlayer;
}
