//using System;

//namespace Assets.Scripts
//{
//    public sealed class NodeUpdatedEventArgs : EventArgs
//	{
//		public readonly Node Node;

//		public NodeUpdatedEventArgs(Node node)
//		{
//			Node = node;
//		}
//	}

//	public sealed class NodeEvents
//	{
//		public event EventHandler<NodeUpdatedEventArgs> InfluencesUpdated = (s, e) => { };
//		public event EventHandler<NodeUpdatedEventArgs> IsPassableUpdated = (s, e) => { };
//		public event EventHandler<NodeUpdatedEventArgs> NeighboursUpdated = (s, e) => { };
//		public event EventHandler<NodeUpdatedEventArgs> NodeDataUpdated = (s, e) => { };
//		public event EventHandler<NodeUpdatedEventArgs> GameObjectUpdated = (s, e) => { };
//		public event EventHandler<NodeUpdatedEventArgs> RadiusUpdated = (s, e) => { };
//		public event EventHandler<NodeUpdatedEventArgs> PositionUpdated = (s, e) => { };

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="node"></param>
//		public void OnInfluencesUpdated(object sender, Node node)
//		{
//			OnUpdated(sender, node, InfluencesUpdated);
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="node"></param>
//		public void OnIsPassableUpdated(object sender, Node node)
//		{
//			OnUpdated(sender, node, IsPassableUpdated);
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="node"></param>
//		public void OnNeighboursUpdated(object sender, Node node)
//		{
//			OnUpdated(sender, node, NeighboursUpdated);
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="node"></param>
//		public void OnNodeDataUpdated(object sender, Node node)
//		{
//			OnUpdated(sender, node, NodeDataUpdated);
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="node"></param>
//		public void OnGameObjectUpdated(object sender, Node node)
//		{
//			OnUpdated(sender, node, GameObjectUpdated);
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="node"></param>
//		public void OnPositionUpdated(object sender, Node node)
//		{
//			OnUpdated(sender, node, PositionUpdated);
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="node"></param>
//		public void OnRadiusUpdated(object sender, Node node)
//		{
//			OnUpdated(sender, node, RadiusUpdated);
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="node"></param>
//		/// <param name="handler"></param>
//		private void OnUpdated(object sender, Node node, EventHandler<NodeUpdatedEventArgs> handler = null)
//		{
//			if (handler == null) { return; }

//			handler(sender, new NodeUpdatedEventArgs(node));
//			NodeDataUpdated(sender, new NodeUpdatedEventArgs(node));
//		}
//	}
//}