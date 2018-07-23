using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Camera cam;

	void Start () 
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}


    void Update()
    {
        GetInput();
	}

    void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("ahoj");
            Physics.Raycast(cam.transform.position, cam.transform.forward);
            Debug.DrawRay(cam.transform.position, cam.transform.forward);

        }
    }

}
