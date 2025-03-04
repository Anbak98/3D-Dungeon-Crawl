using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "ScriptableObject/Stat/Player")]
public class PlayerStatus : ScriptableObject
{
    [Header("Energy")]
    public int health;

    [Header("Movement")]
    public float moveSpeed;
}
