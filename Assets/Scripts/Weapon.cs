using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon {
    private float _minDamage;
    private float _maxDamage;
    private float _damageAreaWidthMultiplier;
    private float _damageAreaHeightMultiplier;
    private string _weaponName;
    private WeaponType _weaponType;

    private enum WeaponType
    {
        MeleeOneHand,
        MeleeTwoHand,
        RangedOneHand,
        RangedTwoHand
    }
}