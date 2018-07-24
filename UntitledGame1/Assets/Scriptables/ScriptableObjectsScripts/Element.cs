using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Elements")]
public class Element : ScriptableObject {

    //name of element
    public new string name;
    public string description;

    public GameObject obj;
    public GameObject showObj;


    public float cost;
    public float quality;


	
}
