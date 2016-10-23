using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using WebGLSocketLobby.Panels;

namespace WebGLSocketLobby.UI {
    public class SocketLobbyUI : MonoBehaviour {

        public Transform panelHolder;

        public TopPanel topPanelPrefab;
        public ConnectPanel connectPanelPrefab;
        public LobbyPanel lobbyPanelPrefab;
        public RoomPanel roomPanelPrefab;

        public TopPanel topPanel;
        public ConnectPanel connectPanel;
        public LobbyPanel lobbyPanel;
        public RoomPanel roomPanel;


        public void CreateTopPanel() {
            if(topPanel != null)
                return;

            topPanel = Instantiate(topPanelPrefab) as TopPanel;
            topPanel.transform.SetParent(panelHolder, false);
        }

        public void CreateConnectPanel() {
            connectPanel = CreateSinglePanel(connectPanelPrefab) as ConnectPanel;
            connectPanel.SetServerUrl(SocketLobby.Instance.settings.gameServerUrl);
        }

        public void CreateLobbyPanel() {
            lobbyPanel = CreateSinglePanel(lobbyPanelPrefab) as LobbyPanel;
        }

        public void CreateRoomPanel() {
            roomPanel = CreateSinglePanel(roomPanelPrefab) as RoomPanel;
        }

        public void DeleteTopPanel() {
            if(topPanel != null)
                GameObject.Destroy(topPanel.gameObject);

            topPanel = null;
        }

        public void DeleteRoomPanel() {
            if(roomPanel != null)
                GameObject.Destroy(roomPanel.gameObject);

            roomPanel = null;
        }

        SinglePanel CreateSinglePanel(SinglePanel panelPrefab) {
            DeleteAllSinglePanels();

            SinglePanel panel = Instantiate(panelPrefab) as SinglePanel;
            panel.transform.SetParent(panelHolder, false);

            return panel;
        }

        void DeleteAllSinglePanels() {
            SinglePanel[] oldPanels = panelHolder.GetComponentsInChildren<SinglePanel>();

            for(int i = 0; i < oldPanels.Length; i++) {
                GameObject.Destroy(oldPanels[i].gameObject);
            }
        }
    }
}