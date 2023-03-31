using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBar : MonoBehaviour
{
    int myFavoriteNumber = 42;
    public GameObject bar;

    public void SpawnBar() {
        Instantiate(bar, Vector3.zero, Quaternion.identity);
    }
}
