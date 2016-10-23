using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using WebGLSocketLobby.IO;
using WebGLSocketLobby.Data;
using WebGLSocketLobby.Panels;

namespace WebGLSocketLobby.ListItems {
    public class RoomListItem : MonoBehaviour {

        public Text roomName;
        public Text roomPlayers;
        public Button joinRoomButton;

        public RoomData room;
        
        public void SetRoom(RoomData room) {
            this.room = room;

            roomName.text = room.roomName;
            roomPlayers.text = room.playerCount.ToString() + "/" + SocketLobby.Instance.settings.maxPlayersPerRoom.ToString();

            joinRoomButton.onClick.AddListener(() => {
                joinRoomButton.interactable = false;

                transform.parent.parent.GetComponent<LobbyPanel>().joinRoomLoading.SetActive(true);

                SocketSender.Send("JoinRoom", room.roomID);
            });
        }

    }
}

