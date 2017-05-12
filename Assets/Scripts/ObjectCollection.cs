using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Assets.Scripts
{

	public class ObjectCollection : IObjectCollection
	{
		private List<GameObject> _gameObjects = new List<GameObject>();

		public ReadOnlyCollection<GameObject> GameObjects
		{
			get
			{
				return _gameObjects.AsReadOnly();
			}
		}

		public GameObject this[int i] { get { return GameObjects[i]; } }


		public void AddObject(GameObject prefab, Transform placement)
		{
			var position = placement.position;
			var rotation = placement.rotation;
			var scale = placement.localScale;

			AddObject(prefab, position, rotation, scale);
		}

		public void AddObject(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale)
		{
			var newObject = UnityEngine.Object.Instantiate(prefab, position, rotation);
			newObject.transform.localScale = scale;
			_gameObjects.Add(newObject);
		}

		public void AddObject(
			GameObject prefab,
			Transform placement,
			Transform placementVariation)
		{
			var position = placement.position
				+ new Vector3(
					Random.Range(-placementVariation.position.x, placementVariation.position.x),
					Random.Range(-placementVariation.position.y, placementVariation.position.y),
					Random.Range(-placementVariation.position.z, placementVariation.position.z));
			var rotation = placement.rotation
				* Quaternion.Euler(
					Random.Range(-placementVariation.rotation.x, placementVariation.rotation.x),
					Random.Range(-placementVariation.rotation.y, placementVariation.rotation.y),
					Random.Range(-placementVariation.rotation.z, placementVariation.rotation.z));
			var scale = placement.localScale
				+ new Vector3(
					Random.Range(-placementVariation.localScale.x, placementVariation.localScale.x),
					Random.Range(-placementVariation.localScale.y, placementVariation.localScale.y),
					Random.Range(-placementVariation.localScale.z, placementVariation.localScale.z));
			AddObject(prefab, position, rotation, scale);
		}

		public void RemoveObject(GameObject gameObject)
		{
			_gameObjects.Remove(gameObject);
		}

		public void ShiftObjectPlacement(GameObject gameObject, Transform placementDelta)
		{
			if (!GameObjects.Contains(gameObject))
			{
				return;
			}
			gameObject.transform.position += placementDelta.position;
			gameObject.transform.rotation *= placementDelta.rotation;
			gameObject.transform.localScale += placementDelta.localScale;

		}

		public void ShiftAllObjectPlacements(Transform placementDelta)
		{
			foreach (var gameObject in GameObjects)
			{
				ShiftObjectPlacement(gameObject, placementDelta);
			}
		}

		public void UpdateObjectPlacement(GameObject gameObject, Transform newPlacement)
		{
			var position = newPlacement.position;
			var rotation = newPlacement.rotation;
			var scale = newPlacement.localScale;
			UpdateObjectPlacement(gameObject, position, rotation, scale);
		}

		public void UpdateObjectPlacement(
			GameObject gameObject,
			Transform newPlacement,
			Transform placementVariation)
		{
			var position = newPlacement.position
				+ new Vector3(
					Random.Range(-placementVariation.position.x, placementVariation.position.x),
					Random.Range(-placementVariation.position.y, placementVariation.position.y),
					Random.Range(-placementVariation.position.z, placementVariation.position.z));
			var rotation = newPlacement.rotation
				* Quaternion.Euler(
					Random.Range(-placementVariation.rotation.x, placementVariation.rotation.x),
					Random.Range(-placementVariation.rotation.y, placementVariation.rotation.y),
					Random.Range(-placementVariation.rotation.z, placementVariation.rotation.z));
			var scale = newPlacement.localScale
				+ new Vector3(
					Random.Range(-placementVariation.localScale.x, placementVariation.localScale.x),
					Random.Range(-placementVariation.localScale.y, placementVariation.localScale.y),
					Random.Range(-placementVariation.localScale.z, placementVariation.localScale.z));
			UpdateObjectPlacement(gameObject, position, rotation, scale);
		}

		private void UpdateObjectPlacement(
			GameObject gameObject,
			Vector3 position,
			Quaternion rotation,
			Vector3 scale)
		{
			gameObject.transform.position = position;
			gameObject.transform.rotation = rotation;
			gameObject.transform.localScale = scale;
		}

	}
}