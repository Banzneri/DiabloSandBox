using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(fileName = "Shield", menuName = "Inventory/Shield", order = 1)]
public class Shield : Armor
{
    [SerializeField] private Image _image;

    public Shield() : base(5, 5, new Vector3(2, 3), "Shield", false, 5)
    {
        base.SetImage(_image);
        Debug.Log("Image: " + _image);
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
