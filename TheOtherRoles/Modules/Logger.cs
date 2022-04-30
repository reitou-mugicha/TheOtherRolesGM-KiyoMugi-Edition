using System.Diagnostics;
using System;
using LogLevel = BepInEx.Logging.LogLevel;

namespace TheOtherRoles
{
    class Logger
    {
        public static bool isDetail = false;
        public static bool isAlsoInGame = false;
        public static void SendInGame(string text)
        {
            if (DestroyableSingleton<HudManager>._instance) DestroyableSingleton<HudManager>.Instance.Notifier.AddItem(text);
        }
        public static void SendToFile(string text, LogLevel level = LogLevel.Info, string tag = "")
        {
            string t = DateTime.Now.ToString("HH:mm:ss");
            string log_text = $"[{t}][{tag}]{text}";
            if (isDetail && TheOtherRolesPlugin.DebugMode.Value)
            {
                StackFrame stack = new StackFrame(2);
                string class_name = stack.GetMethod().ReflectedType.Name;
                string method_name = stack.GetMethod().Name;
                log_text = $"[{t}][{class_name}.{method_name}][{tag}]{text}";
            }
            TheOtherRolesPlugin.Logger.Log(level,log_text);
            if (isAlsoInGame) SendInGame(text);
        }
        public static void info(string text, string tag = "") => SendToFile(text, LogLevel.Info, tag);
        public static void warn(string text, string tag = "") => SendToFile(text, LogLevel.Warning, tag);
        public static void error(string text, string tag = "") => SendToFile(text, LogLevel.Error, tag);
        public static void fatal(string text, string tag = "") => SendToFile(text, LogLevel.Fatal, tag);
        public static void msg(string text, string tag = "") => SendToFile(text, LogLevel.Message, tag);
    }
}