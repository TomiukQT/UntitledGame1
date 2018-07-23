﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BeeHiveUIManager : MonoBehaviour {

    public Text honeyText;

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

    }

    
}