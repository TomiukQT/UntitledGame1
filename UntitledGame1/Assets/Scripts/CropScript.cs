using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropScript : MonoBehaviour {

    //grains
    private GameObject[] stems;

    //vegetable
    [SerializeField]
    private GameObject[] phases;

    [SerializeField]
    private int phaseCount;
    [SerializeField]
    private float phaseBorder;
    [SerializeField]
    private int currentPhase;

    public bool isPlanted;

    private Clocks clocks;
    private float timer;

    public float maxGrow;
    public float actuallGrow = 0;
    public bool isReady = false;



    public Item harvestItem;
    public int harvestItemCount;


    public Crop crop;

	void Start () 
    {
        clocks = GameObject.Find("GameManager").GetComponent<Clocks>();

        if (crop.type == Crop.Type.grain)
        {
            stems = new GameObject[gameObject.transform.childCount];
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                stems[i] = gameObject.transform.GetChild(i).gameObject;
            }
        }
        else if (crop.type == Crop.Type.vegetable)
        {
            phases = new GameObject[gameObject.transform.childCount];
            {
                for (int i = 0; i < phases.Length; i++)
                {
                    phases[i] = gameObject.transform.GetChild(i).gameObject;
                }
            }

            phaseCount = phases.Length -1;
            phaseBorder = maxGrow / phaseCount;
            currentPhase = 1;

            DisableAllChildrensActivateOne(0);
            

        }
        
	}

    

    private void DisableAllChildrensActivateOne(int index)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }

        gameObject.transform.GetChild(index).gameObject.SetActive(true);
    }


    void Update()
    {
        if(isPlanted)
        {
            UpdateTime();
        }
       
	}


    void UpdateTime()
    {
        timer += Time.deltaTime * clocks.timeSpeed * clocks.timerReduction;

        if (timer >= 1)
        {
            timer = 0;
            Tick();
        }
    }

    private void Tick()
    {
        if(isPlanted)
        {
            if (crop.type == Crop.Type.grain)
            {
                if (actuallGrow < maxGrow)
                    actuallGrow += 0.1f;
                foreach (GameObject stem in stems)
                {
                    if (gameObject.transform.localScale.y < maxGrow / 7.5f)
                        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, 0.15f + actuallGrow / 7.5f, gameObject.transform.localScale.z);
                    if (actuallGrow >= maxGrow)
                    {
                        isReady = true;
                    }
                }
            }
            else if (crop.type == Crop.Type.vegetable)
            {
                if (actuallGrow < maxGrow)
                    actuallGrow += 0.1f;

                if (actuallGrow >= (phaseBorder*currentPhase))
                {
                    if(currentPhase < phaseCount)
                    {                       
                        currentPhase++;
                        DisableAllChildrensActivateOne(currentPhase - 1);
                    }
                    

                }

                if (actuallGrow >= maxGrow)
                {
                    isReady = true;
                    DisableAllChildrensActivateOne(currentPhase);

                }


            }
           
        }
    }

 

        

}
