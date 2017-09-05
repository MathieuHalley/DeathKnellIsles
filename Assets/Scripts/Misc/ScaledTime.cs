using UnityEngine;

namespace Assets.Scripts
{
	public static class ScaledTimeManager
	{
		private static float scaledSmoothDeltaTime;
		private static double scaledFixedTime;
		private static double scaledTime;
		private static double scaledTimeSinceStartup;
		private static double scaledTimeSinceSceneLoad;

		public static float TimeScale { get; set; }
		public static float ScaledDeltaTime { get { return Time.deltaTime * TimeScale; } }
		public static float ScaledFixedDeltaTime { get { return Time.fixedDeltaTime * TimeScale; } }
		public static float ScaledSmoothDeltaTime { get { return scaledSmoothDeltaTime *= 0.2f * ScaledDeltaTime + (0.2f - 1) * scaledSmoothDeltaTime; } }
		public static double ScaledFixedTime { get { return scaledFixedTime += ScaledFixedDeltaTime; } }
		public static double ScaledTime { get { return scaledTime += ScaledDeltaTime; } }

		public static double TimeSinceStartup
		{
			get
			{
				if (Time.timeSinceLevelLoad >= Time.deltaTime) { return scaledTimeSinceStartup += ScaledDeltaTime; }
				return scaledTimeSinceStartup = Time.realtimeSinceStartup * TimeScale;
			}
		}
	
		public static double TimeSinceSceneLoad
		{
			get
			{
				if (Time.timeSinceLevelLoad >= Time.deltaTime) { return scaledTimeSinceSceneLoad += ScaledDeltaTime; }
				return scaledTimeSinceSceneLoad = Time.timeSinceLevelLoad * TimeScale;
			}
		}
	}
}