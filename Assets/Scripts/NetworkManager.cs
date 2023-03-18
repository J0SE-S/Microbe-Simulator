using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Riptide;
using Riptide.Utils;

public class NetworkManager : MonoBehaviour {
    public enum ServerToClientId : ushort {
        Movement = 0
    }

    public enum ClientToServerId : ushort {
        AdjustPosition = 100
    }

    private static NetworkManager _singleton;
    public static NetworkManager Singleton {
        get => _singleton;
        private set {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value) {
                Debug.Log($"{nameof(NetworkManager)} instance already exists, destroying object!");
                Destroy(value);
            }
        }
    }

    internal Server Server { get; private set; }
    internal Client Client { get; private set; }

    [SerializeField] private string ip;
    [SerializeField] private ushort port;
    [SerializeField] private ushort maxPlayers;

    void Start() {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
        Server = new Server();
        Client = new Client();
    }

    private void FixedUpdate() {
        if (Server.IsRunning)
            Server.Update();    
        Client.Update();
    }

    private void StartHost() {
        Server.Start(port, maxPlayers);
    }

    private void JoinGame() {
        Client.Connect($"{ip}:{port}");
    }

    private void OnApplicationQuit() {
        Server.Stop();
        Client.Disconnect();
    }
}
