using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BeeHiveUIManager : MonoBehaviour {

    public Text honeyText;
    public Text rackText;

    public Slider hp;
    public Text hpText;

    private BeeHive beeHive;

	void Start () 
    {
        beeHive = this.GetComponent<BeeHive>();
	}
	
	
	void Update () 
    {
        UpdateText();
	}

    private void UpdateText()
    {
        honeyText.text = string.Format("Honey: {0} ml", beeHive.honeyAmount.ToString("0"));
        rackText.text = string.Format("Racks: {0}", beeHive.racks);
        hpText.text = string.Format("{0}%", (beeHive.beesHealth * 100).ToString("0"));
        hp.value = beeHive.beesHealth;

    }

    
}
