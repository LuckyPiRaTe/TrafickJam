using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceLoos : MonoBehaviour
{

    public InterfaceGUI interfaceGUI;
    public GameManager gameManager;
    public Missions missions;


    float lineAnim;

    float menuAnim;
    bool toMenu;


    //Когда выполнил все миссии
    float missionCompleatedAnim;
    int missionCompleatedAnim_steps = 0;


    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    bool missionCompleatedTest() {
        for (int i = 0; i < missions.missions.Count; i++) { if (!missions.missions[i].compleated) { return false; } }
        return true;
    }


    void Update()
    {

        if (gameManager.GameStatus == "loos")
        {
            if (lineAnim < missions.missions.Count) { lineAnim += Time.deltaTime * 5; }

            // Анимация зачёркивания миссий
            for (int i = 0; i < missions.missions.Count; i++)
            {
                if (missions.missions[i].compleated && (lineAnim >= i))
                {
                    if (missions.missions[i].lineEndAnim < 1) { missions.missions[i].lineEndAnim += Time.deltaTime * 2; }
                    if (missions.missions[i].lineEndAnim > 1) { missions.missions[i].lineEndAnim = 1; }
                }
            }

            //Когда отыгралась последняя анимация
            if (missions.missions[missions.missions.Count - 1].lineEndAnim >= 1)
            {
                // Если завершились все миссии
                if ((missionCompleatedAnim_steps == 0) && (missionCompleatedTest()))
                {
                    missionCompleatedAnim_steps = 1;
                }
            }

            // Анимация смена миссий
            if (missionCompleatedAnim_steps == 1)
            {
                if (missionCompleatedAnim < 1) { missionCompleatedAnim += Time.deltaTime * 5; }
                if (missionCompleatedAnim >= 1)
                {
                    missionCompleatedAnim = 1;
                    //Следующий шаг
                    missionCompleatedAnim_steps = 2;
                    //Создать новые миссии
                    missions.missionsGenerate();
                }
            }
            else if (missionCompleatedAnim_steps == 2)
            {
                if (missionCompleatedAnim > 0) { missionCompleatedAnim -= Time.deltaTime * 5; }
                if (missionCompleatedAnim <= 0)
                {
                    missionCompleatedAnim = 0;
                    missionCompleatedAnim_steps = 0;
                }
            }
        }
        else {
            lineAnim = 0;
            menuAnim = 0;
        }

        if (toMenu) {
            if (menuAnim < 1) { menuAnim += Time.deltaTime * 5; }
            if (menuAnim >= 1) {
                toMenu = false;

                gameManager.loadGame();
                //Application.LoadLevel("game");

                //interfaceGUI.animLoos = 0;
                gameManager.GameStatus = "menu";
                gameManager.startInicialis();
                //menuAnim = 0;
            }
        }
    }

    void missionsClean() {
        for (int i = 0; i < missions.missions.Count; i++)
        {
            missions.missions[i].realized = 0;
        }
    }


    // Перезагрука сцены

    void MissionText(Rect rect, string text, Texture2D whiteT, float anim, float animLine) {
        GUI.color = MDS.ColorSetAlpha(Color.white, 0.7f * anim * (1 - missionCompleatedAnim));
        GUI.skin.label.fontSize = (int)(Screen.height / 30);
        GUI.skin.label.fontStyle = FontStyle.Normal;
        GUI.Label(rect, text);

        float len = text.Length * GUI.skin.label.fontSize * 0.7f + 10;
        GUI.DrawTexture(new Rect(rect.x + rect.width/2 - len /2 - 5, rect.y + rect.height/2 + 2, len * animLine, 3), whiteT);
    }

    public void loos(float anim) {

        GUI.color = MDS.ColorSetAlpha(Color.black, 0.7f * anim);
        GUI.DrawTexture(MDS.SizeScreen(), interfaceGUI.whiteT);

        Rect rect = new Rect(Screen.width / 2 - Screen.width / 5 * 3 / 2, Screen.height - Screen.height / 8 - 10, Screen.width / 5 * 3, Screen.height / 8);

        GUI.color = MDS.ColorSetAlpha(Color.white, 0.3f * anim);
        GUI.DrawTexture(rect, interfaceGUI.whiteT);
        if (GUI.Button(rect, "")) {
            gameManager.coinsAll += gameManager.coinsLeavel;
            gameManager.coinsLeavel = 0;

            // Обнуление прогресса миссий
            missionsClean();

            toMenu = true;
        }

        GUI.color = MDS.ColorSetAlpha(Color.black, 0.7f * anim);
        GUI.skin.label.fontSize = (int)(Screen.height/24);
        GUI.skin.label.fontStyle = FontStyle.Bold;
        GUI.Label(rect, "Menu");


        rect = new Rect(10, Screen.height / 2 - Screen.height / 8 / 2, Screen.width - 20, Screen.height / 8);
        rect.y = Screen.height / 2 - (rect.height + 5) * missions.missions.Count / 2 - (Screen.height/8 * missionCompleatedAnim)/*Анимация смены миссий*/;


        for (int i = 0; i < missions.missions.Count; i++) {
            MissionText(rect, missions.missions[i].name, interfaceGUI.whiteT, anim, missions.missions[i].lineEndAnim);

            rect.y += rect.height + 5;
        }


        GUI.color = MDS.ColorSetAlpha(Color.black, menuAnim);
        GUI.DrawTexture(MDS.SizeScreen(), interfaceGUI.whiteT);
    }
}