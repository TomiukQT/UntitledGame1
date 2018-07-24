using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeHive : MonoBehaviour {

    private Clocks clocks;

    private Canvas ui;

    public float honeyProduction;
    public float honeyConsumption;

    public float honeyAmount;

    public float racks = 1f;

    [Range(0, 1)]
    public float beesHealth = 1f;

    private float timer;

    public GameObject bees;
    private GameObject currentBees;

    public GameObject honeyComb;
    public GameObject dropArea;


    void Start()
    {
        clocks = GameObject.Find("GameManager").GetComponent<Clocks>();
        ui = gameObject.GetComponentInChildren<Canvas>();
        ui.enabled = false;
    }



    void Update()
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
        honeyAmount += (honeyProduction - honeyConsumption) * racks * beesHealth / 10;
        beesHealth -= 0.01f;
    }

    private void OnTriggerEnter(Collider other)
    {
        /* if (other.GetComponent<Collider>().GetType() == typeof(CircleCollider2D))
         {

         }
          */
        if (other.tag == "Player")
        {
            ui.enabled = true;
            currentBees = Instantiate(bees, this.transform.position + this.transform.forward, Quaternion.identity);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            ui.enabled = false;
            StartCoroutine(DestroyBees(currentBees));
        }
    }

    private IEnumerator DestroyBees(GameObject curr)
    {
        yield return new WaitForSeconds(5f);
        Destroy(curr);
    }

    public void Collect()
    {
        if (honeyAmount < 100)
            return;


        int combsToDrop = (int)Mathf.Floor(honeyAmount / 100);
        float honeyToCollect = (float)combsToDrop * 100;
        honeyAmount -= honeyToCollect;
        DropCombs(combsToDrop);
       
    }

    public void DropCombs(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(honeyComb, new Vector3(dropArea.transform.position.x + Random.Range(-0.15f, 0.15f), dropArea.transform.position.y, dropArea.transform.position.z), dropArea.transform.rotation);
        }
    }


    public void AddRack()
    {
        if (racks < 10)
        {
            racks++;
        }
    }

    public void RemoveRack()
    {
        if (racks > 0)
        {
            racks--;
        }
    }



}
