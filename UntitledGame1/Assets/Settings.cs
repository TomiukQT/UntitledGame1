using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {

    public Texture2D trashIcon;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;



    void Start () 
    {
        //Cursor.SetCursor(trashIcon, hotSpot, cursorMode);
    }
	
	
	void Update () 
    {
		
	}
}
