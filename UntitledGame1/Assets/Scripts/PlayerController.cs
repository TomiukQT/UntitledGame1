using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    private Inventory inventory;
    private GameObject hand;
    private GameObject camera;

    private GameObject grabbed;
    private bool isGrabbing = false;

    void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        camera = GameObject.Find("Main Camera");
        hand = GameObject.Find("Hand");
    }


    void Update()
    {
        GetInput();
        if(isGrabbing)
        {
            Carry(hand);
        }
        if(isGrabbing && hand.transform.childCount == 0)
        {
            isGrabbing = false;
        }
    }


    private void GetInput()
    {

        if (Input.GetMouseButtonDown(0))
        {
            TakeItem();
        }
        if (Input.GetKeyDown(KeyCode.E) && !isGrabbing)
        {
            GrabItem();
        }
        else if (Input.GetKeyDown(KeyCode.E) && isGrabbing)
        {
            DropItem();
        }


    }

    private void GrabItem()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log(hit.collider.gameObject.tag);
            if (hit.collider.gameObject.tag == "Item")
            {
                grabbed = hit.collider.gameObject;
                grabbed.transform.SetParent(hand.transform);
                grabbed.GetComponent<Rigidbody>().useGravity = false;
                grabbed.GetComponent<Rigidbody>().isKinematic = true;
                grabbed.GetComponent<Rigidbody>().detectCollisions = true;
                grabbed.transform.position = hand.transform.position;
                isGrabbing = true;

                
            }


        }

    }

    private void Carry(GameObject o)
    {
        o.transform.position = Vector3.Lerp(o.transform.position, camera.transform.position + camera.transform.forward * 1f + camera.transform.up*0.1f, Time.deltaTime * 5f);

    }

    private void DropItem()
    {
        grabbed.GetComponent<Rigidbody>().useGravity = true;
        grabbed.GetComponent<Rigidbody>().isKinematic = false;      
        hand.transform.DetachChildren();
        isGrabbing = false;
    }

    private void TakeItem()
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
