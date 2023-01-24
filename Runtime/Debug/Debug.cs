using System.Diagnostics;
using System.Reflection;
using UnityEngine;

namespace CommonUtils.Debug
{
    public static class GameConsole
    {
        public static bool StackTraceLogs = true;
        public static bool inProduction = false;

        public static void Space() {
            if (inProduction) return;

            string data = formatLogMessage(" ------------------------------------------------------------ ");
            UnityEngine.Debug.Log(data);
        }

        public static void Log(string info) {
            if (inProduction) return;

            string data = formatLogMessage(info.ToString());
            UnityEngine.Debug.Log(data);
        }

        public static void Log(int info) {
            if (inProduction) return;

            string data = formatLogMessage(info.ToString());
            UnityEngine.Debug.Log(data);
        }

        public static void Log(float info) {
            if (inProduction) return;

            string data = formatLogMessage(info.ToString());
            UnityEngine.Debug.Log(data);
        }

        public static void Log(double info) {
            if (inProduction) return;

            string data = formatLogMessage(info.ToString());
            UnityEngine.Debug.Log(data);
        }

        public static void Log(bool info) {
            if (inProduction) return;

            string data = formatLogMessage(info.ToString());
            UnityEngine.Debug.Log(data);
        }

        public static void Log(Vector2 info) {
            if (inProduction) return;

            string data = formatLogMessage(info.ToString());
            UnityEngine.Debug.Log(data);
        }

        public static void Log(Vector3 info) {
            if (inProduction) return;

            string data = formatLogMessage(info.ToString());
            UnityEngine.Debug.Log(data);
        }

        /////

        public static void Warn(string info) {
            if (inProduction) return;

            string data = formatLogMessage(info.ToString());
            UnityEngine.Debug.LogWarning(data);
        }

        public static void Warn(int info) {
            if (inProduction) return;

            string data = formatLogMessage(info.ToString());
            UnityEngine.Debug.LogWarning(data);
        }

        public static void Warn(float info) {
            if (inProduction) return;

            string data = formatLogMessage(info.ToString());
            UnityEngine.Debug.LogWarning(data);
        }

        public static void Warn(double info) {
            if (inProduction) return;

            string data = formatLogMessage(info.ToString());
            UnityEngine.Debug.LogWarning(data);
        }

        public static void Warn(bool info) {
            if (inProduction) return;

            string data = formatLogMessage(info.ToString());
            UnityEngine.Debug.LogWarning(data);
        }

        public static void Warn(Vector2 info) {
            if (inProduction) return;

            string data = formatLogMessage(info.ToString());
            UnityEngine.Debug.LogWarning(data);
        }

        public static void Warn(Vector3 info) {
            if (inProduction) return;

            string data = formatLogMessage(info.ToString());
            UnityEngine.Debug.LogWarning(data);
        }

        ///

        private static string formatLogMessage(string data) {
            if (StackTraceLogs) {
                MethodBase method = (new StackFrame(2)).GetMethod();
                MethodBase caller_method = (new StackFrame(3)).GetMethod();
                if (caller_method != null)
                    return "[ " + caller_method.Name + "->" + method.DeclaringType.Name + ":" + method.Name + " ] - " + data.ToString();
                else
                    return "[ " + method.DeclaringType.Name + ":" + method.Name + " ] - " + data.ToString();
            } else {
                return data;
            }
        }
    }
}