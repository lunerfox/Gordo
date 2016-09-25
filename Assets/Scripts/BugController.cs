using UnityEngine;
using System.Collections;
using UnityEngine.Events;

/*
//Controls the Bug.
//Receives input from the Input Handler
//handles receiving powerup from MeatCube
*/

public class BugController : MonoBehaviour {

    public UnityEvent OnFeeding;
    public UnityEvent OnFallen;
    public UnityEvent OnDefeat;

    public PlayerInputHandler input;

    public float bugRollStrength = 4.0f;
    private float currentStrength;

    private Rigidbody rb;
    private int GordoLevel = 0;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        if (!rb) Debug.LogError("Failed to find Rigid Body on Bug");
        UpdateGordoLevel(0); //Sets up the parameters of the bug.
	}
	
	// Update is called once per frame
	void Update () {

	}

    void FixedUpdate ()
    {
        rb.AddForce(input.getForceVector()*currentStrength);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Food"))
        {
            Debug.Log("Bug has reached a meatCube");
            OnFeeding.Invoke();
            UpdateGordoLevel(1);
            Destroy(other.gameObject);
        }
    }

    void UpdateGordoLevel(int amt)
    {
        Debug.Log("Bug is now at gordo level " + GordoLevel);
        GordoLevel += amt;
        if (GordoLevel < 0) GordoLevel = 0;
        if (GordoLevel > 4) GordoLevel = 4;

        //In general, as the bug gets more massive, they're harder to control.
        //This serves to make the game more difficult as times goes on.
        switch(GordoLevel)
        {
            case 0:
                SetupBugParams(2, 4, 5, 1.0f);
                break;
            case 1:
                SetupBugParams(4, 8, 4, 1.25f);
                break;
            case 2:
                SetupBugParams(6, 12, 3, 1.4f);
                break;
            case 3:
                SetupBugParams(8, 16, 1, 1.5f);
                break;
            case 4:
                SetupBugParams(8, 16, 1, 1.55f);
                break;
            
            default:
                break;
        }
    }

    void SetupBugParams(float mass, float strength, float drag, float scale)
    {
        this.transform.localScale = new Vector3(scale, scale, scale);
        currentStrength = bugRollStrength * strength;
        rb.mass = mass;
        rb.drag = drag;
        Debug.Log("Bug now has mass " + mass + ", strength " + strength + ", drag" + drag);
    }

}
