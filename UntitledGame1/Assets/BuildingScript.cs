using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingScript : MonoBehaviour
{
    bool creating;

    MousePosition pointer;
    
    public GameObject objToBuild;
    public GameObject objToShow;
   
    public Material buildingMat;
    public Material cancelMat;

    public int ableToBuild = -1;


    private GameObject lastObj;

    [SerializeField]
    private Element[] elements;

    void Start()
    {
        pointer = GameObject.Find("Main Camera").GetComponent<MousePosition>();
    }




    void Update()
    {

        if (objToBuild != null)
        {
            GetInput();

            ShowBuildingObject();
        }
        
    }


    void ShowBuildingObject()
    {
        if (objToShow != null)
        {
            Vector3 current = pointer.SnapPosition(pointer.GetWorldPoint());
            current.y = 0f;
            objToShow.transform.position = current;
            if (ableToBuild > 0)
            {
                objToShow.GetComponent<Renderer>().material = cancelMat;
            }
            else if (ableToBuild <= 0)
            {
                objToShow.GetComponent<Renderer>().material = buildingMat;
            }
        }
    }

    void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            if (!EventSystem.current.IsPointerOverGameObject() && ableToBuild == 0)
            {
                BuildObject();
            }
            
        }

       
    }

    void BuildObject()
    {
        Vector3 current = pointer.SnapPosition(pointer.GetWorldPoint());
        current.y = 0f;
        Instantiate(objToBuild, current, Quaternion.identity);
    }


    public void ChooseObjectToBuild(int index)
    {

        if (objToShow != null)
            Destroy(objToShow);

        objToBuild = elements[index].obj;
        objToShow = Instantiate(objToBuild, Vector3.zero, Quaternion.identity);
        objToShow.GetComponent<Renderer>().material = buildingMat;
    }

    // MULTIBUILDING... Do it later 
    /*
    
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

    */





}
