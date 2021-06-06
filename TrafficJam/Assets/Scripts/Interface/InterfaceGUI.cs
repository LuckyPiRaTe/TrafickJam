using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceGUI : MonoBehaviour
{
    public GameManager gameManager;

    //--- Menu ---
    public InterfaceMenu IMenu;
    float animMenu;
    //------------

    //--- Game ---
    public InterfaceGame IGame;
    float animGame;
    //------------

    //--- Loos ---
    public InterfaceLoos ILoos;
    [HideInInspector]
    public float animLoos;
    //------------

    //--- Skin ---
    public InterfaceSkin ISkin;
    float animSkin;
    //------------

    public Texture2D whiteT;
    public Font font1;

    [Range(0,1)]
    public float anim;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // MENU
        if (gameManager.GameStatus == "menu")
        {
            if (animMenu < 1) { animMenu += Time.deltaTime * 5; }
            if (animMenu > 1) { animMenu = 1; }
        }
        else
        {
            if (animMenu > 0) { animMenu -= Time.deltaTime * 5; }
            if (animMenu < 0) { animMenu = 0; }
        }

        // GAME
        if (gameManager.GameStatus == "play")
        {
            if (animGame < 1) { animGame += Time.deltaTime * 5; }
            if (animGame > 1) { animGame = 1; }
        }
        else
        {
            if (animGame > 0) { animGame -= Time.deltaTime * 5; }
            if (animGame < 0) { animGame = 0; }
        }

        //LOOS
        if (gameManager.GameStatus == "loos")
        {
            if (animLoos < 1) { animLoos += Time.deltaTime * 5; }
            if (animLoos > 1) { animLoos = 1; }
        }
        else
        {
            if (animLoos > 0) { animLoos -= Time.deltaTime * 5; }
            if (animLoos < 0) { animLoos = 0; }
        }

        //SKIN
        if (gameManager.GameStatus == "skin")
        {
            if (animSkin < 1) { animSkin += Time.deltaTime * 5; }
            if (animSkin > 1) { animSkin = 1; }
        }
        else
        {
            if (animSkin > 0) { animSkin -= Time.deltaTime * 5; }
            if (animSkin < 0) { animSkin = 0; }
        }
    }

    private void OnGUI()
    {
        MDS.ButtonTextureFree();

        GUI.skin.label.font = font1;
        GUI.skin.button.font = font1;

        // MENU
        if (animMenu > 0)
        {
            IMenu.menu(animMenu);
        }

        // GAME
        if (animGame > 0)
        {
            IGame.interfaceItems(animGame);
        }

        // Loos
        if (animLoos > 0)
        {
            ILoos.loos(animLoos);
        }

        // SKIN
        if (animSkin > 0)
        {
            ISkin.skin(animSkin);
        }
    }


    public void SetStatusPlay() {
        gameManager.SetStatusPlay();
    }

    // Окно выбора скина
    public void SkinSelect() {
        gameManager.SkinSelect();
        ISkin.Entered();
    }
}