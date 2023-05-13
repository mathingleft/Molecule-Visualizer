using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MoleculeSpawner : MonoBehaviour
{
    public GameObject atomPrefab;
    public GameObject singleBondPrefab;
    public GameObject doubleBondPrefab;
    public GameObject tripleBondPrefab;
    public GameObject lonePairPrefab;
    public List<GameObject> atoms = new List<GameObject>();
    public GameObject centralAtom;
    public List<GameObject> lonePairs = new List<GameObject>();

    void Awake()
    {
        //atoms.Add(centralAtom);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SpawnObject("atom");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnObject("single bond");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SpawnObject("double bond");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SpawnObject("triple bond");
        }
        */
    }

    public void SpawnObject(string obj)
    {
        GameObject spawnedObject = null;
        switch (obj)
        {
            case "atom":
                /*
                if (atoms.Count => 6) {
                    break;
                }
                */
                spawnedObject = Instantiate(atomPrefab, Vector3.zero, Quaternion.identity);
                atoms.Add(spawnedObject);
                break;
            case "single bond":
                spawnedObject = Instantiate(singleBondPrefab, Vector3.zero, Quaternion.identity);
                atoms.Add(spawnedObject);
                break;
            case "double bond":
                spawnedObject = Instantiate(doubleBondPrefab, Vector3.zero, Quaternion.identity);
                atoms.Add(spawnedObject);
                break;
            case "triple bond":
                spawnedObject = Instantiate(tripleBondPrefab, Vector3.zero, Quaternion.identity);
                atoms.Add(spawnedObject);
                break;
            case "lone pair":
                spawnedObject = Instantiate(lonePairPrefab, Vector3.zero, Quaternion.identity);
                lonePairs.Add(spawnedObject);
                break;
            default:
                print("Error: tried to spawn unknown object");
                break;
        }

        // Destroy(gameObject)
        
        /*
        if(spawnedObject != null)
        {
            for (int i = 0; spawnedObject.GetChild(i) is object; i++) {
                MeshRenderer objRenderer = spawnedObject.GetChild(i).GetComponent<MeshRenderer>();
                float redness = Random.Range(0f, 1f);
                float greenness = Random.Range(0f, 1f);
                float blueness = Random.Range(0f, 1f);
                float opacity = Random.Range(0f, 1f);
                Color color = new Color(redness, greenness, blueness, opacity);
                objRenderer.material.color = color;
            }
        }
        */
    }

    public void RemoveObject(string obj)
    {
        switch (obj)
        {
            case "atom":
                if (atoms.Count > 0)
                {
                    GameObject temp = atoms[atoms.Count - 1];
                    atoms.Remove(temp);
                    Destroy(temp);
                }
                break;
            case "single bond":
                break;
            case "double bond":
                break;
            case "triple bond":
                break;
            case "lone pair":
                break;
            default:
                print("Error: tried to spawn unknown object");
                break;
        }
    }
}
