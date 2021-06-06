using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polition : MonoBehaviour
{
    public GameManager gameManager;

    public string barier_type;

    public Transform car, posSpawn;

    public Transform car_1, car_1_arrow;
    public Transform pointLeft_1, pointRight_1, pointBack_1;

    void Start()
    {
        
    }

    void Update()
    {
        if (gameManager == null) { gameManager = FindObjectOfType<GameManager>(); gameManager.polition = GetComponent<Polition>(); }

        if (gameManager.GameStatus == "loos") {
            // Врезался в левую стенку
            if (barier_type == "Barier_left") {
                car_1.position = Vector3.Lerp(car_1.position, pointRight_1.position, Time.deltaTime * 5);
                car_1_arrow.LookAt(pointRight_1);
                car_1.rotation = new Quaternion(car_1.rotation.x, car_1_arrow.rotation.y, car_1.rotation.z, car_1.rotation.w);
            }
            else
            // Врезался в правую стенку
            if (barier_type == "Barier_right")
            {
                car_1.position = Vector3.Lerp(car_1.position, pointLeft_1.position, Time.deltaTime * 5);
                car_1_arrow.LookAt(pointLeft_1);
                car_1.rotation = new Quaternion(car_1.rotation.x, car_1_arrow.rotation.y, car_1.rotation.z, car_1.rotation.w);
            }
            else
            // Врезался в прямую стенку
            if (barier_type == "Barier")
            {
                car_1.position = Vector3.Lerp(car_1.position, pointBack_1.position, Time.deltaTime * 5);
                car_1_arrow.LookAt(pointBack_1);
                car_1.rotation = new Quaternion(car_1.rotation.x, car_1_arrow.rotation.y, car_1.rotation.z, car_1.rotation.w);
            }
        }
    }

    public void cars_set_pos() {
        car_1.position = new Vector3(posSpawn.position.x, car_1.position.y, posSpawn.position.z);
    }
}