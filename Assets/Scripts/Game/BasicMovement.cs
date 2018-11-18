using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class BasicMovement : MonoBehaviour
    {
        public List<GameObject> WayPointsList = new List<GameObject>();
        public float Speed;
        public int NextWaypointIndex;

        GameObject _nextWaypointGameObject => WayPointsList[NextWaypointIndex];
        Transform _transform => GetComponent<Transform>();
        Rigidbody _rigidbody => GetComponent<Rigidbody>();

        // Use this for initialization
        void Start ()
        {
            NextWaypointIndex = 0;
            
        }

        // Update is called once per frame
        void FixedUpdate ()
        {
            _rigidbody.velocity = (WayPointsList[NextWaypointIndex].GetComponent<Transform>().position - transform.position).normalized;
            _rigidbody.velocity *= Speed / Time.fixedDeltaTime;
            _transform.LookAt(_nextWaypointGameObject.GetComponent<Transform>().position);
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
