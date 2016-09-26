using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

//The player input handler handles a single player's input and translates it to
//a force vector for the Bug Controller.

public class PlayerInputHandler : MonoBehaviour
{
    //0 for human, 1 for AI, controlled by GameController.
    public int inputMethod;

    public enum Players { player1, player2, player3, player4 };
    public Players player;
    public AIManager AI;
    
    private string horizontalAxisName;
    private string verticalAxisName;

    private float horizontalAxis;
    private float verticalAxis;

    public bool AxisSetupComplete = false;

    private GameController game;

    private bool prepareControlUpdate = true;    

    // Use this for initialization
    void Start()
    {
        //We look to the game controller as the authority of who is AI and who is human.
        game = FindObjectOfType<GameController>();

        //Since it's possible for the player to switch freely between AI and TouchControl, we should
        //create an AI just in case.
        AI = this.gameObject.AddComponent<AIManager>();
        AI.InitAI(4.0f);

        UpdatePlayerMode();

    }

    private void UpdatePlayerMode()
    {
        switch (player)
        {
            case Players.player1:
                setPlayerMode(game.isPlayer1Human, 1);
                break;

            case Players.player2:
                setPlayerMode(game.isPlayer2Human, 2);
                break;

            case Players.player3:
                setPlayerMode(game.isPlayer3Human, 3);
                break;

            case Players.player4:
                setPlayerMode(game.isPlayer4Human, 4);
                break;

            default:
                break;
        }
    }

    void setPlayerMode(bool isHuman, int player)
    {
        if(isHuman)
        {
            Debug.Log(this.gameObject.name + " is Human.");
            inputMethod = 0;
            if (!AxisSetupComplete) SetupTouchControls(player);

        }
        else
        {
            inputMethod = 1;
            Debug.Log(this.gameObject.name + " is AI.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(inputMethod)
        {
            //Joystick
            case 0:
                horizontalAxis = CrossPlatformInputManager.GetAxis(horizontalAxisName);
                verticalAxis = CrossPlatformInputManager.GetAxis(verticalAxisName);
                break;

            //AI
            case 1:
                break;

            default:
                break;                
        }
        
        if(game.isGameStarted && prepareControlUpdate)
        {
            Debug.Log("Setting up player handler for " + this.gameObject.name);
            //UpdatePlayerMode();
            prepareControlUpdate = false;
        }

        if(!prepareControlUpdate && !game.isGameStarted)
        {
            Debug.Log("Control Update now ready for " + this.gameObject.name);
            prepareControlUpdate = true;
        }

    }

    void SetupTouchControls(int playerNum)
    {
        if(!AxisSetupComplete)
        {
            horizontalAxisName = "Horizontal" + playerNum.ToString();
            verticalAxisName = "Vertical" + playerNum.ToString();
            Debug.Log("Player " + playerNum + " input configured - " + horizontalAxisName + ", " + verticalAxisName);
            AxisSetupComplete = true;
        }
    }

    public Vector3 getForceVector()
    {
        if(inputMethod == 0 )
        {
            return new Vector3(horizontalAxis, 0.0f, verticalAxis);
        }
        else if(inputMethod == 1)
        {
            return AI.Calculate();
        }
        else
        {
            return new Vector3(0, 0.0f, 0);
        }
            
    }

    //Switches between AI or Joystick mode.
    public void toggleMode(GameController game)
    {
        Debug.Log("Toggling Mode");
        //If the input method is Joystick, switch and vice versa.
        switch(player)
        {
            case Players.player1:
                setInputMethod(game.isPlayer1Human);
                break;
            case Players.player2:
                setInputMethod(game.isPlayer2Human);
                break;
            case Players.player3:
                setInputMethod(game.isPlayer3Human);
                break;
            case Players.player4:
                setInputMethod(game.isPlayer4Human);
                break;
            default:
                break;
        }

    }

    void setInputMethod(bool isHuman)
    {
        if (isHuman) inputMethod = 0;
        else inputMethod = 1;
    }

    public int getPlayer()
    {
        switch (player)
        {
            case Players.player1:
                return 1;

            case Players.player2:
                return 2;

            case Players.player3:
                return 3;

            case Players.player4:
                return 4;

            default:
                return 0;
        }
    }

}