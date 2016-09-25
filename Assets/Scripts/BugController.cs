using UnityEngine;
using System.Collections;

public class BugController : MonoBehaviour {

    public PlayerInputHandler input;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        if (!rb) Debug.LogError("Failed to find Rigid Body on Bug");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate ()
    {
        rb.AddForce(input.getForceVector());
    }
}
