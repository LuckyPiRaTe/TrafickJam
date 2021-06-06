using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    public MDS mds;
    public GameManager gameManager;
    public Missions missions;

    public List<GameObject> skinsCar;

    public bool run, controll;

    public float spead, speadDinamic, speadMax, uskorenie, speadRot = 10;

    Vector3 CameraPos;
    public Transform CameraPosPoint, CameraLookPoint;
    public Transform rot_left_point, rot_right_point;
    public Quaternion rot_standart, rot_left, rot_right;

    LeavelGenerator leavelGenerator;
    Camera_ camera_;

    public float durControll;


    //Для работы скрипта
    [HideInInspector]
    public float rotateDur;
    public Animator animator;


    public bool LinesOrNo; // Если true, то примагничивается к линиям

    public int lineNow;


    void Start()
    {
        leavelGenerator = GameObject.FindObjectOfType<LeavelGenerator>();
        CameraPos = CameraPosPoint.position - transform.position;

        camera_ = GameObject.FindObjectOfType<Camera_>();

        rot_standart = transform.rotation;
        rot_left = rot_left_point.rotation;
        rot_right = rot_right_point.rotation;


        //gameManager.CarInicialise(GetComponent<Car>());

        
    }

    void CameraActive() {
        camera_.cameraLook = CameraLookPoint;
    }

    public void ActiveSkin(int skinId)
    {
        Debug.Log("Active");
        for (int i = 0; i < skinsCar.Count; i++)
        {
            skinsCar[i].SetActive(i == skinId);
        }
    }

    public void startGame() {
        run = true;
        camera_.toPoints = true;
        camera_.cameraLook = transform;
        animator.SetBool("startgame", true);

        Invoke("StartRun", 0.5f);
        Invoke("CameraActive", 0.5f);
    }


    float getMaxSpeed() {
        if (gameManager.getCurrentSkin().skills.testIn("speed")) { return gameManager.getCurrentSkin().skills.getSkill("speed").floatData; }
        return speadMax;
    }

    float getMaxSpeadRot()
    {
        if (gameManager.getCurrentSkin().skills.testIn("speedRot")) { return gameManager.getCurrentSkin().skills.getSkill("speedRot").floatData; }
        return speadRot;
    }

    private void FixedUpdate()
    {
        if (mds == null) { mds = FindObjectOfType<MDS>(); }
        if (gameManager == null) { gameManager = FindObjectOfType<GameManager>(); }
        if (missions == null) { missions = FindObjectOfType<Missions>(); }
        if (leavelGenerator == null) { leavelGenerator = FindObjectOfType<LeavelGenerator>(); }


        if (run) {
            transform.Translate(Vector3.forward * (spead + speadDinamic) * Time.deltaTime);
        }
        CameraPosPoint.position = transform.position + CameraPos;

        if (LinesOrNo)
        {
            if (run)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(gameManager.lines[lineNow].position.x, transform.position.y, transform.position.z), Time.deltaTime * 5);
            }
        }
        else {
            // Управление
            if (controll)
            {
                camera_.toPoints = true;
                //rotateDur = ControllCar();
                rotateDur = durControll;
            }

            if (run)
            {
                if (rotateDur < 0)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, MDS.QuaternionAnim(rot_standart, rot_left, MDS.Absolute(rotateDur)), Time.deltaTime * getMaxSpeadRot());
                }
                else if (rotateDur > 0)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, MDS.QuaternionAnim(rot_standart, rot_right, MDS.Absolute(rotateDur)), Time.deltaTime * getMaxSpeadRot());
                }
                else
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, rot_standart, Time.deltaTime * getMaxSpeadRot());
                }
            }
        }

        
    }

    void Update()
    {
        //LINES

        if (LinesOrNo) {
            if (mds.swipeLeft) {
                if (lineNow < 4) { lineNow++; }
                Debug.Log(lineNow + "");
            }
            if (mds.swipeRight) {
                if (lineNow > 0) { lineNow--; }
                Debug.Log(lineNow + "");
            }


            //(gameManager.lines[lineNow].position.x - transform.position.x)
            if ((gameManager.lines[lineNow].position.x - transform.position.x) > 0.5f)
            {
                durControll = 1;
            }

            if ((gameManager.lines[lineNow].position.x - transform.position.x) < -0.5f)
            {
                durControll = -1;
            }
        }

        // NE LINES
        if (!LinesOrNo)
        {
            if (Input.GetMouseButton(0))
            {
                if ((durControll >= -1) && (durControll <= 1))
                {
                    durControll += mds.mouseDeltaPosition.x * 0.007f;
                }
                if (durControll < -1)
                {
                    durControll = -1;
                }
                if (durControll > 1)
                {
                    durControll = 1;
                }
            }
            else
            {
                durControll = Mathf.Lerp(durControll, 0, Time.deltaTime * 5);
                if (MDS.Absolute(durControll) <= 0.005f) { durControll = 0; }
            }
        }

        //if (Input.GetKey(KeyCode.Space)) { startGame(); }

        
        if (run) {
            if (speadDinamic < getMaxSpeed())
            {
                speadDinamic += Time.deltaTime * uskorenie;
            }
            else {
                speadDinamic = getMaxSpeed();
            }
        }
        
        
        //transform.Translate(Vector3.forward * spead * Time.deltaTime);
    }

    // Управление
    float ControllCar() {
        // Управление клавиатурой
        if (Input.GetKey(KeyCode.A))
        {
            return -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            return 1;
        }

        return 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Barier" || other.tag == "Barier_left" || other.tag == "Barier_right")
        {
            //Debug.Log("Barier");
            CameraShake.Shake(0.2f, 1.5f);
            //camera_.transform.position -= new Vector3(camera_.transform.position.x, camera_.transform.position.y - 0.2f, camera_.transform.position.z);

            loos(other.tag);
        }
        else if (other.tag == "LeavelGenerator") {
            leavelGenerator.Generate();
        }

        // Монетка
        if (other.tag == "Coin") {
            gameManager.coinsLeavel += 1;
            missions.coinAdd();
            Destroy(other.transform.gameObject);
        }
    }

    public void StartRun() {
        controll = true;
    }

    public void loos(string barier_type) {
        gameManager.GameStatus = "loos";
        run = false;
        controll = false;

        gameManager.polition.barier_type = barier_type;
        gameManager.polition.cars_set_pos();
    }
}