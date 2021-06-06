using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public Transform coinObj;

    void Start()
    {
        
    }

    void Update()
    {
        coinObj.Rotate(new Vector3(0, 0, Time.deltaTime * 300));
    }
}