using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DegreeHandler : MonoBehaviour
{
    public List<GameObject> bonds = new List<GameObject>();
    public List<Degree> degrees = new List<Degree>();
    public GameObject degreePrefab;

    // Start is called before the first frame update
    void Start()
    {  
        for (int i = 0; i < bonds.Count - 1; i++)
        {
            Transform transform1 = bonds[i].transform;
            for (int j = i + 1; j < bonds.Count; j++)
            {
                Transform transform2 = bonds[j].transform;
                // instantiate text object from prefab
                TMP_Text t = Instantiate(degreePrefab).GetComponent<TMP_Text>();
                // create new degree object for the new prefab
                Degree d = new Degree(transform1, transform2, t);
                // add it to degrees list
                degrees.Add(d);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Degree degree in degrees)
        {
            degree.setDegrees();
            degree.setMiddleVector();
            degree.setColor();
        }
    }

    public class Degree
    {
        Transform transform1;
        Transform transform2;
        TMP_Text text;

        public Degree(Transform transform1, Transform transform2, TMP_Text text) {
            this.transform1 = transform1;
            this.transform2 = transform2;
            this.text = text;
        }

        public void setDegrees()
        {
            float angle = Vector3.Angle(transform1.position,transform2.position);
            text.text = angle.ToString("F") + "°";
        }

        public void setMiddleVector() {
            float x = (transform1.position.x + transform2.position.x) / 2;
            float y = (transform1.position.y + transform2.position.y) / 2;
            float z = (transform1.position.z + transform2.position.z) / 2;
            text.gameObject.transform.position = new Vector3(x, y, z);
        }

        public void setColor() {
            Color color1 = new Color(text.color.r, text.color.g, text.color.b, 1f);
            Color color2 = new Color(text.color.r, text.color.g, text.color.b, 0f);

            float progress = text.gameObject.transform.position.z * -1f;

            text.color = Color.Lerp(color2, color1, progress);
        }
    }
}
