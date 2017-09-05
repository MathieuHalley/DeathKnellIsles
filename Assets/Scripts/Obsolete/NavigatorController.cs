//namespace Assets.Scripts
//{
//    public class NavigatorController
//	{
//		public readonly NavigatorEvents Events;

//		public NavigatorController()
//		{
//			Events = new NavigatorEvents();
//			Events.AccelerationUpdated += HandleAccelerationUpdated;
//			Events.GameObjectUpdated += HandleGameObjectUpdated;
//			Events.NavigatorUpdated += HandleNavigatorUpdated;
//			Events.PreviousNodeUpdated += HandlePreviousNodeUpdated;
//			Events.PrioritiesUpdated += HandlePrioritiesUpdated;
//			Events.TargetNodeUpdated += HandleTargetNodeUpdated;
//			Events.TopSpeedUpdated += HandleTopSpeedUpdated;
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="eventArgs"></param>
//		private void HandleAccelerationUpdated(object sender, NavigatorUpdatedEventArgs eventArgs)
//		{
//			//Debug.Log("Navigator (" + eventArgs.ID + ") accleration updated: " + eventArgs.Acceleration.ToString());
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="eventArgs"></param>
//		private void HandleGameObjectUpdated(object sender, NavigatorUpdatedEventArgs eventArgs)
//		{
//			//Debug.Log("Navigator (" + eventArgs.ID + ") gameObject updated: " + eventArgs.GameObject.ToString());
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="eventArgs"></param>
//		private void HandleNavigatorUpdated(object sender, NavigatorUpdatedEventArgs eventArgs)
//		{
//			//Debug.Log("Navigator data (" + eventArgs.ID + ") data updated.");
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="eventArgs"></param>
//		private void HandlePreviousNodeUpdated(object sender, NavigatorUpdatedEventArgs eventArgs)
//		{
//			//Debug.Log("Navigator previous node (" + eventArgs.ID + ") previous node updated: " + eventArgs.PreviousNode.InfluencesToString());
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="eventArgs"></param>
//		private void HandlePrioritiesUpdated(object sender, NavigatorUpdatedEventArgs eventArgs)
//		{
//			//Debug.Log("Navigator priorities (" + eventArgs.ID + ") priorities updated: " + eventArgs.Navigator.PrioritiesToString());
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="eventArgs"></param>
//		private void HandleTargetNodeReached(object sender, NavigatorUpdatedEventArgs eventArgs)
//		{
//			//Debug.Log("Navigator target node(" + eventArgs.ID + ") reached: " + eventArgs.TargetNode.InfluencesToString());
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="eventArgs"></param>
//		private void HandleTargetNodeUpdated(object sender, NavigatorUpdatedEventArgs eventArgs)
//		{
//			//Debug.Log("Navigator target node(" + eventArgs.ID + ") target node updated: " + eventArgs.TargetNode.InfluencesToString());
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="eventArgs"></param>
//		private void HandleTopSpeedUpdated(object sender, NavigatorUpdatedEventArgs eventArgs)
//		{
//			//Debug.Log("Navigator top speed(" + eventArgs.ID + ") top speed updated: " + eventArgs.TopSpeed.ToString());
//		}

//		public void Dispose()
//		{
//			Events.AccelerationUpdated -= HandleAccelerationUpdated;
//			Events.GameObjectUpdated -= HandleGameObjectUpdated;
//			Events.NavigatorUpdated -= HandleNavigatorUpdated;
//			Events.PreviousNodeUpdated -= HandlePreviousNodeUpdated;
//			Events.PrioritiesUpdated -= HandlePrioritiesUpdated;
//			Events.TargetNodeUpdated -= HandleTargetNodeUpdated;
//			Events.TopSpeedUpdated -= HandleTopSpeedUpdated;
//		}
//	}
//}