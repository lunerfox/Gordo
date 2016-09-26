using UnityEngine;
using System.Collections;

//Spawns various objects in different locations based on the level selected.

public class SpawnPositionController : MonoBehaviour {

    public GameObject[] playerBugs;
    public SpawnPointManager[] spawnpointByScene;

    public GameObject meatSpawnerPrefab;

    private GameController game;

	// Use this for initialization
	void Start () {
        game = FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SpawnObjectsAtLevel()
    {

    }
}
