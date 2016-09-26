using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

//Stores data about the current game state.

public class GameController : MonoBehaviour {

    public UnityEvent OnLevelChange;

    public bool isGameStarted = false;

    public bool isPlayer1Human;
    public bool isPlayer2Human;
    public bool isPlayer3Human;
    public bool isPlayer4Human;

    public int currentSelectedLevel;
    public int numberOfLevels;

    //Allow some settling time before starting certain actions.
    private float timeBeforeStart = 0.10f;
    private SpawnPositionController spawner;

    // Use this for initialization
    void Start () {
        spawner = FindObjectOfType<SpawnPositionController>();
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
        Debug.Log("Level changed to " + currentSelectedLevel);
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
    }

    public void EndGame()
    {
        isGameStarted = false;
    }

    public void ResetGame()
    {

    }

}
