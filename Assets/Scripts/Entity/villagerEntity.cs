using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class villagerEntity : entity {

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
    }

    // Use this for initialization
    void Start()
    {
        navigationAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (navigationAgent == null)
        {
            Debug.Log("The undead entity did not have a navmesh agent. Please check prefab connection");
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
            currentState = EntityStates.attacking;
            if (timeSinceLastAttacked >= attackSpeed)
                timeSinceLastAttacked = 0;

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
                   ///TODO find and run towards nearest tower
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
