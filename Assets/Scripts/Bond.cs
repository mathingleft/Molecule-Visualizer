using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bond : MonoBehaviour
{
    public Transform bondTransform;

    public Transform getBondTransform()
    {
        return this.bondTransform;
    }

    public void setBondTransform(Transform newBondTransform)
    {
        this.bondTransform = newBondTransform;
    }
}
