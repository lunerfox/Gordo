using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class MobileJoystickUIHandler : MonoBehaviour {

    //Shows the current mode of the player (Human, or AI)
    public enum Players { player1, player2, player3, player4 };
    public Players player;

    private GameController game;
    private Joystick joystick;
    private Image image;

	// Use this for initialization
	void Start () {
        game = FindObjectOfType<GameController>();
        joystick = GetComponent<Joystick>();
        image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        switch(player)
        {
            case Players.player1:
                setUI(game.isPlayer1Human);
                break;
            case Players.player2:
                setUI(game.isPlayer2Human);
                break;
            case Players.player3:
                setUI(game.isPlayer3Human);
                break;
            case Players.player4:
                setUI(game.isPlayer4Human);
                break;
            default:
                break;
        }
	}

    void setUI(bool switchOn)
    {
        if(game.isGameStarted)
        {
            joystick.enabled = switchOn;
            image.enabled = switchOn;
        }
        else
        {
            joystick.enabled = false;
            image.enabled = false;
        }
        
    }
}
