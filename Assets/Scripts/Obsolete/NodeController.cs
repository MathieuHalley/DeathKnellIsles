//using System;

//namespace Assets.Scripts
//{
//    public class NodeController : IDisposable
//	{
//		public readonly NodeEvents Events;

//		/// <summary></summary>
//		public NodeController()
//		{
//			Events = new NodeEvents();
//			Events.InfluencesUpdated += HandleInfluencesUpdated;
//			Events.IsPassableUpdated += HandleIsPassableUpdated;
//			Events.GameObjectUpdated += HandleGameObjectUpdated;
//			Events.NeighboursUpdated += HandleNeighboursUpdated;
//			Events.NodeDataUpdated += HandleNodeDataUpdated;
//			Events.PositionUpdated += HandlePositionUpdated;
//			Events.RadiusUpdated += HandleRadiusUpdated;
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="eventArgs"></param>
//		private void HandleInfluencesUpdated(object sender, NodeUpdatedEventArgs eventArgs)
//		{
//			//Debug.Log("Node (" + eventArgs.Data.ID + ") influences updated: " + eventArgs.Data.Node.InfluencesToString());
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="eventArgs"></param>
//		private void HandleIsPassableUpdated(object sender, NodeUpdatedEventArgs eventArgs)
//		{
//			//Debug.Log("Node (" + eventArgs.Data.ID + ") isPassable status updated: " + eventArgs.Data.Node.IsPassableToString());
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="eventArgs"></param>
//		private void HandleNeighboursUpdated(object sender, NodeUpdatedEventArgs eventArgs)
//		{
//			//Debug.Log("Node (" + eventArgs.Data.ID + ") neighbours updated: " + eventArgs.Data.Node.NeighboursToString());
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="eventArgs"></param>
//		private void HandleNodeDataUpdated(object sender, NodeUpdatedEventArgs eventArgs)
//		{
//			//Debug.Log("Node (" + eventArgs.Data.ID + ") data updated.");
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="eventArgs"></param>
//		private void HandleGameObjectUpdated(object sender, NodeUpdatedEventArgs eventArgs)
//		{
//			//Debug.Log("Node (" + eventArgs.Data.ID + ") gameObject updated: " + eventArgs.Data.Node.GameObjectToString());
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="eventArgs"></param>
//		private void HandlePositionUpdated(object sender, NodeUpdatedEventArgs eventArgs)
//		{
//			//Debug.Log("Node (" + eventArgs.Data.ID + ") position updated: " + eventArgs.Data.Node.PositionToString());
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="eventArgs"></param>
//		private void HandleRadiusUpdated(object sender, NodeUpdatedEventArgs eventArgs)
//		{
//			//Debug.Log("Node (" + eventArgs.Data.ID + ") radius updated: " + eventArgs.Data.Radius.ToString());
//		}

//		/// <summary></summary>
//		public void Dispose()
//		{
//			Events.InfluencesUpdated -= HandleInfluencesUpdated;
//			Events.IsPassableUpdated -= HandleIsPassableUpdated;
//			Events.GameObjectUpdated -= HandleGameObjectUpdated;
//			Events.NeighboursUpdated -= HandleNeighboursUpdated;
//			Events.NodeDataUpdated -= HandleNodeDataUpdated;
//			Events.PositionUpdated -= HandlePositionUpdated;
//			Events.RadiusUpdated -= HandleRadiusUpdated;

//		}
//	}
//}