using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    private Inventory inventory;
	
	void Start () 
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
	}
	
	
	void Update () 
    {
        GetInput();
	}


    private void GetInput()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log(hit.collider.gameObject.tag);
                if (hit.collider.gameObject.tag == "Item")
                {
                    inventory.AddItemToInventory(hit.collider.gameObject.GetComponent<ItemScript>().item);
                    Destroy(hit.collider.gameObject);
                }


            }
        }
    }
}
