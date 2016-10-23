using UnityEngine;
using System.Collections;

namespace WebGLSocketLobby.Data {
    [System.Serializable]
    public class PlayerData {

        public string playerID;
        public string playerName;
        public Color playerColor;
        public bool playerReady;

    }
}
