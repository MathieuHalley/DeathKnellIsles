using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class FlockBoid : IFlockBoid
    {
        internal FlockBoid()
        {
        }

        public FlockBoid(FlockInfluences influences, float maxSpeed, Vector3 position)
        {
            Influences = influences;
            MaxSpeed = maxSpeed;
            Position = position;
        }

        public Vector3 Direction => Velocity.normalized;
        public FlockInfluences Influences { get; internal set; }
        public float MaxSpeed { get; }
        public HashSet<IFlockBoid> Neighbours { get; set; } = new HashSet<IFlockBoid>();
        public Vector3 Position { get; internal set; }
        public Vector3 Velocity { get; internal set; }

        public void UpdateBoid()
        {
            if (Neighbours != null && Neighbours.Any())
            {
                CalculateInfluences();
                CalculateMovementVelocity();
            }
        }

        public IEnumerable<IFlockBoid> UpdateNeighbourList(IEnumerable<IFlockBoid> neighbours = null)
        {
            if (neighbours != null)
            {
                Neighbours.UnionWith(neighbours);
            }

            var maxInfluenceRange = Mathf.Max(Influences.Alignment.Range, Influences.Cohesion.Range, Influences.Separation.Range);

            Neighbours.RemoveWhere(n => (n.Position - Position).magnitude > maxInfluenceRange);

            return Neighbours;
        }

        /// <summary>Calculate the average velocity of the provided IFlockBoids relative to the velocity of this Boid.</summary>
        /// <returns>A Vector3 that contains the average relative velocity of the provided IFlockBoids.</returns>
        internal Vector3 CalculateFlockAlignment(IEnumerable<IFlockBoid> neighbours)
        {
            var alignment = new Vector3();

            foreach (var neighbour in neighbours)
            {
                alignment += neighbour.Velocity;
            }

            alignment = alignment.normalized * MaxSpeed;

            return Vector3.ClampMagnitude(alignment - Velocity, MaxSpeed);
        }

        /// <summary>Calculate the average position of neighbours relative to this Boid.</summary>
        /// <returns>The average relative position of neighbours.</returns>
        internal Vector3 CalculateFlockCohesion(IEnumerable<IFlockBoid> neighbours)
        {
            var cohesion = new Vector3();

            foreach (var neighbour in neighbours)
            {
                cohesion += neighbour.Position - Position;
            }

            return cohesion / neighbours.Count();
        }

        internal void CalculateInfluences()
        {
            var neighbours = IdentifyNeighbours(Neighbours, Influences.Alignment.Range);
            var alignment = new FlockInfluence(CalculateFlockAlignment(neighbours), Influences.Alignment.Range, Influences.Alignment.Weight);
            var cohesion = new FlockInfluence(CalculateFlockCohesion(neighbours), Influences.Cohesion.Range, Influences.Cohesion.Weight);
            var separation = new FlockInfluence(CalculateFlockSeparation(neighbours), Influences.Separation.Range, Influences.Separation.Weight);

            Influences = new FlockInfluences(alignment, cohesion, separation);
        }

        internal void CalculateMovementVelocity()
        {
            var velocity = Influences.Alignment.WeightedInfluence + Influences.Cohesion.WeightedInfluence + Influences.Separation.WeightedInfluence;

            Velocity = Vector3.ClampMagnitude(velocity, MaxSpeed);
        }

        /// <summary>Calculate the average position of neighbours relative to this Boid.</summary>
        /// <returns>The average relative position of neighbours.</returns>
        /// <remarks>The magnitude of the return value is inversely scaled by the distance of each neighbour to give closer neighbours a greater impact on the magnitude of the return value.</remarks>
        internal Vector3 CalculateFlockSeparation(IEnumerable<IFlockBoid> neighbours)
        {
            var separation = new Vector3();

            foreach (var neighbour in neighbours)
            {
                var offset = Position - neighbour.Position;
                separation += offset.normalized / offset.sqrMagnitude;
            }

            separation /= neighbours.Count();
            separation = (separation.normalized * MaxSpeed) - Velocity;
            separation = Vector3.ClampMagnitude(separation, MaxSpeed);

            return separation;
        }

        internal IEnumerable<IFlockBoid> IdentifyNeighbours(IEnumerable<IFlockBoid> neighbourCandidates, float radius)
        {
            return from candidate in neighbourCandidates
                   where candidate != this
                   let candidateOffset = (candidate.Position - Position).sqrMagnitude
                   where candidateOffset < radius
                   select candidate;
        }
    }
}