using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinCar : MonoBehaviour
{
    [Multiline(5)]
    public string descript;


    public bool Active;
    public int costGold;
    public int costDimond;


    [System.Serializable]
    public class Skill {
        public string skillName;
        public int intData;
        public float floatData;
    }

    [System.Serializable]
    public class Skills
    {
        public Skill[] skills;


        public bool testIn(string name)
        {
            for (int i = 0; i < skills.Length; i++) { if (skills[i].skillName == name) { return true; } }
            return false;
        }

        public Skill getSkill(string name) {
            for (int i = 0; i < skills.Length; i++) { if (skills[i].skillName == name) { return skills[i]; } }
            return null;
        }
    }

    public Skills skills;
}