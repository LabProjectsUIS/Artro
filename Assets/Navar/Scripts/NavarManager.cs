using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Threading;
using System;
using System.Text;

public class NavarManager : MonoBehaviour {    

    [SerializeField]
    private GameObject[] _toolsModels;
    [SerializeField]
    private GameObject _trackingObject;

    private List<Vector3> _posStreaming = new List<Vector3>();
    private List<Quaternion> _rotStreaming = new List<Quaternion>();
    private IEnumerator<Vector3> _posEnumarator;
    private IEnumerator<Quaternion> _rotEnumarator;
    private bool _playStream = false;

    private TcpClient socketConnection;
    private Thread clientReceiveThread;

    // Use this for initialization
    void Start () {
        
        Assert.IsNotNull(_toolsModels, "no se encontro el modelo que se quiere mostrar");
        HideModel();
        StreamReader readFile = new StreamReader(Application.dataPath + "/Resources/datos.csv");
        string line;
        string[] row;
        while((line = readFile.ReadLine()) != null)
        {
            row = line.Split(';');
            _posStreaming.Add(new Vector3(float.Parse(row[4]), float.Parse(row[5]), float.Parse(row[6])));
            _rotStreaming.Add(new Quaternion(float.Parse(row[0]), float.Parse(row[1]), float.Parse(row[2]), float.Parse(row[3])));
        }
        _posEnumarator = _posStreaming.GetEnumerator();
        _rotEnumarator = _rotStreaming.GetEnumerator();
        ConnectToTcpServer();
    }

    private void ConnectToTcpServer()
    {
        try
        {
            clientReceiveThread = new Thread(new ThreadStart(ListenForData));
            clientReceiveThread.IsBackground = true;
            clientReceiveThread.Start();
            Debug.Log("Thread Started");
        }
        catch (Exception e)
        {
            Debug.Log("On client connect exception " + e);
        }
    }

    private void ListenForData()
    {
        Debug.Log("ListenForData");
        try
        {
            Debug.Log("Try");
            ///ocketConnection = new TcpClient("192.168.1.6", 4444);
            socketConnection = new TcpClient("localHost", 4444);
            Byte[] bytes = new Byte[1024];
            while (true)
            {
                // Get a stream object for reading             
                using (NetworkStream stream = socketConnection.GetStream())
                {
                    int length;
                    // Read incomming stream into byte arrary.                 
                    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        var incommingData = new byte[length];
                        Array.Copy(bytes, 0, incommingData, 0, length);
                        // Convert byte array to string message.                       
                        string serverMessage = Encoding.ASCII.GetString(incommingData);
                        Debug.Log("server message received as: " + serverMessage);
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }

    public void CalibrateCameras()
    {
        if (socketConnection == null)
        {
            return;
        }
        try
        {
            // Get a stream object for writing.            
            NetworkStream stream = socketConnection.GetStream();
            if (stream.CanWrite)
            {
                string clientMessage = "startCalibration";
                // Convert string message to byte array.                
                byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);
                // Write byte array to socketConnection stream.                
                stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                Debug.Log("Client sent his message - should be received by server");
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }

    public void SetParams(GameObject paramsContainer)
    {
        if (socketConnection == null)
        {
            return;
        }
        try
        {
            Slider[] slidersParams = paramsContainer.GetComponentsInChildren<Slider>();
            float[] camParams = new float[4];
            foreach(Slider slide in slidersParams)
            {
                switch(slide.name)
                {
                    case "Param1":
                        camParams[0] = slide.value;
                        break;

                    case "Param2":
                        camParams[1] = slide.value;
                        break;

                    case "Param3":
                        camParams[2] = slide.value;
                        break;

                    case "Param4":
                        camParams[3] = slide.value;
                        break;

                }
            }
            // Get a stream object for writing.            
            NetworkStream stream = socketConnection.GetStream();
            if (stream.CanWrite)
            {
                string clientMessage = "setParams : " + camParams[0] + ", " + camParams[1] + ", " + camParams[2] + ", " + camParams[3];
                // Convert string message to byte array.                
                byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);
                // Write byte array to socketConnection stream.                
                stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                Debug.Log("Client sent his message - should be received by server - messagge: " + clientMessage);
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }

    private void HideModel()
    {
        foreach (GameObject go in _toolsModels)
        {
            go.SetActive(false);
        }        
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            //ShowModel();
            _playStream = true;
            _posEnumarator.Reset();
        }
        if (_playStream && _posEnumarator.MoveNext() && _rotEnumarator.MoveNext())
        {
            _trackingObject.transform.position = _posEnumarator.Current;
            _trackingObject.transform.rotation = _rotEnumarator.Current;
        }
        else
        {
            _playStream = false;
        }
    }

    public void ShowModel()
    {
        HideModel();
        var go = EventSystem.current.currentSelectedGameObject;
        if (go != null)
        {
            Debug.Log("Clicked on : " + go.name);
            foreach (GameObject tool in _toolsModels)
            {
                if(go.GetComponentInChildren<Text>().text == tool.name)
                {
                    tool.SetActive(true);
                }
            }
        }
        else
            Debug.Log("currentSelectedGameObject is null");
    }
}
