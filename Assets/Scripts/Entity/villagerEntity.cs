using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class villagerEntity : entity {

    //we are going to hold the closest enemy if we have one
    //we use this if needed to ensure we dont accidently run towards one
    //a house tat is far away but away from enemies is preferable to one that is close to them, but through them
    GameObject closestEnemy = null;
    public override GameObject checkForEnemies()
    {
        //check fo rpotential attack targets in range. Targets may include buildinns
        //attack closest one if any
        int mask = (1 << LayerMask.NameToLayer("living")) | (1 << LayerMask.NameToLayer("livingClimber")) | (1 << LayerMask.NameToLayer("livingFlying"));
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
        return null;
    }

    // Use this for initialization
    void Start()
    {
        navigationAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (navigationAgent == null)
        {
            Debug.Log("The villager entity did not have a navmesh agent. Please check prefab connection");
        }

        currentHealth = totalHealth;
        currentMoveSpeed = navigationAgent.speed;
    }

    // Update is called once per frame
    void Update()
    {

        GameObject enemyIfFound = checkForEnemies();
        if (enemyIfFound != null)
        {
            if (currentState == EntityStates.defending /*Flag for whether we were in a house or not. Are we in defense mode?*/)
            {
                currentState = EntityStates.attacking;
                if (timeSinceLastAttacked >= attackSpeed)
                    timeSinceLastAttacked = 0;
            }
            else if(currentState != EntityStates.retreating)
            {
                currentState = EntityStates.retreating;
            }

        }
        else if (currentState == EntityStates.attacking)
        {
            currentState = EntityStates.idle;
            //possibly not ideal, we would be allowed to attack again almost as soon we kill en enemy
            timeSinceLastAttacked = 0;
        }
        switch (currentState)
        {
            case EntityStates.attacking:
                {
                    attack(enemyIfFound);
                    break;
                }
            case EntityStates.retreating:
                {
                    //find nearest house
                    GameObject[] houses = GameObject.FindGameObjectsWithTag("House");
                    if (houses.Length == 0)
                    {
                        //no houses left run towards the tower
                        if (navigationAgent.destination != referenceToTower.transform.position)
                        {
                            navigationAgent.destination = referenceToTower.transform.position;
                        }
                    }
                    else
                    {
                        float distance = Mathf.Infinity;
                        Vector3 closestTansformPosition = Vector3.zero;
                        foreach(var house in houses)
                        {
                            //OK so basically the idea is that if we dot prodict two vecotors and get a positive result they are kinda in the same direction
                            //if they are facing the opposite way (desireable) we would expect negative
                            if (closestEnemy != null)
                            {
                                if (Vector3.Dot((closestEnemy.transform.position - this.transform.position), house.transform.position) > 0)
                                {
                                    //ignore this house
                                    continue;
                                }
                            }
                            float currentDistance = (house.transform.position - this.transform.position).sqrMagnitude;
                            if(currentDistance < distance)
                            {
                                distance = currentDistance;
                                closestTansformPosition = house.transform.position;

                            }
                        }
                        if(navigationAgent.destination != closestTansformPosition && closestTansformPosition != Vector3.zero)
                            navigationAgent.destination = closestTansformPosition;
                        else
                        {
                            if(navigationAgent.destination != referenceToTower.transform.position)
                            {
                                navigationAgent.destination = referenceToTower.transform.position;
                            }
                        }
                    }
                    break;
                }
            default:
                {
                    //move towards tower
                    if (navigationAgent.destination != referenceToTower.transform.position)
                    {
                        navigationAgent.destination = referenceToTower.transform.position;
                    }
                    break;
                }
        }
    }
}
