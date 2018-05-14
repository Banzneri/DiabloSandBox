using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks {
    
    public static Attack MeleeAttackDefault()
    {
        float attackDuration = 1f;
        float damageAreaDuration = attackDuration * 0.4f;
        Attack.DamageAreaShape damageAreaShape = Attack.DamageAreaShape.Sphere;
        float damageAreaWidth = 1f;
        float damageAreaLength = 1f;
        string attackName = "Attack1";
        return new Attack(attackDuration, damageAreaDuration, damageAreaShape, damageAreaWidth, damageAreaLength, attackName);
    }

    public static Attack TestAttack1()
    {
        float attackDuration = 1f;
        float damageAreaDuration = attackDuration * 0.4f;
        Attack.DamageAreaShape damageAreaShape = Attack.DamageAreaShape.Box;
        float damageAreaWidth = 1f;
        float damageAreaLength = 3f;
        string attackName = "Attack2";
        return new Attack(attackDuration, damageAreaDuration, damageAreaShape, damageAreaWidth, damageAreaLength, attackName);
    }

    public static Attack TestAttack2()
    {
        float attackDuration = 1f;
        float damageAreaDuration = attackDuration * 0.4f;
        Attack.DamageAreaShape damageAreaShape = Attack.DamageAreaShape.Sphere;
        float damageAreaWidth = 1f;
        float damageAreaLength = 1f;
        string attackName = "Attack3";
        return new Attack(attackDuration, damageAreaDuration, damageAreaShape, damageAreaWidth, damageAreaLength, attackName);
    }
}