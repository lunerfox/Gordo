using UnityEngine;
using System.Collections;
using System.Linq;

//The AI Manager is used to provide input in place of a
//player. It has a reference to the bug Controller (Also
//in the same gameObject and makes decisions based on 
//information about it's target.

public class AIManager : MonoBehaviour {

    private BugController bug;
    private BugController targetBug;

    private BugController [] enemyBugs;
    private float timeBetweenStrategies;

    private MeatSpawner meatSpawner;

    // Use this for initialization
    void Start() {
        
        //Debug.Log("Initializing AI for " + this.gameObject.name);
        bug = GetComponent<BugController>();
        enemyBugs = FindObjectsOfType<BugController>();

        enemyBugs = enemyBugs.Where(val => val != bug).ToArray();

        //Debug.Log("Found " + enemyBugs.Length + " other Bugs");
        targetBug = PickTarget();

        meatSpawner = FindObjectOfType<MeatSpawner>();
        if (!meatSpawner) Debug.LogError("AI Manager cannot find meat spawner");
    }

    public void InitAI(float timeBetweenStrat)
    {
        timeBetweenStrategies = timeBetweenStrat;
    }

    private BugController PickTarget()
    {
        int index = Random.Range(0, enemyBugs.Length);
        BugController target = enemyBugs[index];
        Debug.Log(this.gameObject.name + " picks " + target.gameObject.name + " as its target");
        return target;
    }

    public Vector3 Calculate()
    {
        if (!targetBug) Debug.LogWarning(this.gameObject.name + " does not have a valid target");
        else
        {
            //A valid target exists. we'll try to do something.
            var direction = targetBug.gameObject.transform.position - this.gameObject.transform.position;
            return direction.normalized;
        }
        return Vector3.zero;
    }

}
