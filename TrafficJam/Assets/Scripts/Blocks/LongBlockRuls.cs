using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongBlockRuls : MonoBehaviour
{
    public Transform LegtBorder, RightBorder;
    public Car car;

    public bool end;

    void Start()
    {
        car = GameObject.FindObjectOfType<Car>();
    }

    public void Rule(BlockSpawnerManager spawnerManager) {
        if (LegtBorder.position.x < spawnerManager.leftSpawn.position.x) {
            transform.position = new Vector3(spawnerManager.leftSpawn.position.x + 3, transform.position.y, transform.position.z);
        }

        if (RightBorder.position.x > ((spawnerManager.rightSpawn.position.x - spawnerManager.leftSpawn.position.x)/2 + spawnerManager.leftSpawn.position.x))
        {
            transform.position = new Vector3(((spawnerManager.rightSpawn.position.x - spawnerManager.leftSpawn.position.x) / 2 + spawnerManager.leftSpawn.position.x) - 3, transform.position.y, transform.position.z);
        }
    }


    public void OnGUI() {
        /*
        if (end) { return; }
        if (car.transform.position.z > (transform.position.z + 5)) { end = true; }

        GUI.color = Color.white;
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        pos.y = Screen.height - pos.y;
        if (pos.y < 40) { pos.y = 40; }
        GUI.DrawTexture(new Rect(pos.x - 50, pos.y, 100, 100), MDS.white);
        */
    }
}
