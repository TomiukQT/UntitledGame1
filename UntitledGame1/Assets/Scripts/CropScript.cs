using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropScript : MonoBehaviour {


    public GameObject[] stems;

    public bool isPlanted;

    private Clocks clocks;
    private float timer;

    public float maxGrow = 150;
    public float actuallGrow = 0;
    public bool isReady = false;

    public Item harvestItem;
    public int harvestItemCount;

	void Start () 
    {
        clocks = GameObject.Find("GameManager").GetComponent<Clocks>();


        stems = new GameObject[gameObject.transform.childCount];
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            stems[i] = gameObject.transform.GetChild(i).gameObject;
        }
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
            if(actuallGrow < maxGrow)
            actuallGrow += 0.1f;
            foreach (GameObject stem in stems)
            {
                if(gameObject.transform.localScale.y < maxGrow/7.5f)
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, 0.15f + actuallGrow/7.5f, gameObject.transform.localScale.z);
                if(actuallGrow >= maxGrow )
                {
                    isReady = true;
                }
            }
        }
    }

 

        

}
