using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddBar : MonoBehaviour
{
    int myFavoriteNumber = 42;
    string myName = "Chris";
    float myFloat = 1.254f;
    char characterExample = 'a';
    int[] myInts = new int[] {1,2,3,4,5,6,7,8,9};
    public List<GameObject> bars = new List<GameObject>();

    public GameObject bar;

    public void SpawnBar() {
        GameObject newBar = Instantiate(bar, Vector3.zero, Quaternion.identity);
        bars.Add(newBar);
        Destroy(bars[0]);
        bars.RemoveAt(0);
    }

    public static void SayHello()
    {
        print("Hello");
    }

    void Start()
    {
        foreach (GameObject gO in bars)
        {
            print(gO.transform.position);
        }
    }

    public void Update()
    {

    }
}
