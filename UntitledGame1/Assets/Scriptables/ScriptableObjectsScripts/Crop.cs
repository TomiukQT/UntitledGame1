using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Crops")]
public class Crop : ScriptableObject
{
    public new string name;
    public string description;

    public Sprite icon;
    public GameObject obj;

    public float cost;

	
	
}
