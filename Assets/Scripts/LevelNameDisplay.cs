using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Shows the name of the level you're selecting

public class LevelNameDisplay : MonoBehaviour {

    public string[] LevelName;

    private Text text;
    private GameController game;

    void Start()
    {
        text = GetComponent<Text>();
        game = FindObjectOfType<GameController>();
    }


    public void UpdateLevelName()
    {
        int index = game.currentSelectedLevel;
        if(index < LevelName.Length && index >= 0)
        {
            text.text = LevelName[index];
        }
        else
        {
            Debug.LogWarning("Level Name Display: Index out of range - " + index);
        }   
    }
}
