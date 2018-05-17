using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemInInventory : MonoBehaviour, IDragHandler
{
    private Item _item;
    private PlayerController _player;
    private InventorySlot _startSlot;

    public InventorySlot Slot
    {
        get { return _startSlot; }
    }

    private void Start() {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void Init(Item item, InventorySlot startSlot)
    {
        _item = item;
        _startSlot = startSlot;
        Vector3 scale = transform.localScale;
        scale.x *= _item.InvSize.x;
        scale.y *= _item.InvSize.y;
        transform.localScale = scale;
        Vector3 position = _startSlot.Image.transform.position;
        
        position.y -= _startSlot.Image.GetComponent<RectTransform>().rect.height / 2;
        position.x += _startSlot.Image.GetComponent<RectTransform>().rect.width / 2;
        transform.position = position;
        //GetComponent<Image>().sprite = _item.ItemImage.sprite;
    }

    public Item Item
    {
        get { return _item; }
        set { _item = value; }
    }

    public void OnDrag(PointerEventData eventData)
    {
        _player.Destination = _player.transform.position;
        transform.position = eventData.position;
    }
}
