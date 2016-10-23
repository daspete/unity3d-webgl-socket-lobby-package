using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using WebGLSocketLobby;
using WebGLSocketLobby.Panels;
using WebGLSocketLobby.UI;
using WebGLSocketLobby.IO;
using WebGLSocketLobby.Data;

namespace WebGLSocketLobby {
    public class SocketLobby : MonoBehaviour {

        static SocketLobby instance = null;
        public static SocketLobby Instance { get { return instance; } }

        SocketLobbyUI ui;
        public static SocketLobbyUI UI { get { return instance.ui; } }

        public SocketLobbySettings settings;

        public string connectionID;

        RoomData room;
        PlayerData player;

        public static RoomData Room { get { return instance.room; } }
        public static PlayerData Player { get { return instance.player; } }

        void Awake() {
            if(instance != null) GameObject.Destroy(instance.gameObject);

            DontDestroyOnLoad(this);

            instance = this;

            settings.gameServerUrl = settings.defaultGameServerUrl;
        }

        void Start() {
            ui = GetComponent<SocketLobbyUI>();

            ConfigData config = new ConfigData();
            config.maxPlayersPerRoom = settings.maxPlayersPerRoom;
            config.minPlayerToStartGame = settings.minPlayersToStartGame;
            config.playerColors = settings.playerColors;
            config.defaultPlayerColor = settings.defaultPlayerColorIndex;

            SocketSender.Send("SetConfig", JsonUtility.ToJson(config));
            
            ui.CreateTopPanel();
            ui.CreateConnectPanel();

            SocketReceiver.OnConnected += OnConnected;
            SocketReceiver.OnDisconnected += OnDisconnected;
            SocketReceiver.OnConnectionTimeout += OnConnectionTimeout;
            SocketReceiver.OnStartedGame += OnStartedGame;
        }

        void OnConnected(string connectionID) {
            this.connectionID = connectionID;
            ui.CreateLobbyPanel();
        }

        void OnDisconnected() {
            connectionID = "";
            room = null;
            
            SceneManager.LoadScene(settings.lobbySceneBuildIndex);
        }

        void OnConnectionTimeout() {
            connectionID = "";
            room = null;
            
            if(ui.connectPanel != null) {
                ui.connectPanel.EnableConnectButton();
            }
        }

        void OnStartedGame() {
            ui.DeleteTopPanel();
            ui.DeleteRoomPanel();
            SceneManager.LoadScene(settings.gameSceneBuildIndex);
        }
        
        public void SetGameServerUrl(string value) {
            settings.gameServerUrl = value.Length == 0 ? settings.defaultGameServerUrl : value;
        }

        public void SetRoom(RoomData room) {
            this.room = room;

            ui.CreateRoomPanel();
        }

        public void SetPlayer(PlayerData player) {
            this.player = player;
        }

        public void BackToLobby() {
            room = null;
            ui.CreateLobbyPanel();
        }

    }
}
