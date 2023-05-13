using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repulsion : MonoBehaviour
{
    float forceFactor = 1000f;
    Transform center;
    List<Rigidbody> otherAtoms = new List<Rigidbody>();
    GameObject centralAtom;
    float range = 100f;
    float xAngle;
    float yAngle;

    // Start is called before the first frame update
    void Start()
    {
        center = GetComponent<Transform>();
        centralAtom = GameObject.Find("Central Atom");
    }

    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */

    void FixedUpdate()
    {
        foreach (Rigidbody obj in otherAtoms)
        {
            Vector3 distance = obj.position - center.position;
            float absoluteDistance = Vector3.Distance(center.position, obj.position);
            float distanceSquared = distance.sqrMagnitude;

            float forceMagnitude = forceFactor / distanceSquared;
            float distanceScaled = Mathf.InverseLerp(range, 0, distanceSquared);
            float attractStrength = Mathf.Lerp(0, forceMagnitude, distanceScaled);

            float xForce = forceMagnitude * distance.x / absoluteDistance;
            float yForce = forceMagnitude * distance.y / absoluteDistance;
            float zForce = forceMagnitude * distance.z / absoluteDistance;

            Vector3 Force = new Vector3(xForce, yForce, zForce);

            // try to make this from obj to itself
            obj.AddForce((attractStrength * distance.normalized * Time.fixedDeltaTime));
        }

        if (this.gameObject == UserRotation.selected && Input.GetMouseButton(0))
        {
            //this.transform.rotation = calculateRotation();
            rotationTest();
            Debug.Log("this works!");
        }
    }

    Quaternion calculateRotation()
    {
        xAngle += Input.GetAxis("Mouse X") * forceFactor * -Time.deltaTime;
        yAngle += Input.GetAxis("Mouse Y") * forceFactor * -Time.deltaTime;
        return Quaternion.AngleAxis(xAngle, Vector3.up) * Quaternion.AngleAxis(yAngle, -Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Atom"))
        {
            otherAtoms.Add(other.GetComponent<Rigidbody>());
        }
        if (other.CompareTag("Central Atom"))
        {
            otherAtoms.Add(other.GetComponent<Rigidbody>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Atom"))
        {
            otherAtoms.Remove(other.GetComponent<Rigidbody>());
        }
    }

    private void rotationTest()
    {
        xAngle = Input.GetAxis("Mouse X") * forceFactor * -Time.deltaTime;
        yAngle = Input.GetAxis("Mouse Y") * forceFactor * -Time.deltaTime;
        transform.RotateAround(Vector3.zero, Vector3.up, xAngle);
        transform.RotateAround(Vector3.zero, -Vector3.forward, yAngle);
    }
}
