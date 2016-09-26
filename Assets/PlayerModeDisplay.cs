using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class PlayerModeDisplay : MonoBehaviour {

    //Shows the current mode of the player (Human, or AI)
    public enum Players { player1, player2, player3, player4};
    public Players player;

    public string humanString;
    public string robotString;

    private Text text;
    private GameController game;

    void Start()
    {
        
    }

    public void changeMode(GameController game)
    {
        switch (player)
        {
            case Players.player1:
                setString(game.isPlayer1Human);
                break;
            case Players.player2:
                setString(game.isPlayer2Human);
                break;
            case Players.player3:
                setString(game.isPlayer3Human);
                break;
            case Players.player4:
                setString(game.isPlayer4Human);
                break;
            default:
                break;
        }
    }

    void setString(bool isHuman)
    {
        text = GetComponent<Text>();
        if (isHuman) text.text = humanString;
        else text.text = robotString;
    }

}
