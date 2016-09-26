using UnityEngine;
using System.Collections;

//Spawns various objects in different locations based on the level selected.

public class SpawnPositionController : MonoBehaviour {

    public GameObject[] playerBugs;
    public SpawnPointManager[] spawnpointByScene;

    public GameObject meatSpawnerPrefab;

    private GameController game;

    //This object will hold all of the temporary game objects. Which will aid in easy cleanup when
    //we're finished with a round.
    private GameObject tempParent;

	// Use this for initialization
	void Start () {
        game = FindObjectOfType<GameController>();
        tempParent = new GameObject();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //Resets everything
    public void SpawnObjectsAtLevel()
    {
        Debug.Log("Spawning objects at level " + game.currentSelectedLevel);

        SpawnPointManager spawnLocation = spawnpointByScene[game.currentSelectedLevel];

        //Spawn the meat spawner.
        var newMeatSpawner = Instantiate(meatSpawnerPrefab);
        newMeatSpawner.transform.position = spawnLocation.meatSpawner.transform.position;
        newMeatSpawner.transform.parent = tempParent.transform;

        //Spawn and reset the player objects.
        foreach (var player in playerBugs)
        {
            setupPlayerPosition(player, spawnLocation);
            var bug = player.GetComponent<BugController>();
            if(bug)
            {
                bug.ResetBug();
                player.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Cannot find bug controller");
            }
            
        }
    }

    void setupPlayerPosition(GameObject player, SpawnPointManager spawnLocation)
    {
        var whois = player.GetComponent<PlayerInputHandler>().player;
        
        switch(whois)
        {
            case PlayerInputHandler.Players.player1:
                player.transform.position = spawnLocation.P1SpawnPoint.transform.position;
                break;
            case PlayerInputHandler.Players.player2:
                player.transform.position = spawnLocation.P2SpawnPoint.transform.position;
                break;
            case PlayerInputHandler.Players.player3:
                player.transform.position = spawnLocation.P3SpawnPoint.transform.position;
                break;
            case PlayerInputHandler.Players.player4:
                player.transform.position = spawnLocation.P4SpawnPoint.transform.position;
                break;
            default:
                break;
        }
    }

    public void Cleanup()
    {
        Destroy(tempParent.GetComponentInChildren<MeatSpawner>().gameObject);
    }
}
