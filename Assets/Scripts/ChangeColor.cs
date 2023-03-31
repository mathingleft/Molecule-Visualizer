using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        
    }

    private void OnTriggerEnter(Collider other) {
        other.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    public void PrintString(string s) {
        print(s);
    }
}
