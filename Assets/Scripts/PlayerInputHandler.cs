using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

//The player input handler handles a single player's input and translates it to
//a force vector for the Bug Controller.

public class PlayerInputHandler : MonoBehaviour
{

    public enum InputMethod { touchJoystick, AI };
    public InputMethod inputMethod;

    public enum Players { player1, player2, player3, player4 };
    public Players player;
    public AIManager AI;

    private string horizontalAxisName;
    private string verticalAxisName;

    private float horizontalAxis;
    private float verticalAxis;
        
    // Use this for initialization
    void Start()
    {
        if (inputMethod == InputMethod.touchJoystick)
        {
            switch (player)
            {
                case Players.player1:
                    SetupTouchControls(1);
                    break;

                case Players.player2:
                    SetupTouchControls(2);
                    break;

                case Players.player3:
                    SetupTouchControls(3);
                    break;

                case Players.player4:
                    SetupTouchControls(4);
                    break;

                default:
                    break;
            }
        }
        else if (inputMethod == InputMethod.AI)
        {
            //This Player is AI controlled. Create an AI component
            AI = this.gameObject.AddComponent<AIManager>();
            AI.InitAI(10.0f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        switch(inputMethod)
        {
            case InputMethod.touchJoystick:
                horizontalAxis = CrossPlatformInputManager.GetAxis(horizontalAxisName);
                verticalAxis = CrossPlatformInputManager.GetAxis(verticalAxisName);
                break;

            case InputMethod.AI:
                break;

            default:
                break;                
        }
        
    }

    void SetupTouchControls(int playerNum)
    {
        horizontalAxisName = "Horizontal" + playerNum.ToString();
        verticalAxisName = "Vertical" + playerNum.ToString();
        //Debug.Log("Player " + playerNum + " input configured - " + horizontalAxisName + ", " + verticalAxisName);
    }

    public Vector3 getForceVector()
    {
        if(inputMethod == InputMethod.touchJoystick )
        {
            return new Vector3(horizontalAxis, 0.0f, verticalAxis);
        }
        else if(inputMethod == InputMethod.AI)
        {
            return AI.Calculate();
        }
        else
        {
            return new Vector3(0, 0.0f, 0);
        }
            
    }

}