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
    private GameObject targetFood;

    private BugController [] enemyBugs;
    private float timeBetweenStrategies;
    private float _timeBetweenStrategies;

    public bool isAttacking = false;
    public bool isFeeding = false;

    private MeatSpawner meatSpawner;

    // Use this for initialization
    void Start() {
        
        //Debug.Log("Initializing AI for " + this.gameObject.name);
        bug = GetComponent<BugController>();
        enemyBugs = FindObjectsOfType<BugController>();

        enemyBugs = enemyBugs.Where(val => val != bug).ToArray();

        //Debug.Log("Found " + enemyBugs.Length + " other Bugs");

        meatSpawner = FindObjectOfType<MeatSpawner>();
        if (!meatSpawner) Debug.LogError("AI Manager cannot find meat spawner");
    }

    public void InitAI(float timeBetweenStrat, float startTime = 0)
    {
        timeBetweenStrategies = timeBetweenStrat;
        _timeBetweenStrategies = startTime;
    }

    void Update()
    {
        _timeBetweenStrategies -= Time.deltaTime;
        if(_timeBetweenStrategies <= 0)
        {
            Debug.Log("Picking a new strategy");
            //Do a few things
            int index = Random.Range(0, 2);
            //This is bad, but doing this in the interest of time!
            meatSpawner = FindObjectOfType<MeatSpawner>();

            if (index == 1 && meatSpawner.meatCount() > 0)
            {
                Debug.Log(this.gameObject.name + " is attempting to eat");
                isAttacking = false;
                isFeeding = true;
                targetFood = PickFoodTarget();
            }
            else
            {
                Debug.Log(this.gameObject.name + " is attacking");
                isAttacking = true;
                isFeeding = false;
                targetBug = PickTarget();
            }

            //Reset Timer
            _timeBetweenStrategies = timeBetweenStrategies;
        }
    }

    private GameObject PickFoodTarget()
    {
        //Get a piece of meat from the Meat spawner, then copy it (since the meat will destroy itself)
        GameObject meat = new GameObject();
        meat.transform.position = meatSpawner.provideMeat().transform.position;
        return meat;
    }

    private BugController PickTarget()
    {
        BugController[] validtargets;
        BugController target;
        //Check if all the bugs has already fallen
        validtargets = enemyBugs.Where(val => val.isFallen == false).ToArray();

        if(validtargets.Length > 0)
        {
            int index = Random.Range(0, validtargets.Length);
            target = validtargets[index];
            Debug.Log(this.gameObject.name + " picks " + target.gameObject.name + " as its target");
            return target;
        }
        else
        {
            //There's no targets to be had.
            return null;
        }
        
    }

    public Vector3 Calculate()
    {
        if(isAttacking)
        {
            if (!targetBug)
            { 
                //Debug.LogWarning(this.gameObject.name + " does not have a valid target"); 
            } 
            else
            {
                //A valid target exists. we'll try to do something.
                var direction = targetBug.gameObject.transform.position - this.gameObject.transform.position;
                return direction.normalized;
            }
        }

        if(isFeeding)
        {
            if (!targetFood) { }
            else
            {
                //A valid target exists. we'll try to do something.
                var direction = targetFood.transform.position - this.gameObject.transform.position;
                return direction.normalized;
            }
        }

        
        return Vector3.zero;
    }

}
