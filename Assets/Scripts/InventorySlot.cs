using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot {
    private Item _occupyingItem = null;
    private Vector2 _coord;
    private Image _image;

    public InventorySlot(Vector2 coord, Image image)
    {
        _coord = coord;
        _image = image;
    }

    public bool IsOccupied
    {
        get { return _occupyingItem != null; }
    }

    public Vector2 Coord
    {
        get { return _coord; }
    }

    public Image Image
    {
        get { return _image; }
    }

    public void SetOccupyingItem(Item item)
    {
        _occupyingItem = item;
    }

    public void RemoveOccupyingItem()
    {
        _occupyingItem = null;
    }
}