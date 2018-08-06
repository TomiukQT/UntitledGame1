using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {


    private Inventory inventory;
    private GameObject hand;
    private GameObject camera;

    [SerializeField]
    private GameObject skillTree;

    public GameObject tools;


    private GameObject grabbed;
    private bool isGrabbing = false;

    private bool hasTool = false;
    private GameObject tool;


    void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        camera = GameObject.Find("Main Camera");
        hand = GameObject.Find("Hand");


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        GetInput();
        if (isGrabbing)
        {
            Carry(hand);
        }
        if (isGrabbing && hand.transform.childCount == 0)
        {
            isGrabbing = false;
        }
        if (hasTool)
        {
            Carry(tool);
            //tool.transform.rotation = new Quaternion(camera.transform.rotation.x + 90, camera.transform.rotation.y, camera.transform.rotation.z,1f);
            
        }
    }


    private void GetInput()
    {

        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
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

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            Cursor.visible = !Cursor.visible;
            skillTree.SetActive(!skillTree.activeInHierarchy);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchTool(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchTool(1);
        }

    }

    public void SwitchTool(int index)
    {
        if (index == 0)
        {
            DisableAllTools();
        }
        else if(index <= tools.transform.childCount)
        {
            hasTool = true;
            tool = tools.transform.GetChild(index - 1).gameObject;
            tools.transform.GetChild(index-1).gameObject.SetActive(true);
        }
    }


    private void DisableAllTools()
    {
        for (int i = 0; i < tools.transform.childCount; i++)
        {
            tools.transform.GetChild(i).gameObject.SetActive(false);
        }
        hasTool = false;
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
