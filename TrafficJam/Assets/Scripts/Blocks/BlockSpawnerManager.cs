using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawnerManager : MonoBehaviour
{

    public Transform spawner;
    public Transform car;

    public Transform leftSpawn, rightSpawn;
    public LeavelGenerator leavelGenerator;

    public GameManager gameManager;

    string rand_block;

    public List<GameObject> blocks;

    float timer;


    //Ремонт - длинный блок
    public GameObject blockRemont;
    public int ves_blockRemont;

    //Шипы
    public GameObject blockShips;
    public int ves_blockShips;

    //Гидрант
    public GameObject blockGidrant;
    public int ves_gidrant;

    //Люди
    public GameObject blockMan_1;
    float timerMans;


    //Items
    public GameObject CoinItem;

    void Start()
    {
        
    }

    public void startInicialis()
    {
        gameManager = FindObjectOfType<GameManager>();
        leavelGenerator = FindObjectOfType<LeavelGenerator>();
        car = FindObjectOfType<Car>().transform;

        spawner = transform.GetChild(0);
    }
    public void CarInicialise(Car car_)
    {
        car = car_.transform;
    }

    string block_random() {
        int b = Random.Range(0, ves_blockRemont + ves_gidrant + ves_blockShips + 10);

        //Стандартная приграда (бочка, коробка и т.д.)
        //if(b)

        // Ремонт участка
        if (b < ves_blockRemont)
        {
            return "blockRemont";
        }

        // Ремонт участка
        if ((ves_blockRemont <= b) && (b < ves_blockRemont + ves_gidrant))
        {
            return "gidrant";
        }

        // Шипы
        if ((ves_blockRemont + ves_gidrant <= b) && (b < ves_blockRemont + ves_gidrant + ves_blockShips))
        {
            return "ships";
        }

        return "";
    }

    Vector3 randPos() {
        float k = Random.Range(0, 100);
        k = k / 100;
        return new Vector3( MDS.FloatAnim(leftSpawn.position.x, rightSpawn.position.x, k), spawner.position.y, spawner.position.z);
    }

    void spawn_blockRemont()
    {
        GameObject block = GameObject.Instantiate(blockRemont);
        block.transform.SetParent(leavelGenerator.road_now.transform);
        block.transform.position = randPos();

        // Особые правила появления (например позиции)
        if (block.GetComponent<LongBlockRuls>()) { block.GetComponent<LongBlockRuls>().Rule(GetComponent<BlockSpawnerManager>()); }
    }


    void spawn_shpis()
    {
        GameObject block = GameObject.Instantiate(blockShips);
        block.transform.SetParent(leavelGenerator.road_now.transform);
        block.transform.position = new Vector3(leftSpawn.position.x + 4, spawner.position.y, spawner.position.z);
    }

    //luke
    void spawn_luke()
    {
        GameObject block = GameObject.Instantiate(blocks[0]);
        block.transform.SetParent(leavelGenerator.road_now.transform);
        block.transform.position = randPos();


        // Монетки
        GameObject coin = GameObject.Instantiate(CoinItem);
        coin.transform.SetParent(leavelGenerator.road_now.transform);
        coin.transform.position = new Vector3(randPos().x, spawner.position.y, spawner.position.z + 10);
    }

    //gidrant
    void spawn_gidrant()
    {
        GameObject block = GameObject.Instantiate(blockGidrant);
        block.transform.SetParent(leavelGenerator.road_now.transform);
        block.transform.position = randPos();
        block.transform.position = new Vector3(block.transform.position.x, block.transform.position.y, block.transform.position.z + 5);
    }


    void spawn_mans() {
        GameObject block = GameObject.Instantiate(blockMan_1);
        block.transform.position = randPos();
        //block.transform.rotation = new Quaternion(Random.Range(-1, 1), 0, Random.Range(-1, 1), block.transform.rotation.w);
        //block.transform.Rotate(new Vector3(0, Random.Range(-10, 10), 0));
        block.transform.SetParent(leavelGenerator.road_now.transform);
        //block.transform.position = new Vector3(block.transform.position.x, block.transform.position.y, block.transform.position.z + 5);
    }

    void Update()
    {
        if (gameManager.GameStatus == "play") {
            timer += Time.deltaTime;
            if (timer >= 1.5f)
            {
                spawn_luke();

                // Случайный выбор спавна припядствий
                rand_block = block_random();
                // Спавн ремонтного участка
                if (rand_block == "blockRemont") { spawn_blockRemont(); }
                if (rand_block == "gidrant") { spawn_gidrant(); }
                if (rand_block == "ships") { spawn_shpis(); }


                timer = 0;
            }

            timerMans += Time.deltaTime;
            if (timerMans >= 0.8f) {
                spawn_mans();

                timerMans = 0;
            }
        }


        if (car == null)
        {
            car = FindObjectOfType<Car>().transform;
        }
    }

    void FixedUpdate() {
        if (car == null)
        {
            car = FindObjectOfType<Car>().transform;
        }
        else {
            spawner.position = new Vector3(spawner.position.x, spawner.position.y, car.position.z + 100);
        }
        
    }
}