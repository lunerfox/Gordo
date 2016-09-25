using UnityEngine;
using System.Collections;

//The player input handler handles a single player's input and translates it to
//a force vector for the Bug Controller.

public class PlayerInputHandler : MonoBehaviour {

    public enum InputMethod {touchJoystick};
    public InputMethod inputMethod;

    public string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input
    public string verticalAxisName = "Vertical"; // The name given to the vertical axis for the cross platform input

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
