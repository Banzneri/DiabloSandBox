using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(fileName = "Potion", menuName = "Inventory/Potion", order = 1)]
public class Potion : Item
{
    [SerializeField] private Image _image;

    public Potion() : base(5, 5, new Vector3(1, 1), "Potion", true)
    {
        base.SetImage(_image);
        Debug.Log("Image: " + _image);
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
