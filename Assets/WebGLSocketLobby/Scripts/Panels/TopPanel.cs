using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using WebGLSocketLobby;
using WebGLSocketLobby.IO;


namespace WebGLSocketLobby.Panels {
    public class TopPanel : MonoBehaviour {
        
        public Text status;
        public Text server;

        void Awake() {
            OnDisconnected();
        }

        void OnEnable() {
            SocketReceiver.OnConnected += OnConnected;
            SocketReceiver.OnDisconnected += OnDisconnected;
        }

        void OnDisable() {
            SocketReceiver.OnConnected -= OnConnected;
            SocketReceiver.OnDisconnected -= OnDisconnected;
        }

        void OnConnected(string connectionID) {
            SetStatus("connected");
            SetServer(SocketLobby.Instance.settings.gameServerUrl);
        }

        void OnDisconnected() {
            SetStatus("offline");
            SetServer("none");
        }


        public void SetStatus(string status) {
            this.status.text = status.ToUpper();
        }

        public void SetServer(string server) {
            this.server.text = server.ToUpper();
        }

    }
}

