using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using WebGLSocketLobby.IO;
using WebGLSocketLobby.Data;

namespace WebGLSocketLobby.ListItems {
    public class PlayerListItem : MonoBehaviour {

        public Button playerColorButton;
        public InputField playerNameInput;
        public Button playerReadyButton;

        public PlayerData player;

        int colorIndex = 0;

        void Start() {
            playerColorButton.onClick.AddListener(() => {
                colorIndex++;

                if(colorIndex >= SocketLobby.Instance.settings.playerColors.Length) {
                    colorIndex = 0;
                }

                playerColorButton.colors = SetColor(playerColorButton.colors, SocketLobby.Instance.settings.playerColors[colorIndex]);
                player.playerColor = SocketLobby.Instance.settings.playerColors[colorIndex];

                UpdatePlayer();
            });

            playerNameInput.onEndEdit.AddListener((string value) => {
                player.playerName = value;

                UpdatePlayer();
            });

            playerReadyButton.onClick.AddListener(() => {
                player.playerReady = !player.playerReady;

                SetPlayerReadyButton();

                UpdatePlayer();
            });
        }

        void UpdatePlayer() {
            UpdatePlayerData updatePlayerData = new UpdatePlayerData();
            updatePlayerData.roomID = SocketLobby.Room.roomID;
            updatePlayerData.player = player;

            SocketLobby.Instance.SetPlayer(player);

            SocketSender.Send("UpdatePlayer", JsonUtility.ToJson(updatePlayerData));
        }

        public void SetPlayer(PlayerData player) {
            this.player = player;

            if(player.playerID != SocketLobby.Instance.connectionID) {
                playerColorButton.interactable = false;
                playerNameInput.interactable = false;
                playerReadyButton.interactable = false;
            }

            this.playerNameInput.text = player.playerName;

            playerColorButton.colors = SetColor(playerColorButton.colors, player.playerColor);

            SetPlayerReadyButton();
        }

        void SetPlayerReadyButton() {
            playerReadyButton.colors = SetAlpha(playerReadyButton.colors, player.playerReady ? 1 : 0);
        }

        ColorBlock SetAlpha(ColorBlock colorBlock, float alpha) {
            Color color = colorBlock.normalColor;
            Color highlightColor = colorBlock.normalColor;
            color.a = alpha;
            highlightColor.a = 0.5f;

            colorBlock.normalColor = color;
            colorBlock.highlightedColor = color;
            colorBlock.pressedColor = highlightColor;
            colorBlock.disabledColor = color;

            return colorBlock;
        }

        ColorBlock SetColor(ColorBlock colorBlock, Color color) {
            colorBlock.normalColor = color;
            colorBlock.highlightedColor = color;
            colorBlock.pressedColor = color;
            colorBlock.disabledColor = color;

            return colorBlock;
        }
    }
}
