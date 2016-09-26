using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Simple Script to update the winner of the on Game End.

public class WinnerTextDisplay : MonoBehaviour {

    private Text text;
    private GameController game;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        game = FindObjectOfType<GameController>();
	}

    public void UpdateWinner()
    {
        text.text = game.winningPlayer;
    }
	
}
