using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBody : MonoBehaviour
{
    public Attractor centralAtom;
    public Transform fgbTrasnform;
    public Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        //rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        rigidBody.useGravity = false;
        fgbTrasnform = transform;
    }

    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */

    private void FixedUpdate()
    {
        centralAtom.attract(fgbTrasnform, rigidBody);
    }
}
