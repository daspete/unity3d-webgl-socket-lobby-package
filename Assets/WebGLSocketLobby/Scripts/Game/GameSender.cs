using UnityEngine;
using System.Collections;

namespace WebGLSocketLobby.Game {
    public class GameSender : MonoBehaviour {

        static string externalReceiverObject = "gameClient.";

        public static void SendToAllPlayers(string parameter) {
            Application.ExternalCall(externalReceiverObject + "SendToAllPlayers", parameter);
        }

        public static void SendToOnePlayer(string parameter) {
            Application.ExternalCall(externalReceiverObject + "SendToOnePlayer", parameter);
        }

        public static void SendToServer(string parameter) {
            Application.ExternalCall(externalReceiverObject + "SendToServer", parameter);
        }

        public static void Send(string functionName, string parameter) {
            Application.ExternalCall(externalReceiverObject + functionName, parameter);
        }

        public static void Send(string functionName) {
            Application.ExternalCall(externalReceiverObject + functionName);
        }

    }
}