using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {
	[SerializeField] private Color _occupiedSpaceColor;
	[SerializeField] private Color _unOccupiedSpaceColor;
	[SerializeField] private Image[] UISlots;
	[SerializeField] GameObject _itemInInventoryPrefab;
    [SerializeField] GameObject _invPanel;

	private int _invSpace = 50;
	private List<ItemInInventory> _items = new List<ItemInInventory>();
	private InventorySlot[,] _invSlots = new InventorySlot[5, 10];

	private void Start() {
		InitInv();
	}

	private void InitInv()
	{
		for (int y = 0; y < _invSlots.GetLength(0); y++)
		{
			for (int x = 0; x < _invSlots.GetLength(1); x++)
			{
				_invSlots[y, x] = new InventorySlot(new Vector2(x, y), UISlots[y * 10 + x]);
				_invSlots[y, x].Image.color = _unOccupiedSpaceColor;
			}
		}
	}

	public bool AddItemDefault(Item item)
	{
		for (int y = 0; y < _invSlots.GetLength(0); y++)
		{
			for (int x = 0; x < _invSlots.GetLength(1); x++)
			{
				List<InventorySlot> slots = new List<InventorySlot>();

				for (int nY = 0; nY < item.InvSize.y; nY++)
				{
					for (int nX = 0; nX < item.InvSize.x; nX++)
					{
						if (!IsSlotOccupied(new Vector2(x + nX, y + nY)))
						{
							Debug.Log(x + nX + " " + y + nY);
							slots.Add(_invSlots[y + nY, x + nX]);
							//Debug.Log("NotOccupied: X: " + _invSlots[(int)idy, (int)idx].Coord.x + " Y: " + _invSlots[(int)idy, (int)idx].Coord.y);
						}
					}
				}

				if (slots.Count == item.InvSize.x * item.InvSize.y)
				{
					foreach (var slot in slots)
					{
						Debug.Log(slot.Coord);
						slot.SetOccupyingItem(item);
						slot.Image.color = _occupiedSpaceColor;
					}
					_items.Add(Items.GetNewItemInInventory(item, slots[0], _itemInInventoryPrefab, _invPanel));
					return true;
				}
			}
		}

		return false;
	}

	public void DeleteItem(ItemInInventory item)
	{
		item.Slot.Image.color = _unOccupiedSpaceColor;
		item.Slot.SetOccupyingItem(null);
		_items.Remove(item);
	}

	private bool IsSlotOccupied(Vector2 coord)
	{
		for (int y = 0; y < _invSlots.GetLength(0); y++)
		{
			for (int x = 0; x < _invSlots.GetLength(1); x++)
			{
				if (coord.x == x && coord.y == y)
				{
					return _invSlots[y, x].IsOccupied;
				}
			}
		}
		return true;
	}
}
