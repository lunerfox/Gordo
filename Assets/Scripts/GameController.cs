using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

//Stores data about the current game state.

public class GameController : MonoBehaviour {

    public UnityEvent OnLevelChange;
    public UnityEvent OnStartGame;
    public UnityEvent OnEndGame;
    public UnityEvent OnResetGame;

    public bool isGameStarted = false;

    public bool isPlayer1Human;
    public bool isPlayer2Human;
    public bool isPlayer3Human;
    public bool isPlayer4Human;

    public int currentSelectedLevel;
    public int numberOfLevels;

    public float GordoModeTime = 10.0f;

    public string winningPlayer = "Nobody";

    //Allow some settling time before starting certain actions.
    private float timeBeforeStart = 0.10f;
    private SpawnPositionController spawner;
    private BugController[] bugs;

    private PlayerModeDisplay [] playerModeUIs;
    
    // Use this for initialization
    void Start () {
        spawner = FindObjectOfType<SpawnPositionController>();
        playerModeUIs = FindObjectsOfType<PlayerModeDisplay>();

        updatePlayerInputMode();

    }
	
	// Update is called once per frame
	void Update () {
	    if(timeBeforeStart > 0)
        {
            timeBeforeStart -= Time.deltaTime;
            if(timeBeforeStart < 0)
            {
                //Do this once.
                changeScene(currentSelectedLevel);
            }
        }
	}

    void changeScene(int scene)
    {
        if (scene < 0) scene = numberOfLevels - 1;
        currentSelectedLevel = scene % numberOfLevels;
        //Debug.Log("Level changed to " + currentSelectedLevel);
        OnLevelChange.Invoke();
    }

    public void nextScene()
    {
        currentSelectedLevel++;
        changeScene(currentSelectedLevel);
    }

    public void prevScene()
    {
        currentSelectedLevel--;
        changeScene(currentSelectedLevel);
    }

    public void StartGame()
    {
        isGameStarted = true;
        OnStartGame.Invoke();
        bugs = FindObjectsOfType<BugController>();
    }

    public void ResetGame()
    {
        OnResetGame.Invoke();
    }

    public void UpdatePlayerMode(int player)
    {
        switch(player)
        {
            case 1:
                isPlayer1Human = !isPlayer1Human;
                break;
            case 2:
                isPlayer2Human = !isPlayer2Human;
                break;
            case 3:
                isPlayer3Human = !isPlayer3Human;
                break;
            case 4:
                isPlayer4Human = !isPlayer4Human;
                break;
            default:
                Debug.Log("GameController - Update Player Mode - Invalid input");
                break;
        }

        updatePlayerInputMode();
    }

    public void CheckForWinner()
    {
        //Debug.Log("Checking for winner");
        int count = 0;
        BugController winningBug = null;
        //Count the number of bugs that hasn't fallen. If only one hasn't, then they've won!
        foreach (var bug in bugs)
        {
            if (!bug.isFallen)
            {
                count++;
                winningBug = bug;
            }
        }

        //Debug.Log(count + " bug(s) remaining.");

        //One and only one bug must be left.
        if(count == 1)
        {
            winningPlayer = winningBug.playerFriendlyName;
            Debug.Log("Waaahoo! " + winningPlayer + " is the winner!");
            isGameStarted = false;
            OnEndGame.Invoke();
        }
    }

    void updatePlayerInputMode()
    {
        //Set the player mode UI to it's default state.
        foreach (var playerModeUI in playerModeUIs)
        {
            playerModeUI.changeMode();
        }
    }

}
