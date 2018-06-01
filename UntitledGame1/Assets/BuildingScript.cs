using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    bool creating;

    MousePosition pointer;

    public GameObject objToBuild;

    private GameObject lastObj;

	void Start () 
    {
        pointer = GetComponent<MousePosition>();
	}


    void Update()
    {
        GetInput();

    }

    void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartBuild();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            SetBuild();
        }
        else if (creating)
        {
            UpdateBuild();
        }
    }

    void StartBuild()
    {
        creating = true;
        Vector3 startPos = pointer.SnapPosition(pointer.GetWorldPoint());
        GameObject startObj = Instantiate(objToBuild, startPos, Quaternion.identity);
        // startObj.transform.position = new Vector3(startPos.x, startPos.y, startPos.z);
        lastObj = startObj;
    }

    void SetBuild()
    {
        creating = false;
    }

    void UpdateBuild()
    {
        Vector3 current = pointer.SnapPosition(pointer.GetWorldPoint());
       // current = new Vector3(current.x, current.y, current.z);
        if (!current.Equals(lastObj.transform.position))
        {
            CreateBuildSegment(current);
        }
    }

    void CreateBuildSegment(Vector3 current)
    {
        GameObject newObj = Instantiate(objToBuild, current, Quaternion.identity);
        lastObj = newObj;
    }


}
