using UnityEngine;
using System.Collections.ObjectModel;

namespace Assets.Scripts
{
	public interface IObjectCollection
	{
		ReadOnlyCollection<GameObject> GameObjects { get; }
		GameObject this[int i] { get; }

		void AddObject(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale);
		void AddObject(GameObject gameObjectPrefab, Transform placement);
		void AddObject(GameObject gameObjectPrefab, Transform placement, Transform placementVariation);
		void RemoveObject(GameObject gameObject);

		void ShiftObjectPlacement(GameObject gameObject, Transform placementDelta);
		void ShiftAllObjectPlacements(Transform placementDelta);

		void UpdateObjectPlacement(GameObject gameObject, Transform newPlacement);
		void UpdateObjectPlacement(GameObject gameObject, Transform newPlacement, Transform placementVariation);
	}
}