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
        mode = "none";
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
        if (Input.GetMouseButtonDown(1) && mode == "none")
        {
            HarvestCrop();
        }

    }
 
    void GetInput()
    {
       
        if(Input.GetKeyDown(KeyCode.R))
        {
            if (mode == "build")
            {
                RotateShowingObject();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {

            if (!EventSystem.current.IsPointerOverGameObject() && ableToBuild == 0 && mode == "build")
            {
                BuildObject();
            }

        }
        if (Input.GetMouseButtonDown(1) && mode != "none")
        {
            DestroyAllShowingObjects();
            ableToBuild = 0;
            mode = "none";
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
        if (Input.GetMouseButtonDown(0))
        {

            if (!EventSystem.current.IsPointerOverGameObject() && mode == "plant")
            {
                
                PlantCrop();
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
            current.y = 0f + (objToBuild.transform.localScale.y / 2);
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

    void RotateShowingObject()
    {
        objToShow.transform.Rotate(new Vector3(0f, 90f, 0f));
    }

    void BuildObject()
    {
        Vector3 current = pointer.SnapPosition(pointer.GetWorldPoint());
        current.y = 0f + (objToBuild.transform.localScale.y/2);
        Instantiate(objToBuild, current, objToShow.transform.rotation);
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
        objToShow = Instantiate(elements[index].showObj, Vector3.zero, Quaternion.identity);
        objToShow.GetComponent<Renderer>().material = buildingMat;
        
       /* Collider[] colliders = objToShow.GetComponents<Collider>();
        foreach (Collider col in colliders)
        {
            if (!col.isTrigger)
            {
                col.isTrigger = true;
            }
            
        }*/
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
                if (hit.collider.gameObject.name == "Farmland(Clone)")
                {
                    SnapCropToFarmLand(hit.collider.gameObject);
                }
                else
                {
                    
                }

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

    void SnapCropToFarmLand(GameObject farmland)
    {
        Collider[] colliders = cropToShow.GetComponents<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }
        cropToShow.transform.position = new Vector3(farmland.transform.position.x, farmland.transform.position.y + 0.1f, farmland.transform.position.z);
    }

    void PlantCrop()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.gameObject.name.ToString());
            if (hit.collider.gameObject.name == "Farmland(Clone)" && !hit.collider.gameObject.GetComponent<FarmlandProperties>().isOccupied)
            {
                Debug.Log("ahoj");
                GameObject farmland = hit.collider.gameObject;
                Vector3 current = new Vector3(farmland.transform.position.x, farmland.transform.position.y + 0.1f, farmland.transform.position.z);
                GameObject planted = Instantiate(cropToPlant, current, Quaternion.identity);
                planted.GetComponent<CropScript>().isPlanted = true;
                farmland.GetComponent<FarmlandProperties>().isOccupied = true;

            }
           

        }

    }


    void HarvestCrop()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.gameObject.name.ToString());
            if (hit.collider.gameObject.name == "Wheat(Clone)" && hit.collider.gameObject.GetComponent<CropScript>().isReady)
            {
                Debug.Log("je ready");
                
                GameObject crop = hit.collider.gameObject;
               
                for (int i = 0; i < crop.GetComponent<CropScript>().harvestItemCount; i++)
                {
                    Vector3 current = new Vector3(crop.transform.position.x + Random.Range(-0.15f, 0.15f), crop.transform.position.y + 1f, crop.transform.position.z);
                    GameObject planted = Instantiate(crop.GetComponent<CropScript>().harvestItem.obj, current, Quaternion.identity);
                    planted.transform.Rotate(new Vector3(Random.Range(0, 180), Random.Range(0,180),Random.Range(0, 180)));
                }

                Destroy(crop);
                                        

            }


        }
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
