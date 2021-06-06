using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{

    GameManager gameManager;
    SaveObject saveObject;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        saveObject = new SaveObject();

        // Загрузка при старте
        //load_();
    }

    [System.Serializable]
    public class SaveObject {
        public int GOLD_COUNT, DIMOND_COUNT;

        public bool[] skinsActive;

        public int locationActive;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) {
            save_();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            load_();
        }
    }

    public void save_() {
        saveObject.GOLD_COUNT = gameManager.GOLD_COUNT;
        saveObject.DIMOND_COUNT = gameManager.DIMOND_COUNT;

        saveObject.skinsActive = new bool[gameManager.car.skinsCar.Count];
        for (int i = 0; i < saveObject.skinsActive.Length; i++) {
            saveObject.skinsActive[i] = gameManager.car.skinsCar[i].GetComponent<SkinCar>().Active;
        }

        PlayerPrefs.SetString("SaveGame", JsonUtility.ToJson(saveObject));
        Debug.Log("Saving");
    }

    public void load_()
    {
        if (!PlayerPrefs.HasKey("SaveGame")) { return; }

        string data_json = PlayerPrefs.GetString("SaveGame");
        saveObject = JsonUtility.FromJson<SaveObject>(data_json);

        gameManager.GOLD_COUNT = saveObject.GOLD_COUNT;
        gameManager.DIMOND_COUNT = saveObject.DIMOND_COUNT;

        for (int i = 0; i < saveObject.skinsActive.Length; i++)
        {
            gameManager.car.skinsCar[i].GetComponent<SkinCar>().Active = saveObject.skinsActive[i];
        }

        Debug.Log("Loading");
    }
}
