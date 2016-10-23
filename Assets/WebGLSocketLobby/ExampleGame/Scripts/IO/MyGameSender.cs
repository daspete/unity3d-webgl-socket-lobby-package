using UnityEngine;
using System.Collections;
using WebGLSocketLobby.IO;
using WebGLSocketLobby.Game;

public class MyGameSender : GameSender {

    public static void SendPlayerClick(string parameter) {
        Send("SendPlayerClick", parameter);
    }

}
