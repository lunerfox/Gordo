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
    public UnityEvent OnCollision;
    public UnityEvent OnFallen;
    public UnityEvent OnDefeat;

    public float bugRollStrength = 4.0f;
    private float currentStrength;

    public bool isFallen = false;

    private Rigidbody rb;
    private PlayerInputHandler input;

    private int GordoLevel = 0;
    private float collisionDisableTime = 0.15f;
    private float _collisionDisableTime = 0;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        if (!rb) Debug.LogError("Failed to find Rigid Body on " + this.gameObject.name);

        input = GetComponent<PlayerInputHandler>();
        if (!input) Debug.LogError("Failed to find player input handler on " + this.gameObject.name);

        UpdateGordoLevel(0); //Sets up the parameters of the bug.
    }
	
	// Update is called once per frame
	void Update () {

	}

    void FixedUpdate ()
    {
        rb.AddForce(input.getForceVector()*currentStrength);
        _collisionDisableTime -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Food"))
        {
            //Debug.Log("Bug has reached a meatCube");
            OnFeeding.Invoke();
            UpdateGordoLevel(1);
            Destroy(other.gameObject);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bug") && _collisionDisableTime < 0)
        {
            OnCollision.Invoke();
            var otherBug = collision.gameObject.GetComponent<BugController>();
            //Debug.Log(this.gameObject.name + " has collided with " + collision.gameObject.name);

            //Get the momentum of the two bugs
            Vector3 thisMu = this.CalculateMomentum();
            Vector3 otherMu = otherBug.CalculateMomentum();
            //Swap momentum between the two bugs.
            this.ResolveVelocity(otherMu);
            otherBug.ResolveVelocity(thisMu);
        }
        else if(collision.gameObject.CompareTag("Destroyer"))
        {
            Debug.Log("Bug " + this.gameObject.name + " has fallen!");
            OnFallen.Invoke();
            isFallen = true;
            Destroy(this.gameObject, 3.0f);
        }
    }

    Vector3 CalculateMomentum()
    {
        var momentum = rb.velocity * rb.mass;
        //Debug.Log("Current Momentum of " + this.gameObject.name +  " is " + momentum);
        return momentum;
    }

    void ResolveVelocity(Vector3 momentum)
    {
        rb.velocity = (momentum / rb.mass)*1.5f;
        //Debug.Log("New Velocity of " + this.gameObject.name + " is " + rb.velocity);
        //Disable collisions for some time. This will prevent the bugs swapping momentum twice.
        _collisionDisableTime = collisionDisableTime;
    }

    void UpdateGordoLevel(int amt)
    {
        //Debug.Log("Bug is now at gordo level " + GordoLevel);
        GordoLevel += amt;
        if (GordoLevel < 0) GordoLevel = 0;
        if (GordoLevel > 4) GordoLevel = 4;

        //In general, as the bug gets more massive, they're harder to control.
        //This serves to make the game more difficult as times goes on.
        switch(GordoLevel)
        {
            case 0:
                SetupBugParams(2.0f, 4.0f, 0.5f, 1.0f);
                break;
            case 1:
                SetupBugParams(4.0f, 8.0f, 0.5f, 1.25f);
                break;
            case 2:
                SetupBugParams(6.0f, 12.0f, 0.3f, 1.4f);
                break;
            case 3:
                SetupBugParams(8.0f, 16.0f, 0.2f, 1.5f);
                break;
            case 4:
                SetupBugParams(8.0f, 16.0f, 0.2f, 1.55f);
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
        //Debug.Log("Bug now has mass " + mass + ", strength " + strength + ", drag" + drag);
    }
}
