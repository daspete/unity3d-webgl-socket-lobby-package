using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using WebGLSocketLobby.IO;
using WebGLSocketLobby.Data;
using WebGLSocketLobby.ListItems;

namespace WebGLSocketLobby.Panels {
    public class RoomPanel : SinglePanel {

        public Text roomName;
        public Transform playerList;
        public Button leaveRoomButton;

        public PlayerListItem playerListItemPrefab;

        public GameObject leaveRoomLoading;
        public GameObject startGameCountDown;
        public Text startGameCountDownText;

        public int countdown;

        void Start() {
            roomName.text = SocketLobby.Room.roomName;

            leaveRoomButton.onClick.AddListener(() => {
                leaveRoomButton.interactable = false;

                leaveRoomLoading.SetActive(true);

                SocketSender.Send("LeaveRoom", SocketLobby.Room.roomID);
            });
        }

        void OnEnable() {
            SocketReceiver.OnLeftRoom += OnLeftRoom;
            SocketReceiver.OnGotPlayerList += OnGotPlayerList;
            SocketReceiver.OnRoomReady += OnRoomReady;

            SocketSender.Send("GetPlayerList", SocketLobby.Room.roomID);
        }

        void OnDisable() {
            SocketReceiver.OnLeftRoom -= OnLeftRoom;
            SocketReceiver.OnGotPlayerList -= OnGotPlayerList;
            SocketReceiver.OnRoomReady -= OnRoomReady;
        }

        void OnLeftRoom() {
            SocketLobby.Instance.BackToLobby();
        }

        void OnGotPlayerList(PlayerListData players) {
            PlayerListItem[] playerListItems = playerList.GetComponentsInChildren<PlayerListItem>();

            if(playerListItems != null) {
                for(int i = 0; i < playerListItems.Length; i++) {
                    bool remain = false;

                    for(int j = 0; j < players.players.Length; j++) {
                        if(playerListItems[i].player.playerID == players.players[j].playerID) {
                            remain = true;
                            break;
                        }
                    }

                    if(!remain) {
                        GameObject.Destroy(playerListItems[i].gameObject);
                    }
                }
            }

            playerListItems = playerList.GetComponentsInChildren<PlayerListItem>();

            for(int i = 0; i < players.players.Length; i++) {
                PlayerListItem playerListItem = null;

                if(playerListItems != null) {
                    for(int j = 0; j < playerListItems.Length; j++) {
                        if(playerListItems[j].player.playerID == players.players[i].playerID) {
                            playerListItem = playerListItems[j];
                            break;
                        }
                    }
                }

                if(playerListItem == null) {
                    playerListItem = Instantiate(playerListItemPrefab) as PlayerListItem;
                    playerListItem.transform.SetParent(playerList, false);
                    playerListItem.SetPlayer(players.players[i]);
                } else {
                    if(players.players[i].playerID != SocketLobby.Instance.connectionID) {
                        playerListItem.SetPlayer(players.players[i]);
                    }
                }
            }
        }

        void OnRoomReady() {
            startGameCountDownText.text = countdown.ToString();

            startGameCountDown.SetActive(true);

            StartCoroutine(CountDown());
        }

        IEnumerator CountDown() {
            for(int i = countdown - 1; i > 0; i--) {
                yield return new WaitForSeconds(1f);
                startGameCountDownText.text = i.ToString();
            }

            yield return new WaitForSeconds(1f);

            SocketSender.Send("StartGame", SocketLobby.Room.roomID);
        }

    }
}