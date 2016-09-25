using UnityEngine;
using System.Collections;

//Spawns Meat Cubes, ejecting the powerup randomly with some force.

//Research shows that Roly Polys actually do not eat meat, so these are Meat(tm) cubes. 
//Filled with delicious, meaty veggies.

public class MeatSpawner : MonoBehaviour {

    [Header("Timing parameters")]
    public float minTimeBetweenSpawn = 10.0f;
    public float maxTimeBetweenSpawn = 25.0f;

    [Header("Spawning parameters")]
    [Tooltip("Maximum force to launch in the XYZ directions")]
    public Vector3 maxForce;

    private float _timeToSpawn;

    public GameObject meatCube;

	// Use this for initialization
	void Start () {
        setupSpawnTimer();
    }
	
	// Update is called once per frame
	void Update () {
        _timeToSpawn -= Time.deltaTime;
        if(_timeToSpawn < 0)
        {
            spawnNewMeat();
        }
	}

    void spawnNewMeat()
    {
        //Instantiates new meat cube. 
        //This object is expected to be a physics object, so we can toss it.
        Debug.Log("Spawning new Meat");
        GameObject freshMeat = Instantiate(meatCube);
        freshMeat.transform.position = this.gameObject.transform.position;
        var rb = freshMeat.GetComponent<Rigidbody>();
        rb.AddForce(randomLaunchForce());
        rb.AddTorque(randomLaunchForce());
        setupSpawnTimer();
    }

    void setupSpawnTimer()
    {
        _timeToSpawn = Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
        Debug.Log("Time to next cube is " + _timeToSpawn);
    }

    public Vector3 randomLaunchForce()
    {
        float forceX = Random.Range(-maxForce.x, maxForce.x);
        float forceY = Random.Range(0, maxForce.y);
        float forceZ = Random.Range(-maxForce.z, maxForce.z);
        var launchForce = new Vector3(forceX, forceY, forceZ);
        Debug.Log("Launch force is " + launchForce);
        return launchForce;
    }
}
