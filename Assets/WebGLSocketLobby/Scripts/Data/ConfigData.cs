using UnityEngine;
using System.Collections;

namespace WebGLSocketLobby.Data {
    [System.Serializable]
    public class ConfigData {

        public int maxPlayersPerRoom;
        public int minPlayerToStartGame;
        public Color[] playerColors;
        public int defaultPlayerColor;

    }
}

