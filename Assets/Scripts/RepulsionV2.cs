using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepulsionV2 : MonoBehaviour
{
    float repulsionConstant = 200f;
    float likeRepulsionMod = 1.0f;
    float unlikeRepulsionMod = 1.0f;
    float bondDistanceConstant = 3f;
    public Rigidbody rigidbody;
    public List<Rigidbody> otherAtoms = new List<Rigidbody>();
    public List<Rigidbody> bondedAtoms = new List<Rigidbody>();
    public static float[,] brokenMultipliers = new float[,]
    {
        {1.0f,1.0f,1.0f},
        {1.0f,1.0f,1.0f}
    };


    void Awake()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Find the repulsion FROM other atoms
        Vector3 repulsionFromOtherAtoms = new Vector3(0f, 0f, 0f);
        //   if the atom this script is running on is a central atom, do not repel from other atoms
        if (rigidbody.CompareTag("Central Atom") || rigidbody.gameObject == UserRotation.selected)
        {

        }
        else {
            repulsionFromOtherAtoms = getRepulsionFromOtherAtoms();
        }

        // Add the repulsion FROM the other atoms
        Vector3 adjustedRepulsionFromOtherAtoms = getAdjustedRepulsionFromOtherAtoms(repulsionFromOtherAtoms);
        rigidbody.AddForce(adjustedRepulsionFromOtherAtoms);

        /*
        Debug.Log(rigidbody);
        Debug.Log("repulsionFromOtherAtoms: " + repulsionFromOtherAtoms);
        Debug.Log("adjustedRepulsionFromOtherAtoms: " + adjustedRepulsionFromOtherAtoms);
        Debug.Log("----------------------");
        */

        // Make sure the bonded atoms are a constant distance from each other
        //   This will be done by changing the position of the bonded atoms
        //   This will function as the attraction script previously
        foreach (Rigidbody bondedAtom in bondedAtoms)
        {
            if (bondedAtom.CompareTag("Central Atom"))
            {
                // don't move the central atom
            } 
            else
            {
                Vector3 distanceVector = bondedAtom.position - transform.position;

                // Direction Vector must be...
                //   in the direction of the bonded atom from the atom
                //   normalized to keep scale
                Vector3 distanceDirection = distanceVector.normalized;

                // The fixed distance vector must be...
                //   in the direction of distanceDirection
                //   scaled to a constant distance
                Vector3 fixedDistanceVector = distanceDirection * bondDistanceConstant;

                // The bonded atom's position will be...
                //   at the atom's position, but offset by a the fixed distance vector
                bondedAtom.position = transform.position + fixedDistanceVector;
            }
        }
        
        // Make sure the bonded atoms' outward velocity is reset
        foreach (Rigidbody bondedAtom in bondedAtoms)
        {
            Vector3 velocityVector = bondedAtom.velocity;
            Vector3 negVelocityVector = -velocityVector;

            Vector3 distanceVector = transform.position - bondedAtom.position; // from bonded atom to transform
            Vector3 distanceDirection = distanceVector.normalized; // velocityChange will be parallel to this

            float velocityChangeMagnitude = Vector3.Dot(negVelocityVector, distanceDirection); // how much to change velocity by
            Vector3 velocityChange = distanceDirection * velocityChangeMagnitude; // what to change velocity
            Vector3 newVelocityVector = velocityVector + velocityChange; // new velocity of bondedAtom

            bondedAtom.velocity = newVelocityVector;
        }
    }

    private Vector3 getRepulsionFromOtherAtoms()
    {
        Vector3 repulsionFromOtherAtoms = new Vector3(0f, 0f, 0f);
        foreach (Rigidbody otherAtom in otherAtoms)
        {
            if (otherAtom == UserRotation.selected)
            {
                continue;
            }

            Vector3 distanceVector = transform.position - otherAtom.position;
            float distanceMagnitude = distanceVector.magnitude;

            // Repulsion force acts on an inverse square relationship to distance
            //   If distance is halved (0.5x less), then repel 4x more
            float repulsionMagnitude = (1f) / (Mathf.Pow(distanceMagnitude, 2));

            // Direction Vector must be...
            //   the direction of the atom from the other atom
            //   normalized to keep scale
            Vector3 directionVector = distanceVector.normalized;

            // The repulsion from the other atom is...
            //   in the direction of the directionVector
            //   scaled relative to distance
            //   scaled to a value that can be adjusted
            Vector3 repulsionFromOtherAtom = directionVector * repulsionMagnitude * repulsionConstant;

            /*
            // repel atom-atom and lone pair-lone pair more to stop them from clumping
            //   has a fault with breaking angles tho
            if (transform.gameObject.tag == otherAtom.gameObject.tag)
            {
                repulsionFromOtherAtom *= 1.3f;
            }
            */

            if (otherAtom.gameObject.tag == "Lone Pair")
            {
                repulsionFromOtherAtom *= 1.3f;
            }

            // The repulsion from all other atoms is the sum of all the repulsions from the other atoms
            repulsionFromOtherAtoms += repulsionFromOtherAtom;
        }

        return repulsionFromOtherAtoms;
    }

    private Vector3 getAdjustedRepulsionFromOtherAtoms(Vector3 repulsionFromOtherAtoms)
    {
        // To add drag...
        //   when the velocity and force are in different directions (when dot product < 0)
        //     increase the force to try to reach velocity of 0 faster
        //   when the velocity and force are in the same direction (when dot product > 0)
        //     decrease change the force

        float dotProduct = Vector3.Dot(rigidbody.velocity, repulsionFromOtherAtoms);
        float forceAdjustment = Mathf.Lerp(3f, 0.2f, Mathf.Pow(dotProduct + 1, 3)/8);
        Vector3 adjustedRepulsionFromOtherAtoms = repulsionFromOtherAtoms * forceAdjustment;
        return adjustedRepulsionFromOtherAtoms;
    }

    public int getMultiplier(int atomNum, int lonePairNum)
    {
        int brokenCase = -1;
        if (atomNum == 3)
        {
            if (lonePairNum == 2)
            {
                brokenCase = 0;
            }
            if (lonePairNum == 3)
            {
                brokenCase = 1;
            }
        }

        return brokenCase;
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb;
        if (other.GetComponent<Rigidbody>())
        {
            rb = other.GetComponent<Rigidbody>();
        }
        else
        {
            return;
        }

        if (otherAtoms.Contains(rb))
        {
            return;
        }
        if (other.CompareTag("Atom"))
        {
            otherAtoms.Add(rb);
        }
        if (other.CompareTag("Lone Pair"))
        {
            otherAtoms.Add(rb);
        }
        if (other.CompareTag("Bond"))
        {
            otherAtoms.Add(rb);
        }
        if (other.CompareTag("Central Atom"))
        {
            otherAtoms.Add(rb);
            if (!bondedAtoms.Contains(rb))
            {
                bondedAtoms.Add(rb);
            }
        }
        if (this.CompareTag("Central Atom") && !bondedAtoms.Contains(rb))
        {
            bondedAtoms.Add(rb);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (other.CompareTag("Atom"))
        {
            otherAtoms.Remove(rb);
        }
        if (other.CompareTag("Lone Pair"))
        {
            otherAtoms.Remove(rb);
        }
        if (other.CompareTag("Bond"))
        {
            otherAtoms.Remove(rb);
        }
        if (other.CompareTag("Central Atom"))
        {
            otherAtoms.Remove(rb);
        }
    }

    public void OnEnable()
    {
        MoleculeSpawner.OnRemoveBond += RigidBodyCleanup;
        MoleculeSpawner.OnFixBrokenCases += getMultiplier;
    }

    public void OnDisable()
    {
        MoleculeSpawner.OnRemoveBond -= RigidBodyCleanup;
        MoleculeSpawner.OnFixBrokenCases -= getMultiplier;
    }

    public void RigidBodyCleanup(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();

        if (rb == null)
        {
            return;
        }

        if (otherAtoms.Contains(rb))
        {
            otherAtoms.Remove(rb);
        }
        if (bondedAtoms.Contains(rb))
        {
            bondedAtoms.Remove(rb);
        }
    }
}
