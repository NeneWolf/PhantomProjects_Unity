using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newRangeAttackStateData", menuName = "Data/State Data/Range Attack Data")]
public class D_RangeAttackState : ScriptableObject
{
    [Header("Projectiles(Default)")]
    public bool useProjectile = true;
    public GameObject projectile;
    public float projectileDamage = 10f;
    public float projectileSpeed = 12f;

    [Header("Lazer")]
    public bool useLazer = false;
    public GameObject lazer;

    [Header("Fire Ball spawn")]
    public bool useFireBall = false;
    public GameObject fireBall;
    public float minRange;
    public float maxRange;
}
