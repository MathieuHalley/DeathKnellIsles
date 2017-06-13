using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public sealed class NodeDataUpdatedEventArgs : EventArgs
	{
		private readonly NodeData data;
		public NodeData Data { get { return data; } }

		public NodeDataUpdatedEventArgs(NodeData data)
		{
			this.data = data;
		}
	}

	public sealed class NodeDataEvents
	{
		public event EventHandler<NodeDataUpdatedEventArgs> InfluencesUpdated = (s, e) => { };
		public event EventHandler<NodeDataUpdatedEventArgs> IsPassableUpdated = (s, e) => { };
		public event EventHandler<NodeDataUpdatedEventArgs> NeighboursUpdated = (s, e) => { };
		public event EventHandler<NodeDataUpdatedEventArgs> NodeDataUpdated = (s, e) => { };
		public event EventHandler<NodeDataUpdatedEventArgs> GameObjectUpdated = (s, e) => { };
		public event EventHandler<NodeDataUpdatedEventArgs> RadiusUpdated = (s, e) => { };
		public event EventHandler<NodeDataUpdatedEventArgs> PositionUpdated = (s, e) => { };

		public void OnInfluencesUpdated(object sender, NodeData data)
		{
			OnUpdated(sender, data, InfluencesUpdated);
		}

		public void OnIsPassableUpdated(object sender, NodeData data)
		{
			OnUpdated(sender, data, IsPassableUpdated);
		}

		public void OnNeighboursUpdated(object sender, NodeData data)
		{
			OnUpdated(sender, data, NeighboursUpdated);
		}

		public void OnNodeDataUpdated(object sender, NodeData data)
		{
			OnUpdated(sender, data, NodeDataUpdated);
		}

		public void OnGameObjectUpdated(object sender, NodeData data)
		{
			OnUpdated(sender, data, GameObjectUpdated);
		}

		public void OnPositionUpdated(object sender, NodeData data)
		{
			OnUpdated(sender, data, PositionUpdated);
		}

		public void OnRadiusUpdated(object sender, NodeData data)
		{
			OnUpdated(sender, data, RadiusUpdated);
		}

		private void OnUpdated(object sender, NodeData data, EventHandler<NodeDataUpdatedEventArgs> handler = null)
		{
			if (handler == null)
			{
				return;
			}

			handler(sender, new NodeDataUpdatedEventArgs(data));
			NodeDataUpdated(sender, new NodeDataUpdatedEventArgs(data));
		}
	}
}