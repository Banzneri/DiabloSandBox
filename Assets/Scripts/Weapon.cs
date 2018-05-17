using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : Item {
    private Vector2 _damage;
    private float _damageAreaWidthMultiplier;
    private float _damageAreaHeightMultiplier;
    private WeaponType _weaponType;

    public Weapon(float value, float weight, Vector2 invSize, string name, bool usable, 
                    Vector2 damage, float damageAreaHeightMultiplier, float damageAreaWidthMultiplier, WeaponType weaponType)
                     : base(value, weight, invSize, name, usable)
    {
        _damage = damage;
        _damageAreaHeightMultiplier = damageAreaHeightMultiplier;
        _damageAreaWidthMultiplier = damageAreaWidthMultiplier;
        _weaponType = weaponType;
    }

    public enum WeaponType
    {
        MeleeOneHand,
        MeleeTwoHand,
        RangedOneHand,
        RangedTwoHand
    }
}