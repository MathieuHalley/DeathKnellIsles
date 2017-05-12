using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Assets.Scripts
{
	public interface INavigationNode
	{
		ReadOnlyCollection<INavigationNode> Neighbours { get; }
		bool IsPassable { get; }
		Vector3 Position { get; }
		IInfluenceController NodeInfluences { get; }
		IObjectCollection NodeObjects { get; }

		void AddNeighbour(INavigationNode node);
		void RemoveNeighbour(INavigationNode node);
		void SortNeighbours(INavigationNode neighbour);
		Vector3 GetNeighbourDirection(INavigationNode neighbour);
		IList<Vector3> GetNeighbourDirections();
	}

	public interface IObjectNavigationNode : INavigationNode
	{
		ReadOnlyCollection<GameObject> GameObjects { get; }

		void AddObject(GameObject gameObjectPrefab, Transform placement);
		void AddObject(GameObject gameObjectPrefab, Transform placement, Transform placementVariation);
		void RemoveObject(GameObject gameObject);

		void ShiftObjectPlacement(GameObject gameObject, Transform placementDelta);
		void ShiftAllObjectPlacements(Transform placementDelta);

		void UpdateObjectPlacement(GameObject gameObject, Transform newPlacement);
		void UpdateObjectPlacement(GameObject gameObject, Transform newPlacement, Transform placementVariation);
	}
}
