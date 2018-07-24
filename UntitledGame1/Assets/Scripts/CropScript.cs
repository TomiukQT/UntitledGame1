using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropScript : MonoBehaviour {


    public GameObject[] stems;

    public bool isPlanted;

    private Clocks clocks;
    private float timer;

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
            foreach (GameObject stem in stems)
            {
                if(stem.transform.localScale.y < 1f)
                stem.transform.localScale += new Vector3(0, 0.1f, 0);

            }
        }
    }
        

}
