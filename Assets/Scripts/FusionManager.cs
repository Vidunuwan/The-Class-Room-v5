using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Linq;



public class FusionManager : MonoBehaviour,INetworkRunnerCallbacks
{
    public static FusionManager instance;

    public bool connecOnAwake=false;
    [HideInInspector] public NetworkRunner runner;

    [SerializeField] NetworkObject playerPrefab;

    public string _playername;

    [Header("Session List")]
    public GameObject roomListCanves;
    private List<SessionInfo> _sessions = new List<SessionInfo>();
    public Button refreshButton;

    public Transform sessionListContent;
    public GameObject sessionEntryPrefab;

    [SerializeField] GameObject camera1;
    [SerializeField] GameObject camera2;

    [SerializeField] public GameObject[] spawnLocations;

    private void Awake()
    {
        if (!instance) { instance = this; }
        if (connecOnAwake)
        {
            ConnectToLobby("Player");
        }
    }
    public void ConnectToLobby(string playerName)
    {
        roomListCanves.SetActive(true);
        _playername = playerName;

        if (!runner)
        {
            runner = gameObject.AddComponent<NetworkRunner>();

        }
        runner.JoinSessionLobby(SessionLobby.Shared);
    }

    public async void ConnetToSession(string sessionName)
    {
        roomListCanves.SetActive(false);

        if (!runner)
        {
            runner = gameObject.AddComponent<NetworkRunner>();
        }

        await runner.StartGame(new StartGameArgs()
        {
            GameMode=GameMode.Shared,
            SessionName= sessionName,
            PlayerCount=2,
            //SceneManager=gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }

    public async void CreateSession()
    {
        roomListCanves.SetActive(false);

        int randomInt = UnityEngine.Random.Range(1000, 9999);
        string randomSessionName = "Session" + randomInt.ToString();

        if (!runner)
        {
            runner = gameObject.AddComponent<NetworkRunner>();
        }

        await runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = randomSessionName,
            PlayerCount = 4,
            //SceneManager=gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }



    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        _sessions.Clear();
        _sessions = sessionList;
    }

    public void RefreshSessionListUI()
    {
        foreach(Transform child in sessionListContent)
        {
            Destroy(child.gameObject);
        }

        foreach(SessionInfo session in _sessions)
        {
            if (session.IsVisible)
            {
                GameObject entry = GameObject.Instantiate(sessionEntryPrefab, sessionListContent);
                SessionEntryPrefab script = entry.GetComponent<SessionEntryPrefab>();

                script.sessionName.text = session.Name;
                script.playerCount.text = session.PlayerCount + "/" + session.MaxPlayers;

                if(session.IsOpen==false || session.PlayerCount >= session.MaxPlayers)
                {
                    script.joinButton.interactable = false;
                }
                else
                {
                    script.joinButton.interactable = true;
                }
            }
        }
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        int random = UnityEngine.Random.Range(0, spawnLocations.Length-1);
        GameObject spawn = spawnLocations[random];
        Vector3 getLocation = spawn.transform.position;

        NetworkObject playerObject = runner.Spawn(playerPrefab, getLocation);
        runner.SetPlayerObject(runner.LocalPlayer, playerObject);
        Debug.Log("On Connected To Server");

        //spawnLocations = spawnLocations.Where(val => val != spawn).ToArray();
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        Debug.Log(reason);
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        Debug.Log(request);

    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("OnPlayerJoined");
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
        
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
        
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        
    }
}

