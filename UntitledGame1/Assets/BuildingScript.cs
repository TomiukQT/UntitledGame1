using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{

    [SerializeField]
    private GameObject objToBuild;
    [SerializeField]
    private GameObject objToShow;
    Vector3 place;
    Vector3 cursor;

    public float rotation = 0;

    private GameObject cursorObj;

	void Start () 
    {
        cursorObj = Instantiate(objToShow, new Vector3(0, -50, 0), Quaternion.Euler(0, rotation, 0));
	}


    void Update()
    {
        //TODO if(BuldingMode)

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            
            if (Physics.Raycast(ray, out hit))
            {
                place = new Vector3(hit.point.x, 0, hit.point.z);
                Instantiate(objToBuild, place, Quaternion.Euler(0, rotation, 0));
            }

        }
        if (Physics.Raycast(ray, out hit))
        {
            cursor = new Vector3(hit.point.x, 0, hit.point.z);
            cursorObj.transform.position = cursor;
            cursorObj.transform.rotation = Quaternion.Euler(0, rotation, 0);

        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            rotation += 45f;
        }

        
        
    }

    


}
