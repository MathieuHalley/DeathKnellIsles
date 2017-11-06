using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class undeadEntity : entity {

    //will require knowledge of the tower if we ever need to retret to it
    GameObject referenceToTower;
    

    // Use this for initialization
    void Start()
    {
        referenceToTower = GameObject.FindGameObjectWithTag("Tower");
        if (referenceToTower == null)
        {
            Debug.Log("An undead entity has failed to find the tower object. Check your scene setup to enusre a toer exists and is tagged \"Tower\"");
        }

        navigationAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (navigationAgent == null)
        {
            Debug.Log("The undead entity did not have a navmesh agent. Please check prefab connection");
        }
        currentHealth = totalHealth;
        currentMoveSpeed = navigationAgent.speed;

        

    }

    public override GameObject checkForEnemies()
    {
        //set up our layer mask to only hit enemies that are acceptable to hit
        int mask = (1 << LayerMask.NameToLayer("living")) | (1 << LayerMask.NameToLayer("livingClimber")) | (1 << LayerMask.NameToLayer("livingFlying")) | (1 << LayerMask.NameToLayer("Villager"));
        Collider[] hitTargets = Physics.OverlapSphere(this.transform.position, perceptionRange, mask);
        if(hitTargets.Length == 0)
        {
            return null;
        }
        else
        {
            Collider closest = new Collider();
            float distance = Mathf.Infinity;
            foreach (Collider hit in hitTargets)
            {
                float currentDistance = (this.transform.position - hit.transform.position).sqrMagnitude;
                if (currentDistance < distance)
                {
                    distance = currentDistance;
                    closest = hit;
                }
            }
            return closest.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject enemyIfFound = checkForEnemies();
        if(enemyIfFound != null)
        {
            currentState = EntityStates.attacking;
            if (timeSinceLastAttacked >= attackSpeed)
                timeSinceLastAttacked = 0;

        }
        else if(currentState == EntityStates.attacking)
        {
            currentState = EntityStates.idle;
            //possibly not ideal, we would be allowed to attack again almost as soon we kill en enemy
            timeSinceLastAttacked = 0;
        }
        switch(currentState)
        {
            case EntityStates.attacking:
                {
                    attack(enemyIfFound);
                    break;
                }
            case EntityStates.retreating:
                {
                    if (navigationAgent.destination != referenceToTower.transform.position)
                    {
                        navigationAgent.destination = referenceToTower.transform.position;
                    }
                    break;
                }
            default:
                {
                    //both dead and idle states do nothing for undead
                    break;
                }
        }
    }
}
