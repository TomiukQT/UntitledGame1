using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {


    public List<Item> inventory;
    public int capacity;
	

	void Start () 
    {
		inventory = new List<Item>();
    }
	
	public void AddItemToInventory(Item i)
    {
        if(inventory.Count<capacity)
        {
            inventory.Add(i);
        }
       
    }

    public void RemoveItemFromInventory(Item i)
    {
        inventory.Remove(i);
    }
    

}
