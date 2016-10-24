using UnityEngine;
using WebGLSocketLobby.Game;
using WebGLSocketLobby.ExampleGame;

namespace WebGLSocketLobby.ExampleGame {
    public class MyGameReceiver : GameReceiver {

        public delegate void SetGoalPositionAction(PositionData position);
        public static event SetGoalPositionAction OnSetGoalPosition;

        public delegate void PlayerWonRoundAction();
        public static event PlayerWonRoundAction OnPlayerWonRound;

        public delegate void PlayerLostRoundAction();
        public static event PlayerLostRoundAction OnPlayerLostRound;

        public void SocketSetGoalPosition(string data) {
            if(OnSetGoalPosition != null)
                OnSetGoalPosition(JsonUtility.FromJson<PositionData>(data));
        }

        public void SocketPlayerWonRound() {
            if(OnPlayerWonRound != null)
                OnPlayerWonRound();
        }

        public void SocketPlayerLostRound() {
            if(OnPlayerLostRound != null)
                OnPlayerLostRound();
        }

    }
}