using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingScript : MonoBehaviour
{
    bool creating;
    public int mode; //1 for building mode, 2 for trash mode;

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
        Cursor.visible = false;

        pointer = GameObject.Find("Main Camera").GetComponent<MousePosition>();
        mode = 1;
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
        if (objToShow != null && mode != 2)
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
        if (Input.GetKeyDown(KeyCode.O))
        {
            
            Cursor.visible = true;
        }
        if (Input.GetKeyUp(KeyCode.O))
        {
            Cursor.visible = false;
        }


        if (Input.GetMouseButtonDown(0))
        {

            if (!EventSystem.current.IsPointerOverGameObject() && ableToBuild == 0 && mode == 1)
            {
                BuildObject();
            }

        }
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject() && mode == 2)
            {
                RaycastHit hit = pointer.GetRaycastHit();

                GameObject obj = hit.transform.gameObject;

                DestroyObject(obj);
            }            
        }


    }

    void BuildObject()
    {
        Vector3 current = pointer.SnapPosition(pointer.GetWorldPoint());
        current.y = 0f;
        Instantiate(objToBuild, current, Quaternion.identity);
        ableToBuild = 0;
    }

    void DestroyObject(GameObject o)
    {
        if (o.name != "Plane")
        Destroy(o);
    }


    public void ChooseObjectToBuild(int index)
    {
        ChangeMode(1);

        Debug.Log("Chosen obj to build");

        if (objToShow != null)
            Destroy(objToShow);

        objToBuild = elements[index].obj;
        objToShow = Instantiate(objToBuild, Vector3.zero, Quaternion.identity);
        objToShow.GetComponent<Renderer>().material = buildingMat;
    }

    public void ChangeMode(int m)
    {
        mode = m;
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
