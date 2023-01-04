using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStats : MonoBehaviour
{
    private float allDamageMultiplier = 1f;

    public float allDamage { get {return allDamageMultiplier;} set {allDamageMultiplier = value;} }

    //Single data

    private float singleDamageMultiplier = 1f;

    public float singleDamage { get {return singleDamageMultiplier;} set {singleDamageMultiplier = value;} }

    public float singleHomingSpeed;

    public int singleBounceAmount;

    public bool singleArePenetrating;

    private float defenseDamageMultiplier = 1f;

    public float defenceDamage { get {return defenseDamageMultiplier;} set {defenseDamageMultiplier = value;} }

    // area data

    private float areaDamageMultiplier = 1f;

    public float areaDamage { get {return areaDamageMultiplier;} set {areaDamageMultiplier = value;} }

    public int areaBulletsFromCorpses;

    public float additionalDamageToShokedEnemies;

    public float additionalDamageToOnFireEnemies;

    private float allDamageBufer = 1;

    private float singleDamageBufer = 1;

    private float defenceDamageBufer = 1;

    private float areaDamageBufer = 1;

    private void Start()
    {
        Main.arenaManager.StartArena += SaveBufers;

        Main.arenaManager.StopArena += ImportBufers;
    }

    private void SaveBufers()
    {
        allDamageBufer = allDamageMultiplier;

        singleDamageBufer = singleDamageMultiplier;

        defenceDamageBufer = defenseDamageMultiplier;

        areaDamageBufer = areaDamageMultiplier;
    }

    private void ImportBufers()
    {
        allDamageMultiplier = allDamageBufer;

        singleDamageMultiplier = singleDamageBufer;

        defenseDamageMultiplier = defenceDamageBufer;

        areaDamageMultiplier = areaDamageBufer;
    }


    public float MultiplyDamage(float damage, DroneType type)
    {
        damage *= allDamageMultiplier; 

        switch (type)
        {
            case DroneType.Single: damage *= singleDamageMultiplier; break;
            case DroneType.Defence: damage *= defenseDamageMultiplier; break;
            case DroneType.Area: damage *= areaDamageMultiplier; break;
            default: break;
        }

        return damage;
    }
}
