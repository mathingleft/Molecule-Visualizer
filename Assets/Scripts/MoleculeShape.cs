using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoleculeShape : MonoBehaviour
{
    public TMP_Text electronGeometryName;
    public TMP_Text moleculeGeometryName;

    string[] electronGeometryTypes = {
        "None",
        "None",
        "Linear",
        "Trigonal Planar",
        "Tetrahedral",
        "Trigonal Bipyramidal",
        "Octahedral"
    };

    /*
    string[] electronGeometryTypes =
    {
        
    };
    */

    Dictionary<string, string[]> molecularGeometryTypes = new Dictionary<string, string[]>()
    {
        {"Linear", new string[]{
            "Linear"}},
        {"Trigonal Planar", new string[]{
            "Trigonal Planar", "Bent"}},
        {"Tetrahedral", new string[]{
            "Tetrahedral", "Trigonal Pyramidal","Bent"}},
        {"Trigonal Bipyramidal", new string[]{
            "Trigonal Bipyramidal", "Seesaw", "T-Shaped", "Linear"}},
        {"Octahedral", new string[]{
            "Octahedral", "Square Pyramidal", "Sqaure Planar", "T-Shaped", "Linear"}},
        {"None", new string[]{
            "None"}}
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        MoleculeSpawner.OnNameChange += moleculeNameUpdate;
    }

    void OnDisable()
    {
        MoleculeSpawner.OnNameChange -= moleculeNameUpdate;
    }

    public void moleculeNameUpdate(int bondNum, int lonePairNum)
    {
        string electronGeometry = electronGeometryTypes[bondNum + lonePairNum];
        electronGeometryName.text = electronGeometry;
        moleculeGeometryName.text = molecularGeometryTypes[electronGeometry][lonePairNum];
    }
}
