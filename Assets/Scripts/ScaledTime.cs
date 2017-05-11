using UnityEngine;

namespace Assets.Scripts
{
	public static class ScaledTime
	{
		private static float _scaledSmoothDeltaTime;
		private static double _scaledFixedTime;
		private static double _scaledTime;
		private static double _scaledTimeSinceStartup;
		private static double _scaledTimeSinceSceneLoad;

		public static float timeScale { get; set; }

		public static float deltaTime
		{
			get
			{
				return Time.deltaTime * timeScale;
			}
		}

		public static float fixedDeltaTime
		{
			get
			{
				return Time.fixedDeltaTime * timeScale;
			}
		}

		public static float smoothDeltaTime
		{
			get
			{
				var f = 0.2f;
				var prevSmoothDeltaTime = _scaledSmoothDeltaTime;
				return _scaledSmoothDeltaTime = f * deltaTime + (f - 1) * prevSmoothDeltaTime;
			}
		}

		public static double fixedTime
		{
			get
			{
				return _scaledFixedTime += fixedDeltaTime;
			}
		}
		public static double time
		{
			get
			{
				return _scaledTime += deltaTime;
			}
		}

		public static double timeSinceStartup
		{
			get
			{
				if (Time.timeSinceLevelLoad < Time.deltaTime)
				{
					return _scaledTimeSinceStartup = Time.realtimeSinceStartup * timeScale;
				}
				else
				{
					return _scaledTimeSinceStartup += deltaTime;
				}
			}
		}
	
		public static double timeSinceSceneLoad
		{
			get
			{
				if (Time.timeSinceLevelLoad < Time.deltaTime)
				{
					return _scaledTimeSinceSceneLoad = Time.timeSinceLevelLoad * timeScale;
				}
				else
				{
					return _scaledTimeSinceSceneLoad += deltaTime;
				}
			}
		}
	}
}