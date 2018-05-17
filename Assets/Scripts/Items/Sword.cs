using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(fileName = "Sword", menuName = "Inventory/Sword", order = 1)]
public class Sword : Weapon
{
    [SerializeField] private Image _image;

    public Sword() : base(5, 5, new Vector3(1, 3), "Sword", false, new Vector2(1, 3), 1, 1, Weapon.WeaponType.MeleeOneHand)
    {
        base.SetImage(_image);
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
