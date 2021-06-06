using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceMenu : MonoBehaviour
{

    public InterfaceGUI interfaceGUI;
    public GameManager gameManager;
    public Missions missions;

    public GUISkin skin1;

    float menuAnim = 1;

    public Texture2D circleT;

    bool clickedTest = false;


    void Start()
    {
        menuAnim = 1;
    }

    void Update()
    {
        if (menuAnim > 0) { menuAnim -= Time.deltaTime * 5; }
        if (menuAnim < 0) { menuAnim = 0; }
    }

    public void menu(float anim) {
        //0 - Слегка увеличить, сделать прозрачным
        //1 - поставить на место

        //--- Обнуление и инициализация значений ---
        interfaceGUI.animLoos = 0;
        MDS.ButtonTextureFree();
        Rect pos = new Rect();
        GUI.skin = skin1;
        clickedTest = false;
        //------------------------------------------

        // Тестовая кнопка
        GUI.color = MDS.ColorSetAlpha(Color.white, MDS.Absolute(anim) * 0.6f);
        pos = new Rect(20, Screen.height - Screen.height / 12 - 20, Screen.width - 40, Screen.height / 12);
        GUI.DrawTexture(pos, interfaceGUI.whiteT);
        GUI.skin.button.fontSize = (int)(Screen.height / 24);
        if (GUI.Button(pos, "Skins")) { interfaceGUI.SkinSelect(); clickedTest = true; }



        // Кнопка "ИГРАТь"
        GUI.color = MDS.ColorSetAlpha(Color.black, MDS.Absolute(anim) * 0.5f);
        GUI.DrawTexture(new Rect(0, Screen.height / 2 - Screen.width / 2, Screen.width, Screen.width), circleT);
        /*
        pos = MDS.RectAnim(MDS.CentrScreen(),
                                new Rect(Screen.width / 2 - Screen.width / 2 / 2, Screen.height / 2 - Screen.height / 6 / 2, Screen.width / 2, Screen.height / 6),
                                MDS.FloatAnim(1.3f, 1, anim));
        */
        pos = MDS.SizeScreen();

        GUI.color = MDS.ColorSetAlpha(Color.white, MDS.Absolute(anim) * 0.6f);
        GUI.skin.label.fontSize = (int)(Screen.height/15 - 10 * anim);
        GUI.Label(pos, "Tap to play");
        if (GUI.Button(MDS.SizeScreen(), "") && !clickedTest) {
            interfaceGUI.SetStatusPlay();


        }







        // Затемнение экрана
        GUI.color = MDS.ColorSetAlpha(Color.black, menuAnim);
        GUI.DrawTexture(MDS.SizeScreen(), interfaceGUI.whiteT);
    }
}