using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BondPosition : MonoBehaviour
{
    public GameObject[] atoms = new GameObject[2];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        updatePositionRotation();
    }

    public void assignAtoms(GameObject central, GameObject other)
    {
        atoms[0] = central;
        atoms[1] = other;
    }

    public void updatePositionRotation()
    {
        if (atoms[0] == null || atoms[1] == null)
        {
            return;
        }

        Vector3 position = Vector3.Lerp(atoms[0].transform.position, atoms[1].transform.position, 0.5f);
        this.transform.position = position;

        Vector3 direction = (atoms[0].transform.position - this.transform.position).normalized;
        this.transform.up = direction;

        return;
    }
}
