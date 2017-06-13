using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public sealed class NavigatorDataUpdatedEventArgs : EventArgs
	{
		private readonly NavigatorData data;
		public NavigatorData Data { get { return data; } }

		public NavigatorDataUpdatedEventArgs(NavigatorData data)
		{
			this.data = data;
		}
	}

	public sealed class NavigatorDataEvents
	{
		public event EventHandler<NavigatorDataUpdatedEventArgs> AccelerationUpdated = (s, e) => { };
		public event EventHandler<NavigatorDataUpdatedEventArgs> GameObjectUpdated = (s, e) => { };
		public event EventHandler<NavigatorDataUpdatedEventArgs> PreviousNodeUpdated = (s, e) => { };
		public event EventHandler<NavigatorDataUpdatedEventArgs> NavigatorDataUpdated = (s, e) => { };
		public event EventHandler<NavigatorDataUpdatedEventArgs> PrioritiesUpdated = (s, e) => { };
		public event EventHandler<NavigatorDataUpdatedEventArgs> TargetNodeUpdated = (s, e) => { };
		public event EventHandler<NavigatorDataUpdatedEventArgs> TargetNodeReached = (s, e) => { };
		public event EventHandler<NavigatorDataUpdatedEventArgs> TopSpeedUpdated = (s, e) => { };

		public void OnAccelerationUpdated(object sender, NavigatorData data)
		{
			OnUpdated(sender, data, AccelerationUpdated);
		}

		internal void OnGameObjectUpdated(object sender, NavigatorData data)
		{
			OnUpdated(sender, data, GameObjectUpdated);
		}

		public void OnPreviousNodeUpdated(object sender, NavigatorData data)
		{
			OnUpdated(sender, data, PreviousNodeUpdated);
		}

		public void OnPrioritiesUpdated(object sender, NavigatorData data)
		{
			OnUpdated(sender, data, PrioritiesUpdated);
		}

		public void OnNavigatorDataUpdated(object sender, NavigatorData data)
		{
			OnUpdated(sender, data, NavigatorDataUpdated);
		}

		public void OnTargetNodeReached(object sender, NavigatorData data)
		{
			OnUpdated(sender, data, TargetNodeReached);
		}

		public void OnTargetNodeUpdated(object sender, NavigatorData data)
		{
			OnUpdated(sender, data, TargetNodeUpdated);
		}

		public void OnTopSpeedUpdated(object sender, NavigatorData data)
		{
			OnUpdated(sender, data, TopSpeedUpdated);
		}

		private void OnUpdated(object sender, NavigatorData data, EventHandler<NavigatorDataUpdatedEventArgs> handler)
		{
			if (handler == null)
			{
				return;
			}

			handler(sender, new NavigatorDataUpdatedEventArgs(data));
			NavigatorDataUpdated(sender, new NavigatorDataUpdatedEventArgs(data));
		}
	}
}