//using System;

//namespace Assets.Scripts
//{
//    public sealed class NavigatorUpdatedEventArgs : EventArgs
//	{
//		public readonly Navigator Navigator;

//		public NavigatorUpdatedEventArgs(Navigator navigator)
//		{
//			Navigator = navigator;
//		}
//	}

//	public sealed class NavigatorEvents
//	{
//		public event EventHandler<NavigatorUpdatedEventArgs> AccelerationUpdated = (s, e) => { };
//		public event EventHandler<NavigatorUpdatedEventArgs> GameObjectUpdated = (s, e) => { };
//		public event EventHandler<NavigatorUpdatedEventArgs> PreviousNodeUpdated = (s, e) => { };
//		public event EventHandler<NavigatorUpdatedEventArgs> NavigatorUpdated = (s, e) => { };
//		public event EventHandler<NavigatorUpdatedEventArgs> PrioritiesUpdated = (s, e) => { };
//		public event EventHandler<NavigatorUpdatedEventArgs> TargetNodeUpdated = (s, e) => { };
//		public event EventHandler<NavigatorUpdatedEventArgs> TargetNodeReached = (s, e) => { };
//		public event EventHandler<NavigatorUpdatedEventArgs> TopSpeedUpdated = (s, e) => { };

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="navigator"></param>
//		public void OnAccelerationUpdated(object sender, Navigator navigator)
//		{
//			OnUpdated(sender, navigator, AccelerationUpdated);
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="navigator"></param>
//		public void OnGameObjectUpdated(object sender, Navigator navigator)
//		{
//			OnUpdated(sender, navigator, GameObjectUpdated);
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="navigator"></param>
//		public void OnPreviousNodeUpdated(object sender, Navigator navigator)
//		{
//			OnUpdated(sender, navigator, PreviousNodeUpdated);
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="navigator"></param>
//		public void OnPrioritiesUpdated(object sender, Navigator navigator)
//		{
//			OnUpdated(sender, navigator, PrioritiesUpdated);
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="navigator"></param>
//		public void OnNavigatorUpdated(object sender, Navigator navigator)
//		{
//			OnUpdated(sender, navigator, NavigatorUpdated);
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="navigator"></param>
//		public void OnTargetNodeReached(object sender, Navigator navigator)
//		{
//			OnUpdated(sender, navigator, TargetNodeReached);
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="navigator"></param>
//		public void OnTargetNodeUpdated(object sender, Navigator navigator)
//		{
//			OnUpdated(sender, navigator, TargetNodeUpdated);
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="navigator"></param>
//		public void OnTopSpeedUpdated(object sender, Navigator navigator)
//		{
//			OnUpdated(sender, navigator, TopSpeedUpdated);
//		}

//		/// <summary></summary>
//		/// <param name="sender"></param>
//		/// <param name="navigator"></param>
//		/// <param name="handler"></param>
//		private void OnUpdated(object sender, Navigator navigator, EventHandler<NavigatorUpdatedEventArgs> handler)
//		{
//			if (handler == null) { return; }

//			handler(sender, new NavigatorUpdatedEventArgs(navigator));
//			NavigatorUpdated(sender, new NavigatorUpdatedEventArgs(navigator));
//		}
//	}
//}