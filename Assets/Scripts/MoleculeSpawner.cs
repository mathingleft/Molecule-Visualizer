using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MoleculeSpawner : MonoBehaviour
{
    public GameObject singleBondPrefab;
    public GameObject doubleBondPrefab;
    public GameObject tripleBondPrefab;
    public GameObject lonePairPrefab;
    public GameObject atomPrefab;
    public List<GameObject> atoms = new List<GameObject>();
    public List<GameObject> bonds = new List<GameObject>();
    public List<GameObject> lonePairs = new List<GameObject>();
    public GameObject centralAtom;
    public delegate void ChangeName(int bondNum, int lonePairNum);
    public static event ChangeName OnNameChange;
    public delegate void SpawnBond(GameObject newBond);
    public static event SpawnBond OnSpawnBond;
    public delegate void RemoveBond(GameObject oldBond);
    public static event RemoveBond OnRemoveBond;
    public delegate int FixBrokenCases(int bondNum, int lonePairNum);
    public static event FixBrokenCases OnFixBrokenCases;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        centralAtom = GameObject.FindWithTag("Central Atom");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnObject(string obj)
    {
        if (atoms.Count + lonePairs.Count >= 6)
        {
            return;
        }

        GameObject spawnedObject = null;
        Vector3 spawnLocation = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f,1f));

        if (obj == "Lone Pair")
        {
            spawnedObject = Instantiate(lonePairPrefab, spawnLocation, Quaternion.identity);
            lonePairs.Add(spawnedObject);
        }
        else
        {
            Dictionary<string, GameObject> bondPrefabs = new Dictionary<string, GameObject>(){
                {"Single Bond", singleBondPrefab},
                {"Double Bond", doubleBondPrefab},
                {"Triple Bond", tripleBondPrefab},
                {"Lone Pair", lonePairPrefab}
            };

            spawnedObject = Instantiate(atomPrefab, spawnLocation, Quaternion.identity);
            atoms.Add(spawnedObject);

            GameObject bondPrefab = bondPrefabs[obj];
            GameObject bond = Instantiate(bondPrefab, Vector3.zero, Quaternion.identity);
            bonds.Add(bond);
            bond.GetComponent<BondPosition>().assignAtoms(centralAtom, spawnedObject);
        }

        /*
        switch (obj)
        {
            case "Single Bond":
                spawnedObject = Instantiate(singleBondPrefab, spawnLocation, Quaternion.identity);
                atoms.Add(spawnedObject);
                break;
            case "Double Bond":
                spawnedObject = Instantiate(doubleBondPrefab, spawnLocation, Quaternion.identity);
                atoms.Add(spawnedObject);
                break;
            case "Triple Bond":
                spawnedObject = Instantiate(tripleBondPrefab, spawnLocation, Quaternion.identity);
                atoms.Add(spawnedObject);
                break;
            case "Lone Pair":
                spawnedObject = Instantiate(lonePairPrefab, spawnLocation, Quaternion.identity);
                lonePairs.Add(spawnedObject);
                break;
            default:
                print("Error: tried to spawn an unknown object");
                break;
        }
        */


        //spawnedObject.GetComponentInChildren<FauxGravityBody>().centralAtom = centralAtom.GetComponent<Attractor>();
        OnSpawnBond(spawnedObject);

        OnNameChange(atoms.Count, lonePairs.Count);
        OnFixBrokenCases(atoms.Count, lonePairs.Count);
    }

    public void RemoveObject(string obj)
    {
        if (obj == "Lone Pair" && lonePairs.Count <= 0)
        {
            return;
        }

        if (obj != "Lone Pair" && atoms.Count <= 0)
        {
            return;
        }

        /* 
         * Look through bonds list
         * if tag == tag:
         *   delete
         */
        // Debug.Log("try to delete " + obj);

        switch (obj)
        {
            case "Single Bond":
            case "Double Bond":
            case "Triple Bond":
                foreach (GameObject bond in bonds)
                {
                    if (bond.tag == obj)
                    {
                        GameObject atom = bond.GetComponent<BondPosition>().atoms[1];
                        atoms.Remove(atom);
                        OnRemoveBond(atom);
                        Destroy(atom);
                        bonds.Remove(bond);
                        Destroy(bond);
                        break;
                    }
                }
                break;
            case "Lone Pair":
                GameObject lonePair = lonePairs[lonePairs.Count - 1];
                lonePairs.Remove(lonePair);
                OnRemoveBond(lonePair);
                Destroy(lonePair);
                break;
            default:
                print("Error: tried to delete an unknown object");
                break;
        }


        OnNameChange(atoms.Count, lonePairs.Count);
    }
}
