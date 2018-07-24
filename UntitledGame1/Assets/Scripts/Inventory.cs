using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {


    public List<Item> inventory; 

	
	void Start () 
    {
		inventory = new List<Item>();
    }
	
	public void AddItemToInventory(Item i)
    {
        inventory.Add(i);
    }
    
}
