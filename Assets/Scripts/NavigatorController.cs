using System;
using UnityEngine;

namespace Assets.Scripts
{
	public class NavigatorController
	{
		private readonly Navigator navigator;
		private NavigatorDataEvents Events { get { return navigator.Data.Events; } }

		public NavigatorController(Navigator navigator)
		{
			this.navigator = navigator;

			Events.AccelerationUpdated += HandleAccelerationUpdated;
			Events.GameObjectUpdated += HandleGameObjectUpdated;
			Events.NavigatorDataUpdated += HandleNavigatorDataUpdated;
			Events.PreviousNodeUpdated += HandlePreviousNodeUpdated;
			Events.PrioritiesUpdated += HandlePrioritiesUpdated;
			Events.TargetNodeUpdated += HandleTargetNodeUpdated;
			Events.TopSpeedUpdated += HandleTopSpeedUpdated;
		}

		private void HandleAccelerationUpdated(object sender, NavigatorDataUpdatedEventArgs eventArgs)
		{
			//Debug.Log("Navigator (" + eventArgs.Data.ID + ") accleration updated: " + eventArgs.Data.Acceleration.ToString());
		}

		private void HandleGameObjectUpdated(object sender, NavigatorDataUpdatedEventArgs eventArgs)
		{
			//Debug.Log("Navigator (" + eventArgs.Data.ID + ") gameObject updated: " + eventArgs.Data.GameObject.ToString());
		}

		private void HandleNavigatorDataUpdated(object sender, NavigatorDataUpdatedEventArgs eventArgs)
		{
			//Debug.Log("Navigator data (" + eventArgs.Data.ID + ") data updated.");
		}

		private void HandlePreviousNodeUpdated(object sender, NavigatorDataUpdatedEventArgs eventArgs)
		{
			//Debug.Log("Navigator previous node (" + eventArgs.Data.ID + ") previous node updated: " + eventArgs.Data.PreviousNode.InfluencesToString());
		}

		private void HandlePrioritiesUpdated(object sender, NavigatorDataUpdatedEventArgs eventArgs)
		{
			//Debug.Log("Navigator priorities (" + eventArgs.Data.ID + ") priorities updated: " + eventArgs.Data.Navigator.PrioritiesToString());
		}

		private void HandleTargetNodeReached(object sender, NavigatorDataUpdatedEventArgs eventArgs)
		{
			//Debug.Log("Navigator target node(" + eventArgs.Data.ID + ") reached: " + eventArgs.Data.TargetNode.InfluencesToString());
		}

		private void HandleTargetNodeUpdated(object sender, NavigatorDataUpdatedEventArgs eventArgs)
		{
			//Debug.Log("Navigator target node(" + eventArgs.Data.ID + ") target node updated: " + eventArgs.Data.TargetNode.InfluencesToString());
		}

		private void HandleTopSpeedUpdated(object sender, NavigatorDataUpdatedEventArgs eventArgs)
		{
			//Debug.Log("Navigator top speed(" + eventArgs.Data.ID + ") top speed updated: " + eventArgs.Data.TopSpeed.ToString());
		}

		public void Dispose()
		{
			Events.AccelerationUpdated -= HandleAccelerationUpdated;
			Events.GameObjectUpdated -= HandleGameObjectUpdated;
			Events.NavigatorDataUpdated -= HandleNavigatorDataUpdated;
			Events.PreviousNodeUpdated -= HandlePreviousNodeUpdated;
			Events.PrioritiesUpdated -= HandlePrioritiesUpdated;
			Events.TargetNodeUpdated -= HandleTargetNodeUpdated;
			Events.TopSpeedUpdated -= HandleTopSpeedUpdated;
		}
	}
}