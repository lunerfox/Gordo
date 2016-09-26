using UnityEngine;
using System.Collections;

//The Camera controller moves around the world via a spring joint.
//Locations are ordered in a list and traveled to via the MoveCamera function.

public class CameraController : MonoBehaviour {

    public GameObject[] locations;
    public bool loopLocations = true;

    private SpringJoint sj;
    private GameController game;

    // Use this for initialization
    void Start()
    {
        game = FindObjectOfType<GameController>();

        //Debug.Log("Found " + locations.Length + " places");
        sj = GetComponent<SpringJoint>();
        if (!sj) Debug.LogWarning("Spring Joint not found in " + this.gameObject.name);
        else
        {
            sj.autoConfigureConnectedAnchor = false;
        }
    }

    //Moves the camera to the current position of the game.
    public void MoveCamera()
    {
        int curPosition = game.currentSelectedLevel;
        if (curPosition >= locations.Length || curPosition < 0)
        {
            Debug.LogError("Move Camera: Index out of range");
        }
        else
        {
            sj.connectedAnchor = locations[curPosition].transform.position;
        }
    }
}
