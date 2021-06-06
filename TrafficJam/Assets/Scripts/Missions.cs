using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missions : MonoBehaviour
{

    [System.Serializable]
    public class mission {
        public string name;
        public string descript;

        public int id;

        public int realized;
        public int maxRealizd;

        public bool compleated;
        public float lineEndAnim; //Анимация линии зачёркивания
    }
    // 1 - add coin;

    public List<mission> missions;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // Проверяем наличие миссии с id в списке
    bool idTest(int id) { for (int i = 0; i < missions.Count; i++) { if (missions[i].id == id) { return true; } } return false; }
    public void missionsGenerate() {
        missions = new List<mission>();
        int idMission = 0;
        int maxMissionsCount = 1;

        for (int i = 0; i < maxMissionsCount; i++)
        {
            while ((idMission == 0) || idTest(idMission)) { idMission = Random.Range(1, 2); }

            //Собрать монеты
            if (idMission == 1) { GenerateMis_Coin(); }
        }
    }

    //Собрать монеты
    void GenerateMis_Coin() {
        mission m = new mission();
        m.id = 1;
        m.maxRealizd = Random.Range(5, 10);
        m.name = "Собрать " + m.maxRealizd + " монет";

        missions.Add(m);
    }

    public void coinAdd() {
        for (int i = 0; i < missions.Count; i++) { if (missions[i].id == 1) { missions[i].realized++; if (missions[i].realized == missions[i].maxRealizd) { missions[i].compleated = true; } } }
    }
}