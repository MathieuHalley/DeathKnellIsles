using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Misc;

namespace Assets.Scripts.Flocking
{
    public interface IFlockCollection
    {
        HashSet<HashSet<IFlockBoid>> Flocks { get; }
        HashSet<IFlockBoid> Boids { get; }

        IFlockCollection AddBoid(HashSet<IFlockBoid> flock, IFlockBoid boid);

        IFlockCollection AddBoids(HashSet<IFlockBoid> flock, IEnumerable<IFlockBoid> boids);

        IFlockCollection CreateFlock(IEnumerable<IFlockBoid> boids);

        IFlockCollection DestroyFlock(HashSet<IFlockBoid> flock);

        HashSet<IFlockBoid> GetFlock(IFlockBoid boid);

        IFlockCollection RemoveBoid(IFlockBoid boid);

        IFlockCollection RemoveBoid(HashSet<IFlockBoid> flock, IFlockBoid boid);

        IFlockCollection RemoveBoids(IEnumerable<IFlockBoid> boids);

        IFlockCollection RemoveBoids(HashSet<IFlockBoid> flock, IEnumerable<IFlockBoid> boids);

        IFlockCollection UpdateFlocks();
    }

    public class FlockCollection : IFlockCollection
    {
        public HashSet<HashSet<IFlockBoid>> Flocks { get; } = new HashSet<HashSet<IFlockBoid>>();

        public HashSet<IFlockBoid> Boids
            => Flocks.SelectMany(flock => flock)
                .DistinctBy(boid => new { boid.Position, boid.Velocity })
                .ToHashSet();

        public IFlockCollection AddBoid(HashSet<IFlockBoid> flock, IFlockBoid boid)
        {
            flock.Add(boid);

            if (!Flocks.Contains(flock))
            {
                Flocks.Add(flock);
            }

            return this;
        }

        public IFlockCollection AddBoids(HashSet<IFlockBoid> flock, IEnumerable<IFlockBoid> boids)
        {
            flock.UnionWith(boids);

            return this;
        }

        public IFlockCollection CreateFlock(IEnumerable<IFlockBoid> boids)
        {
            Flocks.Add(new HashSet<IFlockBoid>(boids));

            return this;
        }

        public IFlockCollection DestroyFlock(HashSet<IFlockBoid> flock)
        {
            if (Flocks.Contains(flock))
            {
                Flocks.Remove(flock);
            }

            return this;
        }

        public IFlockCollection DestroyFlocks(IEnumerable<HashSet<IFlockBoid>> flocks)
        {
            foreach (var flock in flocks)
            {
                DestroyFlock(flock);
            }

            return this;
        }

        public HashSet<IFlockBoid> GetFlock(IFlockBoid boid)
        {
            foreach (var flock in Flocks)
            {
                if (flock.Contains(boid))
                {
                    return flock;
                }
            }

            return null;
        }

        public IFlockCollection RemoveBoid(IFlockBoid boid)
        {
            GetFlock(boid).Remove(boid);

            return this;
        }

        public IFlockCollection RemoveBoid(HashSet<IFlockBoid> flock, IFlockBoid boid)
        {
            flock.Remove(boid);

            return this;
        }

        public IFlockCollection RemoveBoids(IEnumerable<IFlockBoid> boids)
        {
            var flockBoids = boids as IFlockBoid[] ?? boids.ToArray();
            foreach (var flock in Flocks)
            {
                foreach (var boid in flockBoids)
                {
                    if (flock.Contains(boid))
                    {
                        flock.Remove(boid);
                    }
                }
            }

            return this;
        }

        public IFlockCollection RemoveBoids(HashSet<IFlockBoid> flock, IEnumerable<IFlockBoid> boids)
        {
            foreach (var boid in boids)
            {
                flock.Remove(boid);
            }

            return this;
        }

        public IFlockCollection UpdateFlocks()
        {
            foreach (var flock in Flocks)
            {
                if (!flock.Any())
                {
                    Flocks.Remove(flock);
                }
            }

            return this;
        }
    }
}