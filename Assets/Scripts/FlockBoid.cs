using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class FlockBoid : IFlockBoid
    {
        public FlockBoid(FlockInfluenceValues influenceRanges, FlockInfluenceValues influenceWeights, float maxSpeed, Vector3 position)
        {
            Influences = new FlockInfluences();
            InfluenceRanges = influenceRanges;
            InfluenceWeights = influenceWeights;
            MaxSpeed = maxSpeed;
            Neighbours = new HashSet<IFlockBoid>();
            Position = position;
        }

        public Vector3 Direction
        {
            get
            {
                return Velocity.normalized;
            }
        }

        public FlockInfluences Influences { get; internal set; }
        public FlockInfluenceValues InfluenceRanges { get; internal set; }
        public FlockInfluenceValues InfluenceWeights { get; internal set; }
        public float MaxSpeed { get; internal set; }
        public HashSet<IFlockBoid> Neighbours { get; set; }
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

        public IEnumerable<IFlockBoid> UpdateNeighbourList(IEnumerable<IFlockBoid> neighbours = default(IEnumerable<IFlockBoid>))
        {
            if (neighbours != default(HashSet<IFlockBoid>) || neighbours != default(IEnumerable<IFlockBoid>))
            {
                Neighbours.UnionWith(neighbours);
            }

            Neighbours.RemoveWhere(n => (n.Position - Position).magnitude < InfluenceRanges.Max);

            return Neighbours;
        }

        /// <summary>Calculate the average velocity of the provided IFlockBoids relative to the velocity of this Boid.</summary>
        /// <returns>A Vector3 that contains the average relative velocity of the provided IFlockingBoids.</returns>
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

        /// <summary>
        ///     Calculate the average position of the provided IFlockingBoids relative to this Boid.
        /// </summary>
        /// <returns>The average position of the provided IFlockingBoids relative to this Boid.</returns>
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
            var neighbours = IdentifyNeighbours(Neighbours, InfluenceRanges.Alignment);

            Influences = new FlockInfluences(CalculateFlockAlignment(neighbours), CalculateFlockCohesion(neighbours), CalculateFlockSeparation(neighbours));
        }

        internal void CalculateMovementVelocity()
        {
            var velocity = Influences.Alignment * InfluenceWeights.Alignment
                         + Influences.Cohesion * InfluenceWeights.Cohesion
                         + Influences.Separation * InfluenceWeights.Separation;

            Velocity = Vector3.ClampMagnitude(velocity, MaxSpeed);
        }

        /// <summary>
        ///     Calculate the average position of the provided IFlockingBoids relative to this Boid.
        /// </summary>
        /// <returns> The average relative position of the provided neighbours.</returns>
        /// <remarks>
        ///     The average position is scaled to give closer neighbours a greater impact on the magnitude of the return value.
        ///     The magnitude of the return value is inversely scaled by the distance of each neighbour
        /// </remarks>
        internal Vector3 CalculateFlockSeparation(IEnumerable<IFlockBoid> neighbours)
        {
            var separation = new Vector3();

            foreach (var neighbour in neighbours)
            {
                var offset = Position - neighbour.Position;
                separation += offset.normalized / offset.sqrMagnitude;
            }

            separation /= neighbours.Count();
            separation = separation.normalized * MaxSpeed;
            separation -= Velocity;
            separation = Vector3.ClampMagnitude(separation, MaxSpeed);

            return separation;
        }

        internal IEnumerable<IFlockBoid> IdentifyNeighbours(
            IEnumerable<IFlockBoid> neighbourCandidates,
            float radius)
        {
            return from candidate in neighbourCandidates
                   where candidate != this
                   let candidateOffset = (candidate.Position - Position).sqrMagnitude
                   where candidateOffset < radius
                   select candidate;
        }
    }
}