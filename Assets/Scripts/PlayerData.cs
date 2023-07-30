using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Walk state")]
    public float walkAcceleration = 30f;
    public float maxWalkSpeed = 12f, turnTime = .1f, turnSpeed = 150, jumpSpeed = 5f, damage = 7f;

    [Header("Unlocks")]
    public bool chargeBoostUnlocked = false;
}
