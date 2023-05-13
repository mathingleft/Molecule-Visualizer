using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoleculeShape : MonoBehaviour
{
    public TMP_Text moleculeGeometryName;
    public TMP_Text electronGeometryName;
    string[] electronGeometryTypes = {
        "",
        "",
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
            "Octahedral", "Square Pyramidal", "Sqaure Planar", "T-Shaped", "Linear"}}
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
