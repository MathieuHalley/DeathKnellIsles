using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class InfluenceFlowCoordinator : MonoBehaviour, IInfluenceFlowCoordinator
    {

        public void PropagateInfluence(IEnumerable<IInfluenceNode> sourceNodes, 
                                       IEnumerable<Influences> influences)
        {
            var visitedNodes = new Queue<IInfluenceNode>(sourceNodes);

            while (visitedNodes.Count > 0)
            {
                var currentNode = visitedNodes.Dequeue();

                foreach (var neighbour in currentNode.Neighbours)
                {
                    var influenceMagnitudeDelta = (currentNode.Position - neighbour.Position).magnitude;

                    SpreadInfluenceToAgent(currentNode, neighbour, influences, influenceMagnitudeDelta);

                    if (!visitedNodes.Contains(neighbour))
                    {
                        visitedNodes.Enqueue(neighbour);
                    }
                }
            }
        }

        private void SpreadInfluenceToAgent(IInfluenceNode source,
                                            IInfluenceNode neighbour,
                                            IEnumerable<Influences> influences,
                                            float influenceMagnitudeDelta)
        {
            var influenceMagnitudes = 
                neighbour is ISelectiveInfluenceNode 
                    ? (neighbour as ISelectiveInfluenceNode).InterestingInfluenceMagnitudes 
                    : neighbour.InfluenceMagnitudes;

            foreach (var influence in influenceMagnitudes.Keys)
            {
                var currentInfluenceMagnitude = source.InfluenceMagnitudes[influence];
                var newInfluenceMagnitude = currentInfluenceMagnitude - influenceMagnitudeDelta;

                if (influenceMagnitudeDelta > currentInfluenceMagnitude || currentInfluenceMagnitude >= newInfluenceMagnitude)
                {
                    continue;
                }

                neighbour.SetInfluenceMagnitude(influence, newInfluenceMagnitude);
            }
        }

        public IList<IInfluenceNode> RankNodeNeighboursByInfluence(IEnumerable<IInfluenceNode> nodes, 
                                                                   Influences influence)
        {
            return (from node in nodes
                    where node.InfluenceMagnitudes.ContainsKey(influence)
                    orderby node.InfluenceMagnitudes[influence], node.InfluenceMagnitudes.Values.Sum()
                    select node).ToList();
        }

        private IList<IInfluenceNode> RankNodeNeighboursByInfluenceMagnitude(IEnumerable<IInfluenceNode> nodes, 
                                                                             Influences influence)
        {
            return (from node in nodes
                    where node.InfluenceMagnitudes.Keys.Contains(influence)
                    orderby node.InfluenceMagnitudes[influence]
                    select node).ToList();
        }

        private IList<IInfluenceNode> RankNodeNeighboursByInfluenceMagnitudeSum(IEnumerable<IInfluenceNode> nodes)
        {
            return (from node in nodes
                    orderby node.InfluenceMagnitudes.Values.Sum()
                    select node).ToList();
        }


    }
}
