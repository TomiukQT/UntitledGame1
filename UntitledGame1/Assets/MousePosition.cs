using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour {


    public GameObject mousePointer;


    void Start()
    {
        mousePointer = Instantiate(mousePointer, Vector3.zero, Quaternion.Euler(0, 0, 0));
    }


    void Update()
    {
        mousePointer.transform.position = SnapPosition(GetWorldPoint());
    }

    public Vector3 GetWorldPoint()
    {
        Camera cam = GetComponent<Camera>();
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    public Vector3 SnapPosition(Vector3 original)
    {
        Vector3 snapped;
        snapped.x = Mathf.Round((original.x + 0.05f) * 10f) / 10f;
        snapped.y = Mathf.Round((original.y + 0.05f) * 10f) / 10f;
        snapped.z = Mathf.Round((original.z + 0.05f) * 10f) / 10f;
        return snapped;
    }

}
