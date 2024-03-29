using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repulsion : MonoBehaviour
{
    float forceFactor = 5f;
    Transform center;
    List<Rigidbody> otherAtoms = new List<Rigidbody>();
    GameObject centralAtom;
    float range = 10f;
    float xAngle;
    float yAngle;
    public Transform centerTransform;

    // Start is called before the first frame update

    void Awake()
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

    void OnEnable()
    {
        MoleculeSpawner.OnRemoveBond += removeRigidbody;
    }

    void OnDisable()
    {
        MoleculeSpawner.OnRemoveBond -= removeRigidbody;
    }

    void removeRigidbody(GameObject gameObject)
    {
        otherAtoms.Remove(gameObject.GetComponentInChildren<Rigidbody>());
        // Debug.Log(otherAtoms.Count);
    }

    void FixedUpdate()
    {
        foreach (Rigidbody obj in otherAtoms)
        {
            Vector3 distance = transform.position - obj.position;

            float magnitude = distance.magnitude;
            float distanceScaled = Mathf.InverseLerp(range, 0f, magnitude);
            float attractStrength = Mathf.Lerp(0f, forceFactor, distanceScaled);

            obj.AddForce(distance.normalized * attractStrength * -1f);
            /*
            List<Vector3> forces = new List<Vector3>();

            Vector3 distance = transform.position - obj.position;
            float absoluteDistance = Vector3.Distance(center.position, obj.position);
            float distanceSquared = distance.sqrMagnitude;

            float forceMagnitude = forceFactor / distanceSquared;
            float distanceScaled = Mathf.InverseLerp(range, 0, distanceSquared);
            float attractStrength = Mathf.Lerp(0, forceMagnitude, distanceScaled);

            // doesn't work: less x dist should be more x force
            float xForce = forceMagnitude * distance.x / absoluteDistance;
            float yForce = forceMagnitude * distance.y / absoluteDistance;
            float zForce = forceMagnitude * distance.z / absoluteDistance;

            Vector3 Force = new Vector3(xForce, yForce, zForce);

            forces.Add(Vector3);

            // try to make this from obj to itself
            transform.AddForce((attractStrength * distance.normalized * Time.fixedDeltaTime));
            */
        }

        if (this.gameObject == UserRotation.selected && Input.GetMouseButton(0))
        {
            centerTransform.rotation = calculateRotation();
            //rotationTest();
            // Debug.Log("this works!");
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
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if(otherAtoms.Contains(rb))
        {
            return;
        }
        if (other.CompareTag("Atom"))
        {
            otherAtoms.Add(other.GetComponent<Rigidbody>());
        }
        if (other.CompareTag("Bond"))
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
        if (other.CompareTag("Bond"))
        {
            otherAtoms.Remove(other.GetComponent<Rigidbody>());
        }
        if (other.CompareTag("Central Atom"))
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
