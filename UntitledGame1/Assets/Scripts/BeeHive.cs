using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeHive : MonoBehaviour {

    private Clocks clocks;


    public float honeyProduction;
    public float honeyConsumption;

    public float honeyAmount;

    [Range(0,1)]
    public float beesHealth = 1;

    private float timer;

    public GameObject bees;
    private GameObject currentBees;


    void Start () 
    {
        clocks = GameObject.Find("GameManager").GetComponent<Clocks>();
    }
   

	
	void Update () 
    {
        UpdateTime();
        

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
        honeyAmount += (honeyProduction - honeyConsumption) * beesHealth /10;
    }

    private void OnTriggerEnter(Collider other)
    {
       /* if (other.GetComponent<Collider>().GetType() == typeof(CircleCollider2D))
        {
            
        }
         */   
        if (other.tag == "Player")
        {
            currentBees = Instantiate(bees, this.transform.position + this.transform.forward, Quaternion.identity);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(currentBees);
        }
    }


    public void Collect()
    {
        float honeyToCollect = Mathf.Floor(honeyAmount);
        honeyAmount -= honeyToCollect;
        Debug.Log("Collected: " + honeyToCollect);
    }


}
