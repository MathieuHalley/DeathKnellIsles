using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static entity;

public class livingEntity : entity {

    public override void move()
    {
        ///TODO depends how we are doing movement here, do we have a navmesh etc. 
        /// most liekly we will need to stop moving if attacking etc.
    }

    public override void attack()
    {
        //check fo rpotential attack targets in range. Targets may include buildinns
        //attack closest one if any
        Collider[] hitTargets =  Physics.OverlapSphere(this.transform.position, attackRange);

        Collider closest = new Collider();
        float distance = Mathf.Infinity;
        foreach(Collider hit in hitTargets)
        {
            float currentDistance = (this.transform.position - hit.transform.position).sqrMagnitude;
            if(currentDistance < distance)
            {
                distance = currentDistance;
                closest = hit;
            }
        }

        //replace this static function with our event system calls
        //send event to the selected entity to take damage
        print("attacking an entity " + closest.transform.name);
    }

	// Update is called once per frame
	void Update () {

        ///TODO determine when to begin attacking, preferably without always checking radius all the time
        if (isAttacking)
        {
            attack();
        }
        else
            move();
	}
}
