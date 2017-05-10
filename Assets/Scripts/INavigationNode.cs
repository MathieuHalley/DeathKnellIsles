using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Assets.Scripts
{
	[Flags]
	public enum Influences
	{
		None = 0,					//	0
		Boat = 1 << 0,				//	1
		Tower = 1 << 1,				//	2
		House = 1 << 2,				//	4
		Attacker = 1 << 3,			//	8
		Defender = 1 << 4,			//	16
		Villager = 1 << 5,			//	32
		HideBell = 1 << 6,			//	64
		PowerBell = 1 << 7,			//	128
		RallyBell = 1 << 8,			//	256
		SleepBell = 1 << 9,			//	512
		RetreatBell = 1 << 10,		//	1024
		RaiseBell = 1 << 11,		//	2048
		IndimidationBell = 1 << 12,	//	4196
	}

	public interface INavigationNode
	{
		ReadOnlyCollection<INavigationNode> Neighbours { get; }
		bool IsPassable { get; }
		Vector3 Position { get; }

		void AddNeighbour(INavigationNode neighbour);
		void RemoveNeighbour(INavigationNode neighbour);

		Vector3 GetNeighbourDirection(INavigationNode neighbour);
		IList<Vector3> GetNeighbourDirections();

		int GetInfluence(Influences influences);
		void IncrementInfluence(Influences influences, int value);
		void SetInfluence(Influences influences, int value);
		void ResetInfluence(Influences influences);

		void SetPassable(bool passable);
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
