using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomMovement : MonoBehaviour
{
    int reverse = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }
        if (this.gameObject == UserRotation.selected && Input.GetMouseButton(0))
        {
            transform.RotateAround(Vector3.zero, Vector3.down, reverse * Input.GetAxis("Mouse X") * 300 * Time.deltaTime);
            transform.RotateAround(Vector3.zero, Vector3.right, reverse * Input.GetAxis("Mouse Y") * 300 * Time.deltaTime);
        }
    }

    void OnMouseDown()
    {
        reverse = (transform.position.z > 0 ? -1 : 1);
    }
}