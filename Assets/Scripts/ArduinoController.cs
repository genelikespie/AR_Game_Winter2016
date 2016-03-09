using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.IO.Ports;

public class ArduinoController : MonoBehaviour {

    SerialPort sp = new SerialPort("COM3", 9600);
    moveTank playerTank;
    moveCrosshair crosshair;
    GameManagerScript gameManager;
    bool canPress = true;
    float currTime;
    float debounceTime = 0.1f;
    int debounceCount;
    void Awake()
    {
        crosshair = moveCrosshair.Instance();
        gameManager = GameManagerScript.Instance();
        debounceCount = 0;
        Assert.IsTrue(crosshair && gameManager);
    }

	// Use this for initialization
	void Start () {
        sp.Open();
        sp.ReadTimeout = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if (!canPress)
        {
            //Debug.LogWarning("currtime:" + currTime);
            currTime += gameManager.timeDeltaTime;
            if (currTime > debounceTime)
            {
                canPress = true;
                Debug.LogWarning("canPress:" + canPress);
                currTime = 0;
            }
        }
        else if (sp.IsOpen)
        {
            //Debug.LogWarning("sp open");
            try
            {
                ControllerInput(sp.ReadByte());
            }
            catch (System.Exception)
            {

            }
        }
	}

    public void ControllerInput(int input)
    {
        if (input == 1)
        {
            debounceCount++;
            if (debounceCount == 2)
            {
                Debug.LogWarning("red button " + debounceCount);
                // check if a player tank is available
                if (!playerTank)
                {
                    playerTank = moveTank.Instance();
                }
                // fire the bullet if it is
                if (playerTank && playerTank.activeTurret)
                {
                    playerTank.FireTurret();
                }
                // press virtual buttons as well
                crosshair.VirtualButtonPress();
                debounceCount = 0;
                canPress = false;
                currTime = 0;
                Debug.LogWarning("canPress:" + canPress);
            }
        }
        if (input == 2)
        {
            Debug.Log("green button");
            //transform.position += Vector3.right * 100;
        }

             
    }
}
