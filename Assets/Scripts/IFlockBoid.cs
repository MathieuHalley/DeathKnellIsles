using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IFlockBoid
    {
        Vector3 Direction { get; }
        FlockInfluences Influences { get; }
        FlockInfluenceValues InfluenceRanges { get; }
        FlockInfluenceValues InfluenceWeights { get; }
        float MaxSpeed { get; }
        HashSet<IFlockBoid> Neighbours { get; set; }
        Vector3 Position { get; }
        Vector3 Velocity { get; }

        void UpdateBoid();

        IEnumerable<IFlockBoid> UpdateNeighbourList(IEnumerable<IFlockBoid> neighbours = default(IEnumerable<IFlockBoid>));
    }

    public struct FlockInfluences
    {
        public readonly Vector3 Alignment;
        public readonly Vector3 Cohesion;
        public readonly Vector3 Separation;

        public FlockInfluences(Vector3 alignment, Vector3 cohesion, Vector3 separation)
        {
            Alignment = alignment;
            Cohesion = cohesion;
            Separation = separation;
        }
    }

    public struct FlockInfluenceValues
    {
        public readonly float Alignment;
        public readonly float Cohesion;
        public readonly float Separation;

        public FlockInfluenceValues(float alignment, float cohesion, float separation)
        {
            Alignment = alignment;
            Cohesion = cohesion;
            Separation = separation;
        }

        public float Max
        {
            get
            {
                return Mathf.Max(Alignment, Cohesion, Separation);
            }
        }

        public float Mean
        {
            get
            {
                return (Alignment + Cohesion + Separation) / 3;
            }
        }

        public float Min
        {
            get
            {
                return Mathf.Min(Alignment, Cohesion, Separation);
            }
        }
    }
}