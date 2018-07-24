
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="Items/Item")]
public class Item : ScriptableObject
{
    public new string name;

    public string description;

    public GameObject obj;

}
