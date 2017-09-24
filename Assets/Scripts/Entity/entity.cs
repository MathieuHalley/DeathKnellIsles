using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Events;

public abstract class entity : MonoBehaviour {

    //TODO reference to entity events once we establish how this is to be implemented

    public float totalHealth;
    public float maximumMovementSpeed;
    public float attackDamage;
    public float attackSpeed;
    //we can make melee units using a small attack range, (or maybe even 0). Also gives us the flexibitity to tweak enemies to stand futher away if they have bigger weapons
    public float attackRange;

    //our actual current health value without losing track of the total health we started with
    protected float currentHealth;
    protected float currentMoveSpeed; //Because we can be slowed down

    //probably we will need a switch here to change states. Possibly there is a better way to implement?
    protected bool isAttacking;

    abstract public void move();

    //attack will select a target and send them the event to take damage. 
    //most likely will be check if anything is in range, send damage event and play animation
    //specific behavior provided by sub-classes
    abstract public void attack();

    // Use this for initialization
    void Start()
    {
        currentHealth = totalHealth;
        currentMoveSpeed = maximumMovementSpeed;
    }

    //function which can be caled to cause this entity to takedamage
    //TODO likely this will need to be in our event system
    public void takeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if(currentHealth <= 0)
        {
            //TODO play animation, die etc.
        }
    }
}
