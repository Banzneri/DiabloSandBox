using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack {
    public float _attackDuration;
    public float _damageAreaDuration;
    public DamageAreaShape _damageAreaShape;
    public float _damageAreaWidth;
    public float _damageAreaLength;
    public string _attackName;

    public enum DamageAreaShape
    {
        Sphere,
        Box,
        Capsule
    }

    public Attack(float attackDuration, float damageAreaDuration, DamageAreaShape damageAreaShape, 
                    float damageAreaWidth, float damageAreaLength, string attackName)
    {
        _attackDuration = attackDuration;
        _damageAreaDuration = damageAreaDuration;
        _damageAreaShape = damageAreaShape;
        _damageAreaWidth = damageAreaWidth;
        _damageAreaLength = damageAreaLength;
        _attackName = attackName;
    }
}