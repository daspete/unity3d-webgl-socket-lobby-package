using UnityEngine;
using System.Collections;
using WebGLSocketLobby.Data;

namespace WebGLSocketLobby.IO {
    public class SocketReceiver : MonoBehaviour {

        public delegate void ConnectAction(string connectionID);
        public static event ConnectAction OnConnected;

        public delegate void DisconnectAction();
        public static event DisconnectAction OnDisconnected;

        public delegate void RoomCreatedAction(RoomData room);
        public static event RoomCreatedAction OnRoomCreated;

        public delegate void RoomJoinedAction(RoomData room);
        public static event RoomJoinedAction OnRoomJoined;

        public delegate void LeftRoomAction();
        public static event LeftRoomAction OnLeftRoom;

        public delegate void GotRoomListAction(RoomListData rooms);
        public static event GotRoomListAction OnGotRoomList;

        public delegate void GotPlayerListAction(PlayerListData players);
        public static event GotPlayerListAction OnGotPlayerList;

        public delegate void RoomReadyAction();
        public static event RoomReadyAction OnRoomReady;

        public delegate void StartedGameAction();
        public static event StartedGameAction OnStartedGame;

        public delegate void ConnectionTimeoutAction();
        public static event ConnectionTimeoutAction OnConnectionTimeout;

        public void SocketConnected(string connectionID) {
            if(OnConnected != null) OnConnected(connectionID);
        }

        public void SocketDisconnected() {
            if(OnDisconnected != null) OnDisconnected();
        }

        public void SocketRoomCreated(string data) {
            if(OnRoomCreated != null) OnRoomCreated(JsonUtility.FromJson<RoomData>(data));
        }

        public void SocketRoomJoined(string data) {
            if(OnRoomJoined != null) OnRoomJoined(JsonUtility.FromJson<RoomData>(data));
        }

        public void SocketLeftRoom() {
            if(OnLeftRoom != null) OnLeftRoom();
        }

        public void SocketGotRoomList(string data) {
            if(OnGotRoomList != null) OnGotRoomList(JsonUtility.FromJson<RoomListData>(data));
        }

        public void SocketGotPlayerList(string data) {
            if(OnGotPlayerList != null) OnGotPlayerList(JsonUtility.FromJson<PlayerListData>(data));
        }

        public void SocketRoomReady() {
            if(OnRoomReady != null) OnRoomReady();
        }

        public void SocketStartedGame() {
            if(OnStartedGame != null) OnStartedGame();
        }

        public void SocketConnectionTimeout() {
            if(OnConnectionTimeout != null) OnConnectionTimeout();
        }
    }
}
