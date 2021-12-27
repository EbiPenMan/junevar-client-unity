using ProGraphGroup.General.Manager;
using UnityEditor.PackageManager;

namespace ProGraphGroup.General.Utility
{
    public class Log
    {
        public bool isActive = true;
        public LogLevel logLevel = LogLevel.Debug;
        public string mainCat;


        public Log(string mainCat, LogLevel logLevel)
        {
            this.logLevel = logLevel;
            this.mainCat = mainCat;
            isActive = LogManager.Instance.isActive;
        }

        public Log(string mainCat, bool isActive)
        {
            this.isActive = isActive;
            this.mainCat = mainCat;
            logLevel = LogManager.Instance.logLevel;
        }

        public Log(string mainCat, bool isActive, LogLevel logLevel)
        {
            this.isActive = isActive;
            this.logLevel = logLevel;
            this.mainCat = mainCat;
        }

        public Log(string mainCat)
        {
            this.mainCat = mainCat;
            logLevel = LogManager.Instance.logLevel;
            isActive = LogManager.Instance.isActive;
        }

        private void log(LogLevel logLevel, string msg, string data = "", string[] tags = null)
        {
            string res = "";
            res += $"[{mainCat}]";


            if (tags != null && tags.Length > 0)
            {
                foreach (string tag in tags)
                {
                    res += $"[{tag}]";
                }
            }

            res += $" - {msg}";
            if (data != null)
            {
                res += $": {data} ";
            }

            switch (logLevel)
            {
                case LogLevel.Error:
                    UnityEngine.Debug.LogError(res);
                    break;
                case LogLevel.Warn:
                    UnityEngine.Debug.LogWarning(res);
                    break;
                case LogLevel.Info:
                case LogLevel.Verbose:
                case LogLevel.Debug:
                case LogLevel.Silly:
                    UnityEngine.Debug.Log(res);
                    break;
                default:
                    UnityEngine.Debug.Log(res);
                    break;
            }
        }

        public void Debug(string msg, string data = "", string[] tags = null)
        {
            if (LogManager.Instance.forceConfigsForAll)
            {
                if (!LogManager.Instance.isActive || LogManager.Instance.logLevel < LogLevel.Debug)
                    return;
            }
            else
            {
                if (!isActive || logLevel < LogLevel.Debug)
                    return;
            }

            log(logLevel, msg, data, tags);
        }


        public void Error(string msg, string data = "", string[] tags = null)
        {
            if (LogManager.Instance.forceConfigsForAll)
            {
                if (!LogManager.Instance.isActive || LogManager.Instance.logLevel < LogLevel.Error)
                    return;
            }
            else
            {
                if (!isActive || logLevel < LogLevel.Error)
                    return;
            }
            log(logLevel, msg, data, tags);
        }

        public void Warn(string msg, string data = "", string[] tags = null)
        {
            if (LogManager.Instance.forceConfigsForAll)
            {
                if (!LogManager.Instance.isActive || LogManager.Instance.logLevel < LogLevel.Warn)
                    return;
            }
            else
            {
                if (!isActive || logLevel < LogLevel.Warn)
                    return;
            }
            log(logLevel, msg, data, tags);
        }

        public void Info(string msg, string data = "", string[] tags = null)
        {
            if (LogManager.Instance.forceConfigsForAll)
            {
                if (!LogManager.Instance.isActive || LogManager.Instance.logLevel < LogLevel.Info)
                    return;
            }
            else
            {
                if (!isActive || logLevel < LogLevel.Info)
                    return;
            }
            log(logLevel, msg, data, tags);
        }
    }
}