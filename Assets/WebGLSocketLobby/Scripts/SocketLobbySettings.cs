using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace WebGLSocketLobby {
    [System.Serializable]
    public class SocketLobbySettings {

        public string gameServerUrl;
        public string defaultGameServerUrl;

        public int maxPlayersPerRoom;
        public int minPlayersToStartGame;
        
        public Color[] playerColors;
        public int defaultPlayerColorIndex;

        public int gameSceneBuildIndex;
        public int lobbySceneBuildIndex;
    }
}