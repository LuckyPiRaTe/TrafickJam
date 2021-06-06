using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man_1 : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject man;
    public string status = "normal";

    public GameObject[] skins;

    public float spead = 0.3f;

    public Car car;
    void Start()
    {
        if (Random.Range(0, 2) == 0)
        {
            // Front
        }
        else {
            // Back
            man.transform.Rotate(new Vector3(0, 180, 0));
        }

        spead = (float)(Random.Range(1, 4)) / 10;

        int skin = Random.Range(0, skins.Length);
        skins[skin].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (car == null)
        {
            car = FindObjectOfType<Car>();
        }
        else { 
            
        }

        if (status == "normal")
        {
            man.transform.Translate(Vector3.forward * spead);
        }
        else if (status == "run") { 
        
        }
    }
}
