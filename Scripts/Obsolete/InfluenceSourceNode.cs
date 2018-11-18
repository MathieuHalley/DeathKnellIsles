using System.Collections.Generic;

namespace Assets.Scripts
{
    class InfluenceSourceNode : InfluenceNode, IInfluenceSourceNode
    {
        internal IDictionary<Influences, float> influenceLifetime;

        public IDictionary<Influences, float> InfluenceLifetime
        {
            get
            {
                return influenceLifetime;
            }

            internal set
            {
                if (influenceLifetime != value)
                {
                    influenceLifetime = value;
                }
            }
        }

        public void AddInfluenceSource(Influences influence, 
                                       float magnitude, 
                                       float duration)
        {
            AddInfluence(influence, magnitude);
            InfluenceLifetime.Add(influence, duration);
        }

        public float GetInfluenceSourceLifetime(Influences influence)
        {
            return InfluenceLifetime[influence];
        }

        public void RemoveInfluenceSource(Influences influence)
        {
            RemoveInfluence(influence);
            InfluenceLifetime.Remove(influence);
        }

        public void SetInfluenceSourceLifetime(Influences influence, 
                                               float lifetime = -1f)
        {
            InfluenceLifetime[influence] = lifetime;
        }

        public void UpdateInfluenceSourceLifetime(Influences influence, 
                                                  float lifetimeDelta)
        {
            InfluenceLifetime[influence] += lifetimeDelta;
        }
    }
}
