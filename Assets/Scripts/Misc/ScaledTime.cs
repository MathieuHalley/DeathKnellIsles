using UnityEngine;

namespace Assets.Scripts.Misc
{
    public static class ScaledTimeManager
    {
        static float _scaledSmoothDeltaTime;
        static double _scaledFixedTime;
        static double _scaledTime;
        static double _scaledTimeSinceStartup;
        static double _scaledTimeSinceSceneLoad;

        public static float TimeScale { get; set; }
        public static float ScaledDeltaTime => Time.deltaTime * TimeScale;
        public static float ScaledFixedDeltaTime => Time.fixedDeltaTime * TimeScale;
        public static float ScaledSmoothDeltaTime => _scaledSmoothDeltaTime *= 0.2f * ScaledDeltaTime + (0.2f - 1) * _scaledSmoothDeltaTime;
        public static double ScaledFixedTime => _scaledFixedTime += ScaledFixedDeltaTime;
        public static double ScaledTime => _scaledTime += ScaledDeltaTime;

        public static double TimeSinceStartup => (Time.timeSinceLevelLoad >= Time.deltaTime) ? _scaledTimeSinceStartup += ScaledDeltaTime : Time.realtimeSinceStartup * TimeScale;
        public static double TimeSinceSceneLoad => (Time.timeSinceLevelLoad >= Time.deltaTime) ? _scaledTimeSinceSceneLoad += ScaledDeltaTime : Time.timeSinceLevelLoad * TimeScale;
    }
}