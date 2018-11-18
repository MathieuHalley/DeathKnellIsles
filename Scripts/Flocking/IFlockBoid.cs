using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Flocking
{
    public interface IFlockBoid
    {
        Vector3 Direction { get; }
        FlockInfluences Influences { get; }
        float MaxSpeed { get; }
        HashSet<IFlockBoid> Neighbours { get; set; }
        Vector3 Position { get; }
        Vector3 Velocity { get; }

        void UpdateBoid();

        IEnumerable<IFlockBoid> UpdateNeighbourList(IEnumerable<IFlockBoid> neighbours = default(IEnumerable<IFlockBoid>));
    }

    public struct FlockInfluence
    {
        public FlockInfluence(Vector3 influence, float range, float weight = 1f)
        {
            Influence = influence;
            Range = range;
            Weight = weight;
        }

        public Vector3 Influence { get; }
        public float Range { get; }
        public float Weight { get; }
        public Vector3 WeightedInfluence => Influence * Weight;
    }

    public struct FlockInfluences
    {
        public FlockInfluences(FlockInfluence alignment, FlockInfluence cohesion, FlockInfluence separation)
        {
            Alignment = alignment;
            Cohesion = cohesion;
            Separation = separation;
        }

        public FlockInfluence Alignment { get; }
        public FlockInfluence Cohesion { get; }
        public FlockInfluence Separation { get; }
    }
}