using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //System
    public BlockSpawnerManager spawnerManager;
    public InterfaceGUI interfaceGUI;
    public string GameStatus = "menu";

    public SaveData SaveData;

    public Missions missions;

    public Car car;
    public Polition polition;

    public int GOLD_COUNT, DIMOND_COUNT;

    //Game
    public string sceneNow = "clasic";
    public float scoreNow;
    public float coinsLeavel, coinsAll;

    public int SkinID = 0;

    public List<Transform> lines;

    void Start()
    {
        SaveData = GetComponent<SaveData>();

        //DontDestroyObj
        GameManager[] objs = GameObject.FindObjectsOfType<GameManager>();
        for (int i = 0; i < objs.Length; i++) {
            //if (objs[i].gameObject != gameObject) { Destroy(objs[i].gameObject); }
            
        }
        if (objs.Length > 1) { Destroy(gameObject); }
        DontDestroyOnLoad(transform.gameObject);

        startInicialis();
    }

    [System.Serializable]
    public class Save {
        public string test = "TEST";
    }
    public Save saveObj;



    public SkinCar getCurrentSkin() {
        return car.skinsCar[SkinID].GetComponent<SkinCar>();
    }

    public void startInicialis() {
        spawnerManager.startInicialis();
    }
    public void CarInicialise(Car car_) {
        Debug.Log("new car");
        car = car_;
        spawnerManager.CarInicialise(car_);

        Debug.Log(SkinID + "");
        car.ActiveSkin(SkinID);
    }


    public void save() {
        string json = JsonUtility.ToJson(saveObj);
        PlayerPrefs.SetString("Data", json);

        Debug.Log(json);
    }
    public void load() {
        string json = PlayerPrefs.GetString("Data");
        saveObj = JsonUtility.FromJson<Save>(json);
    }

    void Update()
    {
        if (saveObj == null) { saveObj = new Save(); }
        if (Input.GetKeyDown(KeyCode.Space)) { save(); }
        if (Input.GetKeyDown(KeyCode.L)) { load(); }


        if (car == null) {
            car = FindObjectOfType<Car>();
            car.ActiveSkin(SkinID);

            interfaceGUI.ISkin.skinsLineCreate();

            SaveData.load_();
        }

        if (GameStatus == "play") {
            scoreNow += Time.deltaTime;
        }
    }

    public void SetStatusPlay() {
        GameStatus = "play";
        car.startGame();
    }


    // Перезагрузка сцены
    public void loadGame() {
        if (sceneNow == "clasic") { Application.LoadLevel("game"); }
    }

    // Открыть окно выбора скина
    public void SkinSelect()
    {
        GameStatus = "skin";
    }
}