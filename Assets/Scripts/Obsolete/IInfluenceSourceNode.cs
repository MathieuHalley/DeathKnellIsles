using System.Collections.Generic;

namespace Assets.Scripts
{

    public interface IInfluenceSourceNode : IInfluenceNode
    {
        IDictionary<Influences, float> InfluenceLifetime { get; }

        void AddInfluenceSource (Influences influence, float magnitude, float lifetime = -1);
        float GetInfluenceSourceLifetime (Influences influence);
        void RemoveInfluenceSource(Influences influence);
        void SetInfluenceSourceLifetime (Influences influence, float lifetime = -1);
        void UpdateInfluenceSourceLifetime(Influences influence, float delta);
    }
}
