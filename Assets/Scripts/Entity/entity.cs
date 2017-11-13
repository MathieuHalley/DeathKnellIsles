using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Game.Events;
using System;

public abstract class entity : MonoBehaviour, IGameEventHandler, IEventSystemHandler  {

    public enum EntityStates
    {
        attacking = 0,
        retreating,
        dead,
        idle,
        defending //of an object IE villagers that must defend a house
        ///Others as needed for bell effects
    }

    //TODO reference to entity events once we establish how this is to be implemented

    public float totalHealth;
    public float attackDamage;
    public float attackSpeed;
    //we can make melee units using a small attack range, (or maybe even 0). Also gives us the flexibitity to tweak enemies to stand futher away if they have bigger weapons
    public float attackRange;
    public float perceptionRange;

    //our actual current health value without losing track of the total health we started with
    protected float currentHealth;
    protected float currentMoveSpeed; //Because we can be slowed down

    //probably we will need a switch here to change states. Possibly there is a better way to implement?
    protected bool isAttacking;

    protected float timeSinceLastAttacked = 0;

    protected EntityStates currentState = EntityStates.idle;

    protected UnityEngine.AI.NavMeshAgent navigationAgent;

    //will require knowledge of the tower if we ever need to retret to it
    protected GameObject referenceToTower;

    public EventManager EventManager
    {
        get
        {
            ///TODO return useful thing
            throw new NotImplementedException();
        }
    }

    //attack will select a target and send them the event to take damage. 
    //most likely will be check if anything is in range, send damage event and play animation
    //specific behavior provided by sub-classes
    public void attack(GameObject target)
    {
        if(navigationAgent.destination != target.transform.position)
        {
            navigationAgent.destination = target.transform.position;
        }

        float distance = (this.transform.position - target.transform.position).magnitude;
        if (timeSinceLastAttacked == 0 && distance <= attackRange)
        {

            //replace this static function with our event system calls
            //send event to the selected entity to take damage
            print("attacking an entity " + target.transform.name);
            ExecuteEvents.Execute<entity>(target.gameObject, null, (x, y) => x.takeDamage(attackDamage));
        }

        timeSinceLastAttacked += Time.deltaTime;
    }

    //Use appropriate logic to find targets that this particular entity would consider an enemy unit.
    //This entity can then choose to attack / flee whatever seems appropriate
    //It is intended that this function will return a reference to the closest enemy. If any are found
    //If none are found this function may return a null object
    abstract public GameObject checkForEnemies();

    //function which can be caled to cause this entity to takedamage
    //TODO likely this will need to be in our event system
    public void takeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if(currentHealth <= 0)
        {
            //TODO play animation, die etc.
            Destroy(this.gameObject);
        }
    }
}
