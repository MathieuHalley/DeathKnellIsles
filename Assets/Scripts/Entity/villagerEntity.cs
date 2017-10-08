using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class villagerEntity : entity {

    public override void move()
    {
        ///TODO depends how we are doing movement here, do we have a navmesh etc. 
        /// most liekly we will need to stop moving if attacking etc.
    }

    public override void attack()
    {
        if (timeSinceLastAttacked == 0)
        {
            //check fo rpotential attack targets in range. Targets may include buildinns
            //attack closest one if any
            int mask = (1 << LayerMask.NameToLayer("living") ) | (1 << LayerMask.NameToLayer("livingClimber") ) | (1 << LayerMask.NameToLayer("livingFlying") );
            Collider[] hitTargets = Physics.OverlapSphere(this.transform.position, attackRange, mask);

            if (hitTargets.Length == 0)
            {
                print("Stopping attack");
                isAttacking = false;
                timeSinceLastAttacked = 0;
                return;
            }

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

            //replace this static function with our event system calls
            //send event to the selected entity to take damage
            print("attacking an entity " + closest.transform.name);
            ExecuteEvents.Execute<entity>(closest.gameObject, null, (x, y) => x.takeDamage(attackDamage));
        }

        timeSinceLastAttacked += Time.deltaTime;
        if (timeSinceLastAttacked >= attackSpeed)
            timeSinceLastAttacked = 0;
    }

    // Use this for initialization
    void Start()
    {
        currentHealth = totalHealth;
        currentMoveSpeed = maximumMovementSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        ///TODO determine when to begin attacking, preferably without always checking radius all the time
        if (isAttacking)
        {
            attack();
        }
        else
            move();
    }
}
