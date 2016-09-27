using UnityEngine;
using System.Collections;

//Simple Script to fire a particle effect.

public class ParticleFXSpawner : MonoBehaviour {

    public bool parentToObject = false;
    public GameObject particlePrefab;
    public float timeToLive = 5.0f;

    public void FireFX()
    {
        var newFX = Instantiate(particlePrefab);
        newFX.transform.position = this.gameObject.transform.position;
        if (parentToObject)
        {
            newFX.transform.parent = this.gameObject.transform;
        }
        else
        {
            newFX.transform.parent = null;
        }

        Destroy(newFX, timeToLive);
    }

}
