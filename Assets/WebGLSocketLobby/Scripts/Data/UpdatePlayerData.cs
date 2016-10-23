using UnityEngine;
using System.Collections;

namespace WebGLSocketLobby.Data {
    [System.Serializable]
    public class UpdatePlayerData {

        public string roomID;
        public PlayerData player;

    }
}
