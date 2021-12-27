using Infinite8.Winigames.Packages.Utility;
using UnityEditor.PackageManager;

namespace ProGraphGroup.General.Manager
{
    public class LogManager : MonoSingleton<LogManager>
    {
        public bool isActive = true;
        public LogLevel logLevel = LogLevel.Debug;
        public bool forceConfigsForAll = false;
    }
}