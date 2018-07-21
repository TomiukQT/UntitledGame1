using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingScript : MonoBehaviour
{
    bool creating;
    public string mode;

    MousePosition pointer;

    [SerializeField]
    private Element[] elements;

    public GameObject objToBuild;
    public GameObject objToShow;
   
    public Material buildingMat;
    public Material cancelMat;

    public int ableToBuild = -1;


    private GameObject lastObj;

    

    [SerializeField]
    private Crop[] crops;

    public GameObject cropToPlant;
    public GameObject cropToShow;

    void Start()
    {
        pointer = GameObject.Find("Main Camera").GetComponent<MousePosition>();
        mode = "build";
    }




    void Update()
    {

        if (objToBuild != null)
        {
            GetInput();
            ShowBuildingObject();
        }

        if (cropToPlant != null)
        {
            GetInput();
            ShowPlantingCrop();

        }

        
    }


   

    void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (!EventSystem.current.IsPointerOverGameObject() && ableToBuild == 0 && mode == "build")
            {
                BuildObject();
            }

        }
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject() && mode == "trash")
            {
                RaycastHit hit = pointer.GetRaycastHit();

                GameObject obj = hit.transform.gameObject;

                DestroyObject(obj);
            }            
        }


    }

    //Building elements (farmlands,waterwells,etc.)
    #region Building
    

    void ShowBuildingObject()
    {
        if (objToShow != null && mode == "build")
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
        ChangeMode("build");
        DestroyAllShowingObjects();

        objToBuild = elements[index].obj;
        objToShow = Instantiate(objToBuild, Vector3.zero, Quaternion.identity);
        objToShow.GetComponent<Renderer>().material = buildingMat;
    }
    #endregion

    public void DestroyAllShowingObjects()
    {
        if (objToShow != null)
            Destroy(objToShow);
        if (cropToShow != null)
            Destroy(cropToShow);
    }
    //Planting crops
    #region Crop
    public void ChooseCropToPlant(int index)
    {
        ChangeMode("plant");
        DestroyAllShowingObjects();

        cropToPlant = crops[index].obj;
        cropToShow = Instantiate(cropToPlant, Vector3.zero, Quaternion.identity);
       // cropToShow.GetComponent<Renderer>().material = buildingMat;
    }

    void ShowPlantingCrop()
    {
        if (cropToShow != null && mode == "plant")
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.gameObject.name.ToString());

            }


            /*
            Vector3 current = pointer.SnapPosition(pointer.GetWorldPoint());
            current.y = 0f;
            cropToShow.transform.position = current;
            if (ableToBuild > 0)
            {
                cropToShow.GetComponent<Renderer>().material = cancelMat;
            }
            else if (ableToBuild <= 0)
            {
                cropToShow.GetComponent<Renderer>().material = buildingMat;
            }
            */
        }
    }


    void CropPlant()
    {
        Vector3 current = pointer.SnapPosition(pointer.GetWorldPoint());
        current.y = 0f;
        Instantiate(cropToPlant, current, Quaternion.identity);
        ableToBuild = 0;
    }

    #endregion

    public void ChangeMode(string m)
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
