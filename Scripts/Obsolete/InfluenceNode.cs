using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class InfluenceNode : IInfluenceNode
    {
        internal IDictionary<Influences, float> influenceMagnitudes;
        internal IList<IInfluenceNode> neighbours;
        internal Vector3 position;

        public IDictionary<Influences, float> InfluenceMagnitudes
        {
            get
            {
                return influenceMagnitudes;
            }

            internal set
            {
                if (influenceMagnitudes != value)
                {
                    influenceMagnitudes = value;
                }
            }
        }

        public IList<IInfluenceNode> Neighbours
        {
            get
            {
                return neighbours;
            }

            internal set
            {
                if (neighbours != value)
                {
                    neighbours = value;
                }
            }
        }

        public Vector3 Position
        {
            get
            {
                return position;
            }

            set
            {
                if (position != value)
                {
                    position = value;
                }
            }
        }

        public InfluenceNode() : this(new Dictionary<Influences, float>(), 
                                      new List<IInfluenceNode>(), 
                                      new Vector3()) { }

        public InfluenceNode(IDictionary<Influences, float> influenceMagnitudes, 
                             IList<IInfluenceNode> neighbours, 
                             Vector3 position)
        {
            this.influenceMagnitudes = influenceMagnitudes;
            this.neighbours = neighbours;
            this.position = position;
        }

        public void AddInfluence(Influences influence, 
                                 float magnitude)
        {
            InfluenceMagnitudes.Add(influence, magnitude);
        }

        public void AddInfluences(IDictionary<Influences, float> influenceMagnitudes)
        {
            foreach (var key in influenceMagnitudes.Keys)
            {
                AddInfluence(key, influenceMagnitudes[key]);
            }
        }

        public void AddNeighbour(IInfluenceNode neighbour)
        {
            if (!Neighbours.Contains(neighbour))
            {
                Neighbours.Add(neighbour);
            }

            if (!neighbour.Neighbours.Contains(this))
            {
                neighbour.AddNeighbour(this);
            }
        }

        public void AddNeighbours(IEnumerable<IInfluenceNode> neighbours)
        {
            foreach (var neighbour in neighbours)
            {
                AddNeighbour(neighbour);
            }
        }

        public void RemoveNeighbour(IInfluenceNode neighbour)
        {
            if (Neighbours.Contains(neighbour))
            {
                Neighbours.Remove(neighbour);
            }

            if (neighbour.Neighbours.Contains(this))
            {
                neighbour.RemoveNeighbour(this);
            }
        }

        public void RemoveInfluence(Influences influence)
        {
            InfluenceMagnitudes.Remove(influence);
        }

        public void SetInfluenceMagnitude(Influences influence, 
                                          float magnitude)
        {
            InfluenceMagnitudes[influence] = magnitude;
        }

        public void UpdateInfluenceMagnitude(Influences influence,
                                             float magnitudeDelta)
        {
            InfluenceMagnitudes[influence] += magnitudeDelta;
        }
    }
}
