using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDodgeStateData", menuName = "Data/State Data/Dodge Data")]
public class D_DodgeState : ScriptableObject
{
    public float dodgeSpeed = 10;
    public Vector2 dodgeAngle;

    public float dodgeTime = 0.2f;
    public float dodgeCooldown = 2f;
}
