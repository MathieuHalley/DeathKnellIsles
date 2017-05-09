using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Assets.Scripts
{
	[Flags]
	public enum Influences
	{
		None = 1 << 0,				//	0
		Boat = 1 << 1,				//	1
		Tower = 1 << 2,				//	2
		House = 1 << 3,				//	4
		Attacker = 1 << 4,			//	8
		Defender = 1 << 5,			//	16
		Villager = 1 << 6,			//	32
		HideBell = 1 << 7,			//	64
		PowerBell = 1 << 8,			//	128
		RallyBell = 1 << 9,			//	256
		SleepBell = 1 << 10,		//	512
		RetreatBell = 1 << 11,		//	1024
		RaiseBell = 1 << 12,		//	2048
		IndimidationBell = 1 << 13,	//	4196
	}

	public interface INavigationNode
	{
		Vector3 Position { get; }
		IList<INavigationNode> Neighbours { get; }
		void AddNeighbour(INavigationNode neighbour);
		Vector3 GetNeighbourDirection(INavigationNode neighbour);
		IList<Vector3> GetNeighbourDirections();
		int GetInfluence(Influences influences);
		void IncrementInfluence(Influences influences, int value);
		void RemoveNeighbour(INavigationNode neighbour);
		void SetInfluence(Influences influences, int value);
	}
}
