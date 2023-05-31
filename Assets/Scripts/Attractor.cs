using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    float gravity = -100f;
    public float amount = 1f;

    public void attract(Transform body, Rigidbody rb) { 
        Vector3 gravityUp = (body.position - transform.position).normalized;
        Vector3 bodyUp = body.up;
        rb.AddForce(gravityUp * gravity);
        Quaternion targetRot = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation, targetRot, amount * Time.deltaTime);
    }
}
