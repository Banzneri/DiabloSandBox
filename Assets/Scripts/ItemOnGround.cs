using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnGround : MonoBehaviour {
	[SerializeField] private string _itemName;
    private Item _item;

	private InventoryManager _inventoryManager;
	private PlayerController _player;

	void Start () {
		_item = Items.GetNewItem(_itemName);
		_inventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
		_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnMouseDown() {
		if (Input.GetMouseButton(0))
		{
			if (Vector3.Distance(_player.transform.position, transform.position) < 2)
			{
				if (_inventoryManager.AddItemDefault(_item))
				{
					Destroy(this.gameObject);
				}
			}
		}
	}
}
