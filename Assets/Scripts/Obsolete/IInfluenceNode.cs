using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IInfluenceNode
    {
        IDictionary<Influences, float> InfluenceMagnitudes { get; }
        IList<IInfluenceNode> Neighbours { get; }
        Vector3 Position { get; set; }

        void AddInfluence(Influences influence, float magnitude);
        void AddInfluences(IDictionary<Influences, float> influenceMagnitudes);
        void AddNeighbour(IInfluenceNode neighbour);
        void AddNeighbours(IEnumerable<IInfluenceNode> neighbours);
        void RemoveInfluence(Influences influence);
        void RemoveNeighbour(IInfluenceNode neighbour);
        void SetInfluenceMagnitude(Influences influence, float magnitude);
        void UpdateInfluenceMagnitude(Influences influence, float magnitudeDelta);
    }
}
