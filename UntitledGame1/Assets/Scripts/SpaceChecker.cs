using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceChecker : MonoBehaviour {


    private BuildingScript bScript;

	void Start () 
    {
        bScript = GameObject.Find("GameManager").GetComponent<BuildingScript>();
    }
	
	
	void Update () 
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Solid")
        {
            bScript.ableToBuild++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Solid")
        {
            bScript.ableToBuild--;
        }
    }
}
