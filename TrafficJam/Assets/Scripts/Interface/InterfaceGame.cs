using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class InterfaceGame : MonoBehaviour
{
    public GameManager gameManager;
    public InterfaceGUI interfaceGUI;

    float missionAnim;


    void Start()
    {
        
    }

    void Update()
    {
        if (gameManager.GameStatus != "play")
        {
            if (missionAnim < 3) { missionAnim = 3; }
        }
        else {
            if (missionAnim > 0) { missionAnim -= Time.deltaTime; }
            if (missionAnim < 0) { missionAnim = 0; }
        }
    }


    float animPreobraz() {
        if (missionAnim > 2) { return (3 - missionAnim); }
        if ((missionAnim > 1) && (missionAnim <= 2)) { return 1; }
        if (missionAnim <= 1) { return missionAnim; }

        return 0;
    }


    public void interfaceItems(float anim) {
        GUI.color = MDS.ColorSetAlpha(Color.white, anim);

        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUI.skin.label.fontSize = (int)(Screen.height/7);
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height / 5), "" + Mathf.Floor(gameManager.coinsLeavel));


        string text = "";
        for (int i = 0; i < gameManager.missions.missions.Count; i++)
        {
            text += gameManager.missions.missions[i].name + "\n";
        }
        GUI.skin.label.fontSize = (int)(Screen.height / 20);
        GUI.color = MDS.ColorSetAlpha(Color.black, anim * animPreobraz() * 0.8f);
        GUI.Label(MDS.SizeScreen(), text);
    }
}