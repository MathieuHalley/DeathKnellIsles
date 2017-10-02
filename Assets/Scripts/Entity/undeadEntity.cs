using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class undeadEntity : entity {

    public override void move()
    {
        ///TODO depends how we are doing movement here, do we have a navmesh etc. 
        /// most liekly we will need to stop moving if attacking etc.
        /// 
                //for this test project we will move towards "attack target" until we are in attack range
        GameObject target = GameObject.Find("AttackTarget");
        Vector3 direction = (target.transform.position - this.transform.position).normalized;
        direction *= currentMoveSpeed;
        this.transform.Translate(direction * Time.deltaTime);

        if ((target.transform.position - this.transform.position).sqrMagnitude <= (attackRange * attackRange))
        {
            isAttacking = true;
        }
        else if (isAttacking == true)
        {
            isAttacking = false;
        }
    }

    // Use this for initialization
    void Start()
    {
        currentHealth = totalHealth;
        currentMoveSpeed = maximumMovementSpeed;
    }

    public override void attack()
    {
        if (timeSinceLastAttacked == 0)
        {
            //check fo rpotential attack targets in range. Targets may include buildinns
            //attack closest one if any

            //need to correclty set up masks here so that we can not attack friendly types
            int mask = (1 << LayerMask.NameToLayer("living")) | (1 << LayerMask.NameToLayer("livingClimber")) | (1 <<LayerMask.NameToLayer("livingFlying"));
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
        }

        timeSinceLastAttacked += Time.deltaTime;
        if (timeSinceLastAttacked >= attackSpeed)
            timeSinceLastAttacked = 0;
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
