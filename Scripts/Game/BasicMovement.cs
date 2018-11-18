using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class BasicMovement : MonoBehaviour
    {
        public List<GameObject> WayPointsList = new List<GameObject>();
        public float Speed;
        public int NextWaypointIndex;

        // Use this for initialization
        void Start ()
        {
            NextWaypointIndex = 0;
        }

        // Update is called once per frame
        void FixedUpdate ()
        {
            var rigidBody = GetComponent<Rigidbody>();

            rigidBody.velocity = (WayPointsList[NextWaypointIndex].GetComponent<Transform>().position - transform.position).normalized;
            rigidBody.velocity *= Speed / Time.fixedDeltaTime;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Waypoint" || !WayPointsList.Contains(other.gameObject))
                return;

            if (WayPointsList.IndexOf(other.gameObject) == NextWaypointIndex)
                NextWaypointIndex = (NextWaypointIndex + 1) % WayPointsList.Count;
        }

    }
}
