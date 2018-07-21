using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Element", menuName = "Elements")]
public class Element : ScriptableObject {

    //name of element
    public new string name;
    public string description;

    public GameObject obj;


    public float cost;
    public float speed;


	
}
