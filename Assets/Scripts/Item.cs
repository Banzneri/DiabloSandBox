using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : ScriptableObject {
    private float _value;
    private float _weight;
    private Vector2 _invSize;
    private string _name;
    private bool _usable;
    private Image _itemImage;

    public float Value { get { return _value; }}
    public float Weight { get { return _weight; }}
    public Vector2 InvSize { get { return _invSize; }}
    public string Name { get { return _name; }}
    public bool Usable { get { return _usable; }}
    public Image ItemImage { get { return _itemImage; }}

    public Item(float value, float weight, Vector2 invSize, string name, bool usable)
    {
        _value = value;
        _weight = weight;
        _invSize = invSize;
        _name = name;
        _usable = usable;
    }

    public abstract void Use();

    public virtual void SetImage(Image image)
    {
        _itemImage = image;
    }
}
