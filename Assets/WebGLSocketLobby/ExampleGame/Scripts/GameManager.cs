using UnityEngine;
using UnityEngine.UI;
using WebGLSocketLobby;

public class GameManager : MonoBehaviour {

    public SpriteRenderer positionSprite;

    public GameObject playerWon;
    public GameObject playerLost;

    bool clickActive = false;
    float clickStartTime;

	void Awake () {
        SetSocketEventListeners();
	}

    void SetSocketEventListeners() {
        MyGameReceiver.OnSetGoalPosition += OnSetGoalPosition;
        MyGameReceiver.OnPlayerWonRound += OnPlayerWonRound;
        MyGameReceiver.OnPlayerLostRound += OnPlayerLostRound;
    }

    void Update() {
        if(!clickActive) return;

        if(Input.GetMouseButtonDown(0))
            SendClick();
    }

    void SendClick() {
        clickActive = false;

        float clickTime = Time.time - clickStartTime;

        PlayerClickData clickData = new PlayerClickData();
        clickData.playerID = SocketLobby.Player.playerID;
        clickData.clickTime = clickTime;

        MyGameSender.SendPlayerClick(JsonUtility.ToJson(clickData));
    }

    void OnSetGoalPosition(PositionData position) {
        playerWon.SetActive(false);
        playerLost.SetActive(false);
        
        positionSprite.transform.position = new Vector2(position.x, position.y);
        ShowPositionSprite();

        clickActive = true;
        clickStartTime = Time.time;
    }

    void OnPlayerWonRound() {
        HidePositionSprite();
        playerWon.SetActive(true);
        playerLost.SetActive(false);
    }

    void OnPlayerLostRound() {
        HidePositionSprite();
        playerWon.SetActive(false);
        playerLost.SetActive(true);
    }

    void HidePositionSprite() {
        positionSprite.enabled = false;
    }

    void ShowPositionSprite() {
        positionSprite.enabled = true;
    }
    
	
}
