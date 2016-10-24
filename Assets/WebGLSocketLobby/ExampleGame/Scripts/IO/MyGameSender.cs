using UnityEngine;
using System.Collections;
using WebGLSocketLobby.IO;
using WebGLSocketLobby.Game;

namespace WebGLSocketLobby.ExampleGame {
    public class MyGameSender : GameSender {

        public static void SendPlayerClick(string parameter) {
            Send("SendPlayerClick", parameter);
        }

    }
}