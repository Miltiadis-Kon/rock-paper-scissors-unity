using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class udp : MonoBehaviour
{

    public GameMngr manager;
    private UdpClient udpClient;
    private int port = 8000;


    void Awake()
    {
        if(manager==null)
        manager = GameObject.Find("GameMananger").GetComponent<GameMngr>();
    }
    void Start()
    {
        udpClient = new UdpClient(port);
        udpClient.BeginReceive(ReceiveCallback, udpClient);
        Debug.Log($"Listening for UDP messages on port {port}");
    }

    private void ReceiveCallback(IAsyncResult ar)
    {
        UdpClient client = (UdpClient)ar.AsyncState;
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, port);
        byte[] bytes = client.EndReceive(ar, ref groupEP);
        string message = Encoding.UTF8.GetString(bytes);
    //    Debug.Log($"Received message: {message}");

        // Process the received message here
        HandleUdpMessage(message);

        client.BeginReceive(ReceiveCallback, client);
    }

    private void HandleUdpMessage(string message)
    {
        // Do something with the received message
    //    Debug.Log($"Received message: {message}");
        manager.InputHandler(message);
    }

    // Close socket on exit 
    private void OnApplicationQuit()
    {
        udpClient.Close();
    }

    // Close socket on destroy
    private void OnDestroy()
    {
        udpClient.Close();
    }
}