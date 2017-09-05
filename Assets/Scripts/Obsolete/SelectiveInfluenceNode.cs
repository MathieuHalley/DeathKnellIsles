using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    class SelectiveInfluenceNode : InfluenceNode, ISelectiveInfluenceNode
    {
        internal int interests;

        public IList<Influences> Interests
        {
            get
            {
                return (from influence in (IList<Influences>)Enum.GetValues(typeof(Influences))
                        where InfluenceIsInteresting(influence)
                        select influence).ToList();
            }
        }

        public IDictionary<Influences, float> InterestingInfluenceMagnitudes
        {
            get
            {
                return (from influence in InfluenceMagnitudes.Keys
                        where InfluenceIsInteresting(influence)
                        select influence)
                        .ToDictionary(influence => influence, influence => InfluenceMagnitudes[influence]);                
            }
        }

        public SelectiveInfluenceNode() : base()
        {
            interests = 0;
        }

        public SelectiveInfluenceNode(IList<Influences> interests,
                                      IDictionary<Influences, float> influences,
                                      IList<IInfluenceNode> neighbours,
                                      Vector3 position) 
            : base(influences, neighbours, position)
        {
            this.interests = 0;

            foreach (var interest in interests)
            {
                AddInterest(interest);
            }
        }

        public void AddInterest(Influences interest)
        {
            interests |= (int)interest;
        }

        public bool InfluenceIsInteresting(Influences interest)
        {
            return (interests & (int)interest) == (int)interest;
        }

        public void RemoveInterest(Influences interest)
        {
            interests &= ~(int)interest;
        }
    }
}
