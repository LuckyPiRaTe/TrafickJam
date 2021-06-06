using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceSkin : MonoBehaviour
{

    public bool locationsMenuTest;
    float locationsMenuAnim;

    public InterfaceGUI interfaceGUI;
    public GameManager gameManager;
    public MDS mds;
    public LeavelGenerator leavelGenerator;

    public GameObject skinModeObj;
    public Transform centr, skinMather;

    bool lineGenerated;
    public List<Transform> skins;


    public GUISkin skin1;

    public Texture2D arrow_right, arrow_left, circle_grad;

    public Texture2D fon_1;

    // Mechanic
    float enertion;
    public int id = 0;
    bool karusel;
    public float distanceShope = 7;

    float descriptAnim;


    float BlackScreenAnim;

    float LocationsSwipeAnim;


    public Texture2D goldT, dimondT;
    public Color goldC, dimondD;



    // Покупка
    public bool byActive;
    public float byAnim;



    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        leavelGenerator = FindObjectOfType<LeavelGenerator>();

        skinsLineCreate();
    }

    // Срабатывает, когда только открылось это окно (в меню нажали на кнопку)
    public void Entered() {
        skinModeObj.SetActive(true);
    }
    public void End() {
        if (leavelGenerator != null) { leavelGenerator.SetLocation(); }


        locationsMenuTest = false;

        skinModeObj.SetActive(false);
        gameManager.GameStatus = "menu";

        gameManager.SkinID = SkinTest();
        gameManager.car.ActiveSkin(gameManager.SkinID);
    }

    // Создаёт карусель скинов
    public void skinsLineCreate()
    {
        if (lineGenerated) { return; }

        if (gameManager == null) { return; }
        if (gameManager.car == null) { return; }
        for (int i = 0; i < gameManager.car.skinsCar.Count; i++) {
            GameObject carSkinInLine = GameObject.Instantiate(gameManager.car.skinsCar[i]);
            carSkinInLine.transform.position = new Vector3(centr.position.x, centr.position.y, centr.position.z + distanceShope * i);
            carSkinInLine.transform.localScale /= 1.5f;
            carSkinInLine.transform.SetParent(skinMather);
            skins.Add(carSkinInLine.transform);
            carSkinInLine.SetActive(true);
        }

        lineGenerated = true;
    }


    int SkinTest() {
        float minX = int.MaxValue;
        int id = 0;
        for (int i = 0; i < skins.Count; i++) {
            if (MDS.Absolute(centr.position.z - skins[i].position.z) < minX) {
                minX = MDS.Absolute(centr.position.z - skins[i].position.z);
                id = i;
            }
            if (minX <= 1) { break; }
        }
        return id;
    }



    void ByOk() {
        // Покупка локации
        if (locationsMenuTest)
        {

        }
        // Покупка машины
        else {
            gameManager.car.skinsCar[id].GetComponent<SkinCar>().Active = true;
            gameManager.GOLD_COUNT -= gameManager.car.skinsCar[id].GetComponent<SkinCar>().costGold;
            gameManager.DIMOND_COUNT -= gameManager.car.skinsCar[id].GetComponent<SkinCar>().costDimond;

            gameManager.SaveData.save_();
        }
    }

    // Update is called once per frame
    void Update()
    {



        if (gameManager.GameStatus != "skin") { return; }

        if (byActive) { if (byAnim < 1) { byAnim += Time.deltaTime * 7; } else { byAnim = 1; } }
        else{ if (byAnim > 0) { byAnim -= Time.deltaTime * 7; } else { byAnim = 0; } }


        if (locationsMenuTest)
        {
            if (locationsMenuAnim < 1) { locationsMenuAnim += Time.deltaTime * 9; }
            if (locationsMenuAnim > 1) { locationsMenuAnim = 1; }

            // Перелистование сцен
            if (locationsMenuTest && !byActive)
            {
                LocationsSwipeAnim = Mathf.Lerp(LocationsSwipeAnim, 0, Time.deltaTime * 10);
                //if (LocationsSwipeAnim < 0) { LocationsSwipeAnim += Time.deltaTime * 3; }
                //if (LocationsSwipeAnim > 0) { LocationsSwipeAnim -= Time.deltaTime * 3; }

                if (mds.swipeRight && leavelGenerator.LocID < (leavelGenerator.locations.Count - 1))
                {
                    leavelGenerator.LocID++;
                    LocationsSwipeAnim = 1;
                }

                if (mds.swipeLeft && leavelGenerator.LocID > 0)
                {
                    leavelGenerator.LocID--;
                    LocationsSwipeAnim = -1;
                }
            }
        }
        else {
            if (locationsMenuAnim >0) { locationsMenuAnim -= Time.deltaTime * 9; }
            if (locationsMenuAnim < 0) { locationsMenuAnim = 0; }
        }


        if (locationsMenuTest) { return; }


        if (BlackScreenAnim > 0) { BlackScreenAnim -= Time.deltaTime * 5; }
        if (BlackScreenAnim < 0) { BlackScreenAnim = 0; }


        // Карусель
        if (!byActive)
        {
            if (mds.swipeRight)
            {
                if (id < (skins.Count - 1))
                {
                    skins[id].rotation = new Quaternion(0, 0, 0, skins[0].rotation.w);
                    skins[id].localScale /= 1.5f;
                    id++;
                    skins[id].localScale *= 1.5f;
                }
            }

            if (mds.swipeLeft)
            {
                if (id > 0)
                {
                    skins[id].rotation = new Quaternion(0, 0, 0, skins[0].rotation.w);
                    skins[id].localScale /= 1.5f;
                    id--;
                    skins[id].localScale *= 1.5f;
                }
            }
            skins[id].Rotate(Vector3.up * Time.deltaTime * 60);

            skinMather.position = Vector3.Lerp(skinMather.position,
                                                        new Vector3(centr.position.x, centr.position.y, centr.position.z - distanceShope * id),
                                                        Time.deltaTime * 10);

            /*
            if (Input.GetMouseButtonUp(0))
            {
                enertion = mds.mouseDeltaPosition.x / 50;
                karusel = false;
            }

            if (false && Input.GetMouseButton(0) && (karusel || MDS.Absolute(mds.mouseDeltaPosition.x) > 2f))
            {
                karusel = true;
                descriptAnim = 0;


                if (id >= 0)
                {
                    skins[id].rotation = new Quaternion(0, 0, 0, skins[0].rotation.w);
                    skins[id].localScale /= 1.5f;
                }
                id = -1;
                //Debug.Log(mds.mouseDeltaPosition.x + "");
                skinMather.position = new Vector3(skinMather.position.x,
                                                    skinMather.position.y,
                                                    skinMather.position.z + mds.mouseDeltaPosition.x / 100);
            }
            else
            {
                if (MDS.Absolute(enertion) > 0)
                {
                    skinMather.position = new Vector3(skinMather.position.x,
                                                    skinMather.position.y,
                                                    skinMather.position.z + enertion);
                    enertion = Mathf.Lerp(enertion, 0, Time.deltaTime * 10);
                }

                if (id == -1)
                {
                    id = SkinTest();
                    skins[id].localScale *= 1.5f;
                }
                skinMather.position = Vector3.Lerp(skinMather.position,
                                                        new Vector3(centr.position.x, centr.position.y, centr.position.z - distanceShope * id),
                                                        Time.deltaTime * 10);

                skins[id].Rotate(Vector3.up * Time.deltaTime * 60);

                if (descriptAnim < 1) { descriptAnim += Time.deltaTime * 3; }
                if (descriptAnim > 1) { descriptAnim = 1; }
            }
            */
        }
    }

    void FixedUpdate() { 
        
    }


    public void skin(float anim) {

        //--- Обнуление и инициализация значений ---
        Rect pos = new Rect();
        GUI.skin = skin1;
        //------------------------------------------


        float size = Screen.width / 10;
        GUI.color = MDS.ColorSetAlpha(Color.white, anim);
        GUI.skin.label.fontSize = (int)(Screen.width / 20);
        GUI.skin.label.alignment = TextAnchor.MiddleLeft;

        GUI.DrawTexture(new Rect(5, 5, size, size), goldT);
        GUI.Label(new Rect(10 + size, 5, Screen.width, size), gameManager.GOLD_COUNT + "");

        GUI.skin.label.alignment = TextAnchor.MiddleRight;
        GUI.DrawTexture(new Rect(Screen.width - size - 5, 5, size, size), dimondT);
        GUI.Label(new Rect(Screen.width - size - 5 - Screen.width, 5, Screen.width, size), gameManager.DIMOND_COUNT + "");

        GUI.skin.label.alignment = TextAnchor.MiddleCenter;


        if (locationsMenuAnim != 1) { skinsMenu(anim * (1 - locationsMenuAnim)); }
        if (locationsMenuAnim != 0) { locationsMenu(anim * locationsMenuAnim); }


        // Меню покупки
        if (byAnim > 0)
        {
            GUI.color = MDS.ColorSetAlpha(Color.black, byAnim * 0.6f);
            GUI.DrawTexture(MDS.SizeScreen(), interfaceGUI.whiteT);

            GUI.color = MDS.ColorSetAlpha(Color.white, byAnim * 0.7f);
            pos = new Rect(Screen.width / 2 - Screen.width / 1.5f / 2, Screen.height / 2 - Screen.height / 6 / 2, Screen.width / 1.5f, Screen.height / 6);
            GUI.DrawTexture(pos, interfaceGUI.whiteT);
            GUI.color = MDS.ColorSetAlpha(Color.black, byAnim);
            GUI.Label(pos, "Вы уверены, что хотите купить?");
            // Да
            GUI.color = MDS.ColorSetAlpha(Color.green, byAnim);
            pos.y += pos.height;
            pos.width /= 2;
            pos.height /= 2.5f;
            GUI.DrawTexture(pos, interfaceGUI.whiteT);
            GUI.color = MDS.ColorSetAlpha(Color.black, byAnim);
            GUI.Label(pos, "Да");
            if (GUI.Button(pos, "")) { ByOk(); byActive = false; }
            // Нет
            GUI.color = MDS.ColorSetAlpha(Color.red, byAnim);
            pos.x += pos.width;
            GUI.DrawTexture(pos, interfaceGUI.whiteT);
            GUI.color = MDS.ColorSetAlpha(Color.black, byAnim);
            GUI.Label(pos, "Нет");
            if (GUI.Button(pos, "")) { byActive = false; }
        }




        GUI.color = MDS.ColorSetAlpha(Color.black, BlackScreenAnim);
        GUI.DrawTexture(MDS.SizeScreen(), interfaceGUI.whiteT);

    }

    SkinCar CurrentSkin() {
        if (id < 0) { return null; }
        return gameManager.car.skinsCar[id].GetComponent<SkinCar>();
    }

    // Отрисовка меню скинов
    void skinsMenu(float anim) {
        Rect pos = new Rect();

        if ((id != -1) && (CurrentSkin().Active))
        {
            // Меню
            GUI.color = MDS.ColorSetAlpha(Color.white, MDS.Absolute(anim) * 0.4f);
            pos = new Rect(20, Screen.height - Screen.height / 12 - 20, Screen.width - 40, Screen.height / 12);
            GUI.DrawTexture(pos, interfaceGUI.whiteT);
            GUI.skin.button.fontSize = (int)(Screen.height / 24);
            if (GUI.Button(pos, "Menu")) { End(); }


            // Локации
            GUI.color = MDS.ColorSetAlpha(Color.white, MDS.Absolute(anim) * 0.4f);
            pos.y -= pos.height + 20;
            GUI.DrawTexture(pos, interfaceGUI.whiteT);
            GUI.skin.button.fontSize = (int)(Screen.height / 24);
            if (GUI.Button(pos, "Locations")) { locationsMenuTest = true; }
        }
        else {
            // Купить
            pos = new Rect(20, Screen.height - Screen.height / 12 - 20, Screen.width - 40, Screen.height / 12);
            GUI.skin.button.fontSize = (int)(Screen.height / 24);

            //Если за золото
            if (CurrentSkin() != null) {
                if (CurrentSkin().costGold > 0)
                {
                    GUI.color = MDS.ColorSetAlpha(goldC, MDS.Absolute(anim) * 0.4f);
                    GUI.DrawTexture(pos, interfaceGUI.whiteT);
                    if (GUI.Button(pos, "By of " + CurrentSkin().costGold))
                    {
                        byActive = true;
                    }
                    GUI.color = MDS.ColorSetAlpha(Color.white, anim * 0.6f);
                    GUI.skin.label.alignment = TextAnchor.MiddleRight;
                    pos.x -= Screen.width / 20;
                    GUI.Label(pos, goldT);
                }
                else if (CurrentSkin().costDimond > 0)
                {
                    GUI.color = MDS.ColorSetAlpha(dimondD, MDS.Absolute(anim) * 0.4f);
                    GUI.DrawTexture(pos, interfaceGUI.whiteT);
                    if (GUI.Button(pos, "By of " + CurrentSkin().costDimond))
                    {
                        byActive = true;
                    }
                    GUI.color = MDS.ColorSetAlpha(Color.white, anim * 0.6f);
                    GUI.skin.label.alignment = TextAnchor.MiddleRight;
                    pos.x -= Screen.width / 20;
                    GUI.Label(pos, dimondT);
                }
            }
            

        }


        // Описание
        if (id != -1)
        {
            GUI.color = MDS.ColorSetAlpha(Color.white, MDS.Absolute(anim) * descriptAnim * 0.8f);
            pos = new Rect(50, 100, Screen.width - 100, Screen.height / 3);
            GUI.skin.label.fontSize = (int)(Screen.height / 35);
            GUI.skin.label.alignment = TextAnchor.UpperCenter;
            GUI.Label(pos, gameManager.car.skinsCar[id].GetComponent<SkinCar>().descript);
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        }
    }


    // Отрисовка меню локаций
    void locationsMenu(float anim)
    {
        if (leavelGenerator == null) { return; }

        GUI.color = Color.black;
        GUI.DrawTexture(MDS.SizeScreen(), interfaceGUI.whiteT);
        //GUI.color = MDS.ColorSetAlpha(leavelGenerator.locations[leavelGenerator.LocID].col , anim * MDS.FloatAnim(0.8f, 1, (1 - MDS.Absolute(LocationsSwipeAnim))));
        GUI.color = MDS.ColorSetAlpha(Color.white, anim * MDS.FloatAnim(0.3f, 1, (1 - MDS.Absolute(LocationsSwipeAnim))));


        GUI.DrawTexture(MDS.RectMode.cropMax(MDS.SizeScreen(), leavelGenerator.locations[leavelGenerator.LocID].Fon)
                        , leavelGenerator.locations[leavelGenerator.LocID].Fon);


        Rect pos = new Rect();


        // Описание локации

        //GUI.color = MDS.ColorSetAlpha(Color.white, anim);
        pos = new Rect(50 + LocationsSwipeAnim * Screen.width/5, 50, Screen.width - 100, Screen.height / 3);
        GUI.skin.label.fontSize = (int)(Screen.height / 27);
        GUI.skin.label.alignment = TextAnchor.UpperCenter;
        GUI.Label(pos, leavelGenerator.locations[leavelGenerator.LocID].loc_name);

        pos.y += GUI.skin.label.fontSize * 2;
        GUI.skin.label.fontSize = (int)(Screen.height / 30);
        GUI.Label(pos, leavelGenerator.locations[leavelGenerator.LocID].loc_descript);


        GUI.skin.label.alignment = TextAnchor.MiddleCenter;



        // Меню
        GUI.color = MDS.ColorSetAlpha(Color.white, MDS.Absolute(anim) * 0.4f);
        pos = new Rect(20, Screen.height - Screen.height / 12 - 20, Screen.width - 40, Screen.height / 12);
        GUI.DrawTexture(pos, interfaceGUI.whiteT);
        GUI.skin.button.fontSize = (int)(Screen.height / 24);
        if (GUI.Button(pos, "Start")) { End(); }

        // Скины
        GUI.color = MDS.ColorSetAlpha(Color.white, MDS.Absolute(anim) * 0.4f);
        pos.y -= pos.height + 20;
        GUI.DrawTexture(pos, interfaceGUI.whiteT);
        GUI.skin.button.fontSize = (int)(Screen.height / 24);
        if (GUI.Button(pos, "Skins")) { locationsMenuTest = false; }


        GUI.color = MDS.ColorSetAlpha(Color.white, anim * 0.4f);


        pos = new Rect(10, Screen.height / 2 - Screen.height / 8 / 2, Screen.height / 8, Screen.height / 8);

    }
}