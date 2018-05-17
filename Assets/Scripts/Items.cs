using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {

    public static Item GetNewItem(string name)
    {
        switch (name)
        {
            case "Sword":
                return ScriptableObject.CreateInstance("Sword") as Sword;
            case "Shield":
                return ScriptableObject.CreateInstance("Shield") as Shield;
            case "Potion":
                return ScriptableObject.CreateInstance("Potion") as Potion;
        }

        return null;
    }

    public static ItemInInventory GetNewItemInInventory(Item item, InventorySlot startSlot, GameObject itemInInventoryPrefab, GameObject invPanel)
    {
        GameObject itemInInvGO = Instantiate(itemInInventoryPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero), invPanel.transform);
        ItemInInventory itemInInv = itemInInvGO.GetComponent<ItemInInventory>();
        itemInInv.Init(item, startSlot);
        return itemInInv;
    }
}
