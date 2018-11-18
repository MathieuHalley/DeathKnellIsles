using System.Collections.Generic;

namespace Assets.Scripts
{
    public interface IInfluenceFlowCoordinator
    {
        void PropagateInfluence(IEnumerable<IInfluenceNode> nodes, IEnumerable<Influences> influences);
        IList<IInfluenceNode> RankNodeNeighboursByInfluence(IEnumerable<IInfluenceNode> nodes, Influences influence);
    }
}
