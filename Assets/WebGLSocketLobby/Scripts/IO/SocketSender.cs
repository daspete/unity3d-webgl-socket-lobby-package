using UnityEngine;
using System.Collections;

namespace WebGLSocketLobby.IO {
    public class SocketSender : MonoBehaviour {

        static string externalReceiverObject = "webglSocketLobby.";

        public static void Send(string functionName, string parameter) {
            Application.ExternalCall(externalReceiverObject + functionName, parameter);
        }

        public static void Send(string functionName) {
            Application.ExternalCall(externalReceiverObject + functionName);
        }

    }
}
