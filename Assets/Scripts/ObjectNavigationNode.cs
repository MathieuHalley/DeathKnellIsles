using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Assets.Scripts
{

	public class ObjectNavigationNode : NavigationNode, IObjectNavigationNode
	{
		private List<GameObject> _gameObjects = new List<GameObject>();

		public ReadOnlyCollection<GameObject> GameObjects
		{
			get
			{
				return _gameObjects.AsReadOnly();
			}
		}

		//
		//	Constructors
		//
		public ObjectNavigationNode(Vector3 position, bool isPassable = true) 
			: base(position, new List<INavigationNode>(), new Dictionary<Influences, byte>(), isPassable)
		{ }

		public ObjectNavigationNode(Vector3 position, IList<INavigationNode> neighbours, bool isPassable = true) 
			: base(position, neighbours, new Dictionary<Influences, byte>(), isPassable)
		{ }

		public ObjectNavigationNode(Vector3 position, IList<INavigationNode> neighbours, IDictionary<Influences, byte> influences, bool isPassable = true)
			: base(position, neighbours, influences, isPassable)
		{ }

		public void AddObject(GameObject prefab)
		{
			var position = Position;
			var rotation = Quaternion.identity;
			var scale = Vector3.one;

			AddObject(prefab, position, rotation, scale);
		}

		public void AddObject(GameObject prefab, Transform localPlacement)
		{
			var position = Position + localPlacement.position;
			var rotation = localPlacement.rotation;
			var scale = localPlacement.localScale;

			AddObject(prefab, position, rotation, scale);
		}

		public void AddObject(
			GameObject prefab, 
			Transform localPlacement, 
			Transform placementVariation)
		{
			var position = Position + localPlacement.position 
				+ new Vector3(
					Random.Range(-placementVariation.position.x, placementVariation.position.x),
					Random.Range(-placementVariation.position.y, placementVariation.position.y),
					Random.Range(-placementVariation.position.z, placementVariation.position.z));
			var rotation = localPlacement.rotation 
				* Quaternion.Euler(
					Random.Range(-placementVariation.rotation.x, placementVariation.rotation.x),
					Random.Range(-placementVariation.rotation.y, placementVariation.rotation.y),
					Random.Range(-placementVariation.rotation.z, placementVariation.rotation.z));
			var scale = localPlacement.localScale
				+ new Vector3(
					Random.Range(-placementVariation.localScale.x, placementVariation.localScale.x),
					Random.Range(-placementVariation.localScale.y, placementVariation.localScale.y),
					Random.Range(-placementVariation.localScale.z, placementVariation.localScale.z));
			AddObject(prefab, position, rotation, scale);
		}

		private void AddObject (GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale)
		{
			var newObject = UnityEngine.Object.Instantiate(prefab, position, rotation);
			newObject.transform.localScale = scale;
			_gameObjects.Add(newObject);
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

		public void UpdateObjectPlacement(GameObject gameObject, Transform newLocalPlacement)
		{
			var position = Position + newLocalPlacement.position;
			var rotation = newLocalPlacement.rotation;
			var scale = newLocalPlacement.localScale;
			UpdateObjectPlacement(gameObject, position, rotation, scale);
		}

		public void UpdateObjectPlacement(
			GameObject gameObject, 
			Transform newLocalPlacement, 
			Transform placementVariation)
		{
			var position = Position + newLocalPlacement.position
				+ new Vector3(
					Random.Range(-placementVariation.position.x, placementVariation.position.x),
					Random.Range(-placementVariation.position.y, placementVariation.position.y),
					Random.Range(-placementVariation.position.z, placementVariation.position.z));
			var rotation = newLocalPlacement.rotation
				* Quaternion.Euler(
					Random.Range(-placementVariation.rotation.x, placementVariation.rotation.x),
					Random.Range(-placementVariation.rotation.y, placementVariation.rotation.y),
					Random.Range(-placementVariation.rotation.z, placementVariation.rotation.z));
			var scale = newLocalPlacement.localScale
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