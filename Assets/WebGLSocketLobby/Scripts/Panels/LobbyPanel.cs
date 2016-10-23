using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using WebGLSocketLobby.IO;
using WebGLSocketLobby.Data;
using WebGLSocketLobby.ListItems;

namespace WebGLSocketLobby.Panels {
    public class LobbyPanel : SinglePanel {

        public Transform roomList;
        public Button refreshButton;
        public InputField roomNameInput;
        public Button createRoomButton;

        public GameObject createRoomLoading;
        public GameObject joinRoomLoading;
        public GameObject fetchRoomListLoading;

        public RoomListItem roomListItemPrefab;

        public string defaultRoomName = "RoomName";

        void Start() {
            roomNameInput.text = defaultRoomName;

            createRoomButton.onClick.AddListener(() => {
                roomNameInput.interactable = false;
                createRoomButton.interactable = false;

                createRoomLoading.SetActive(true);
               
                SocketSender.Send("CreateRoom", roomNameInput.text == "" ? defaultRoomName : roomNameInput.text);
            });

            refreshButton.onClick.AddListener(() => {
                refreshButton.interactable = false;

                fetchRoomListLoading.SetActive(true);

                SocketSender.Send("GetRoomList");
            });

            fetchRoomListLoading.SetActive(true);
            SocketSender.Send("GetRoomList");
        }

        void OnEnable() {
            SocketReceiver.OnRoomCreated += OnRoomCreated;
            SocketReceiver.OnRoomJoined += OnRoomJoined;
            SocketReceiver.OnGotRoomList += OnGotRoomList;
        }

        void OnDisable() {
            SocketReceiver.OnRoomCreated -= OnRoomCreated;
            SocketReceiver.OnRoomJoined -= OnRoomJoined;
            SocketReceiver.OnGotRoomList -= OnGotRoomList;
        }

        void OnGotRoomList(RoomListData rooms) {
            CreateRoomListItems(rooms);

            fetchRoomListLoading.SetActive(false);
            refreshButton.interactable = true;
        }
        
        void OnRoomCreated(RoomData room) {
            SocketLobby.Instance.SetRoom(room);
        }

        void OnRoomJoined(RoomData room) {
            SocketLobby.Instance.SetRoom(room);
        }
        
        void CreateRoomListItems(RoomListData rooms) {
            RoomListItem[] roomListItems = roomList.GetComponentsInChildren<RoomListItem>();

            if(roomListItems != null) {
                for(int i = 0; i < roomListItems.Length; i++) {
                    bool remain = false;

                    for(int j = 0; j < rooms.rooms.Length; j++) {
                        if(roomListItems[i].room.roomID == rooms.rooms[j].roomID) {
                            remain = true;
                            break;
                        }
                    }

                    if(!remain) {
                        GameObject.Destroy(roomListItems[i].gameObject);
                    }
                }
            }

            roomListItems = roomList.GetComponentsInChildren<RoomListItem>();

            for(int i = 0; i < rooms.rooms.Length; i++) {
                RoomListItem roomListItem = null;

                if(roomListItems != null) {
                    for(int j = 0; j < roomListItems.Length; j++) {
                        if(roomListItems[j].room.roomID == rooms.rooms[i].roomID) {
                            roomListItem = roomListItems[j];
                            break;
                        }
                    }
                }

                if(roomListItem == null) {
                    roomListItem = Instantiate(roomListItemPrefab) as RoomListItem;
                    roomListItem.transform.SetParent(roomList, false);
                }

                roomListItem.SetRoom(rooms.rooms[i]);
            }
            
        }
    }
}
