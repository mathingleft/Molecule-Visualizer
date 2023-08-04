using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserRotation : MonoBehaviour
{
    public static GameObject selected;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectObject();
        }
        if (Input.GetMouseButtonUp(0))
        {
            selected = null;
        }
    }

    void selectObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // Debug.Log(hit.collider.name);
            selected = hit.collider.gameObject;
        }
    }
}
