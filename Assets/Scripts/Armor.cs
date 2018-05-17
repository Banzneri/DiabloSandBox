using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Armor : Item {
    private int _armorRating;

    public Armor(float value, float weight, Vector2 invSize, string name, bool usable,
                    int armorRating) : base(value, weight, invSize, name, usable)
    {
        _armorRating = armorRating;
    }

    public enum ArmorType
    {
        ChestArmor,
        Boots,
        Gloves,
        Helmet,
        Shield
    }

}