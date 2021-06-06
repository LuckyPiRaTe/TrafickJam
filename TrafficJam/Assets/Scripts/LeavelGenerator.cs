using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavelGenerator : MonoBehaviour
{
    public Vector3 positionNowRoad;
    public GameObject car, camera;



    [SerializeField]
    public List<LeavelItem> locations;
    public int LocID;

    //Road
    public GameObject road_now;
    GameObject road_prev;
    public GameObject[] road_obj;

    //Cars block
    public GameObject cars_block_now;
    GameObject cars_block_prev;
    public GameObject cars_block_obj;

    //Shopes block
    public GameObject shopes_block_now;
    GameObject shopes_block_prev;
    public GameObject[] shopes_block_obj;


    public Vector3 sdvig;

    void Start()
    {
        sdvig = Vector3.forward * road_obj[0].transform.localScale.z;



        SetLocation();
    }

    void Update()
    {
        if (car == null) { car = FindObjectOfType<Car>().gameObject; }
        if (camera == null) { camera = FindObjectOfType<Camera_>().gameObject; }

        if (road_now == null) { road_now = GameObject.FindGameObjectWithTag("road_block"); }
        if (cars_block_now == null) { cars_block_now = GameObject.FindGameObjectWithTag("cars_block"); }
        if (shopes_block_now == null) { shopes_block_now = GameObject.FindGameObjectWithTag("shops_block"); }
    }


    public void SetLocation() {
        road_obj = locations[LocID].road_obj;
        cars_block_obj = locations[LocID].cars_block_obj;


        //Shop
        int rand_shop = Random.Range(0, shopes_block_obj.Length);
        shopes_block_obj = locations[LocID].shopes_block_obj;
        GameObject shop_new = Instantiate(shopes_block_obj[rand_shop]);
        shop_new.transform.position = shopes_block_now.transform.position;
        shop_new.transform.SetParent(shopes_block_now.transform.parent);
        Destroy(shopes_block_now);
        shopes_block_now = shop_new;
    }

    //Основная дорога
    void RoadGenerate() {
        if (road_prev != null) {
            Destroy(road_prev);
        }
        road_prev = road_now;
        //Сдвиг поля назад
        road_now.transform.position -= sdvig;
        //Сдвиг машинки назад
        car.transform.position -= sdvig;
        camera.transform.position -= sdvig;
        //Генерация следующего участка поля
        road_now = Instantiate(road_obj[Random.Range(0, road_obj.Length)], road_now.transform.position + sdvig, road_now.transform.rotation);
    }

    //Дорога с машинками слева
    void CarsBlockGenerate()
    {
        if (cars_block_prev != null)
        {
            Destroy(cars_block_prev);
        }
        cars_block_prev = cars_block_now;
        //Сдвиг поля назад
        cars_block_now.transform.position -= sdvig;
        //Генерация следующего участка поля
        cars_block_now = Instantiate(cars_block_obj, cars_block_now.transform.position + sdvig, cars_block_now.transform.rotation);
    }

    //Магазинчики справа
    void ShopsBlockGenerate()
    {
        if (shopes_block_prev != null)
        {
            Destroy(shopes_block_prev);
        }
        shopes_block_prev = shopes_block_now;
        //Сдвиг поля назад
        shopes_block_now.transform.position -= sdvig;
        //Генерация следующего участка поля
        int rand_shop = Random.Range(0, shopes_block_obj.Length);
        shopes_block_now = Instantiate(shopes_block_obj[rand_shop], shopes_block_now.transform.position + sdvig, shopes_block_now.transform.rotation);
    }

    public void Generate() {
        RoadGenerate();
        CarsBlockGenerate();
        ShopsBlockGenerate();
        Debug.Log("Generate");
    }
}