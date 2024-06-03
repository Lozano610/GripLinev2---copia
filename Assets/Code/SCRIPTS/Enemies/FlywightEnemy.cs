using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SlareVelocityIA;

[CreateAssetMenu(menuName = "ScriptableObjects/EnemyData")]
public class FlywightEnemy : ScriptableObject
{
    public float StartSpeed;
    public float V_min;
    public float V_max;

}
