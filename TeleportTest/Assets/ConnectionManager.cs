using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;
using Unity.Netcode.Transports.UTP;

public class ConnectionManager : MonoBehaviour
{
    //public TMP_InputField addressInput;
   // public TMP_InputField portInput;
    public void ConnectAsClient()
    {
        setConnectionInfo();
        NetworkManager.Singleton.StartClient();
    }

    public void ConnectAsHost()
    {
        setConnectionInfo();
        NetworkManager.Singleton.StartHost();
    }

    public void Disconnect()
    {
        NetworkManager.Singleton.Shutdown();
    }

    private void setConnectionInfo()
    {
        //string address = addressInput.text;
        //ushort port = ushort.Parse(portInput.text);
        //print(address + ":" + port);
        //NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(address, port, "0.0.0.0");
    }
}
