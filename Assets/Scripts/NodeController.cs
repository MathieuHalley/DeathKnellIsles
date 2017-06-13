using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public class NodeController : IDisposable
	{
		private readonly Node node;
		private NodeDataEvents Events { get { return node.Data.Events; } }

		public NodeController(Node node)
		{
			this.node = node;

			Events.InfluencesUpdated += HandleInfluencesUpdated;
			Events.IsPassableUpdated += HandleIsPassableUpdated;
			Events.NeighboursUpdated += HandleNeighboursUpdated;
			Events.NodeDataUpdated += HandleNodeDataUpdated;
			Events.GameObjectUpdated += HandleGameObjectUpdated;
			Events.PositionUpdated += HandlePositionUpdated;
			Events.RadiusUpdated += HandleRadiusUpdated;
		}

		private void HandleInfluencesUpdated(object sender, NodeDataUpdatedEventArgs eventArgs)
		{
			//Debug.Log("Node (" + eventArgs.Data.ID + ") influences updated: " + eventArgs.Data.Node.InfluencesToString());
		}

		private void HandleIsPassableUpdated(object sender, NodeDataUpdatedEventArgs eventArgs)
		{
			//Debug.Log("Node (" + eventArgs.Data.ID + ") isPassable status updated: " + eventArgs.Data.Node.IsPassableToString());
		}

		private void HandleNeighboursUpdated(object sender, NodeDataUpdatedEventArgs eventArgs)
		{
			//Debug.Log("Node (" + eventArgs.Data.ID + ") neighbours updated: " + eventArgs.Data.Node.NeighboursToString());
		}

		private void HandleNodeDataUpdated(object sender, NodeDataUpdatedEventArgs eventArgs)
		{
			//Debug.Log("Node (" + eventArgs.Data.ID + ") data updated.");
		}

		private void HandleGameObjectUpdated(object sender, NodeDataUpdatedEventArgs eventArgs)
		{
			//Debug.Log("Node (" + eventArgs.Data.ID + ") gameObject updated: " + eventArgs.Data.Node.GameObjectToString());
		}

		private void HandlePositionUpdated(object sender, NodeDataUpdatedEventArgs eventArgs)
		{
			//Debug.Log("Node (" + eventArgs.Data.ID + ") position updated: " + eventArgs.Data.Node.PositionToString());
		}

		private void HandleRadiusUpdated(object sender, NodeDataUpdatedEventArgs eventArgs)
		{
			//Debug.Log("Node (" + eventArgs.Data.ID + ") radius updated: " + eventArgs.Data.Radius.ToString());
		}

		public void Dispose()
		{
			Events.InfluencesUpdated -= HandleInfluencesUpdated;
			Events.IsPassableUpdated -= HandleIsPassableUpdated;
			Events.NeighboursUpdated -= HandleNeighboursUpdated;
			Events.NodeDataUpdated -= HandleNodeDataUpdated;
			Events.GameObjectUpdated -= HandleGameObjectUpdated;
			Events.PositionUpdated -= HandlePositionUpdated;
		}
	}
}