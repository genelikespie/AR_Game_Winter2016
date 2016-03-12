using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;

public class ArduinoBluetooth : MonoBehaviour {

    public Text statusText;
    public Text activateText;
    public Text powerupText;
    public AUStasis objectReceiver;
    private Text bluetoothText;
    private bool waitResponse;

	void Start () {
        bluetoothText = GetComponent<Text>();
        bluetoothText.text = "";

        // Initialize module and connect to paired Bluetooth device
        BtConnector.moduleName("HC-06");
        if (!BtConnector.isBluetoothEnabled())
        {
            BtConnector.askEnableBluetooth();
        }
        else
        {
            BtConnector.connect();
        }

        Assert.IsTrue(objectReceiver);
	}

	void Update () {
        // Display current status of Bluetooth module
        statusText.text = BtConnector.readControlData();

        // If already connected
        if (BtConnector.isConnected())
        {
            objectReceiver.ActivateSensor(true);
            // Send PING
            if (!waitResponse)
            {
                BtConnector.sendString("PING");
                waitResponse = true;
            }

            // Check for PONG (non-blocking)
            if (BtConnector.available())
            {
                string response = BtConnector.readLine();
                if (response.Length > 0)
                {
                    waitResponse = false;
                    if (response[0] == ' ')
                    {
                        // string to tell us whether or not to activate the stasis
                        string activateStasis = response.Substring(1, 1);
                        activateText.text = activateStasis;
                        powerupText.text = response.Substring(3);
                        if (activateStasis[0] == '1')
                            objectReceiver.FireCharge();
                        objectReceiver.PowerUp(float.Parse(powerupText.text));
                    }
                    else if (response == "PONG BUTTON ON")
                    {
                        bluetoothText.text = "Button On!";
                    }
                    else if (response == "PONG BUTTON OFF")
                    {
                        bluetoothText.text = "...";
                    }
                    else
                    {
                        bluetoothText.text = response;
                    }
                }
            }
        }
	}
}
