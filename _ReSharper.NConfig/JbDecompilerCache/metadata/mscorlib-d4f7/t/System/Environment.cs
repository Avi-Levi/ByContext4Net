// Type: System.Environment
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System
{
    [ComVisible(true)]
    public static class Environment
    {
        #region SpecialFolder enum

        [ComVisible(true)]
        public enum SpecialFolder
        {
            Desktop = 0,
            Programs = 2,
            MyDocuments = 5,
            Personal = 5,
            Favorites = 6,
            Startup = 7,
            Recent = 8,
            SendTo = 9,
            StartMenu = 11,
            MyMusic = 13,
            MyVideos = 14,
            DesktopDirectory = 16,
            MyComputer = 17,
            NetworkShortcuts = 19,
            Fonts = 20,
            Templates = 21,
            CommonStartMenu = 22,
            CommonPrograms = 23,
            CommonStartup = 24,
            CommonDesktopDirectory = 25,
            ApplicationData = 26,
            PrinterShortcuts = 27,
            LocalApplicationData = 28,
            InternetCache = 32,
            Cookies = 33,
            History = 34,
            CommonApplicationData = 35,
            Windows = 36,
            System = 37,
            ProgramFiles = 38,
            MyPictures = 39,
            UserProfile = 40,
            SystemX86 = 41,
            ProgramFilesX86 = 42,
            CommonProgramFiles = 43,
            CommonProgramFilesX86 = 44,
            CommonTemplates = 45,
            CommonDocuments = 46,
            CommonAdminTools = 47,
            AdminTools = 48,
            CommonMusic = 53,
            CommonPictures = 54,
            CommonVideos = 55,
            Resources = 56,
            LocalizedResources = 57,
            CommonOemLinks = 58,
            CDBurning = 59,
        }

        #endregion

        #region SpecialFolderOption enum

        public enum SpecialFolderOption
        {
            None = 0,
            DoNotVerify = 16384,
            Create = 32768,
        }

        #endregion

        public static int TickCount { [SecuritySafeCritical, MethodImpl(MethodImplOptions.InternalCall)]
        get; }

        public static int ExitCode { [SecuritySafeCritical, MethodImpl(MethodImplOptions.InternalCall)]
        get; [SecuritySafeCritical, MethodImpl(MethodImplOptions.InternalCall)]
        set; }

        public static string CommandLine { [SecuritySafeCritical]
        get; }

        public static string CurrentDirectory { get; set; }

        public static string SystemDirectory { [SecuritySafeCritical]
        get; }

        public static string MachineName { [SecuritySafeCritical]
        get; }

        public static int ProcessorCount { [SecuritySafeCritical]
        get; }

        public static int SystemPageSize { [SecuritySafeCritical]
        get; }

        public static string NewLine { get; }
        public static Version Version { get; }

        public static long WorkingSet { [SecuritySafeCritical]
        get; }

        public static OperatingSystem OSVersion { [SecuritySafeCritical]
        get; }

        public static string StackTrace { [SecuritySafeCritical]
        get; }

        public static bool Is64BitProcess { get; }

        public static bool Is64BitOperatingSystem { [SecuritySafeCritical]
        get; }

        public static bool HasShutdownStarted { [SecuritySafeCritical, MethodImpl(MethodImplOptions.InternalCall)]
        get; }

        public static string UserName { [SecuritySafeCritical]
        get; }

        public static bool UserInteractive { [SecuritySafeCritical]
        get; }

        public static string UserDomainName { [SecuritySafeCritical]
        get; }

        [SecuritySafeCritical]
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static void Exit(int exitCode);

        [SecurityCritical]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static void FailFast(string message);

        [SecurityCritical]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static void FailFast(string message, Exception exception);

        [SecuritySafeCritical]
        public static string ExpandEnvironmentVariables(string name);

        [SecuritySafeCritical]
        public static string[] GetCommandLineArgs();

        [SecuritySafeCritical]
        public static string GetEnvironmentVariable(string variable);

        [SecuritySafeCritical]
        public static string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target);

        [SecuritySafeCritical]
        public static IDictionary GetEnvironmentVariables();

        [SecuritySafeCritical]
        public static IDictionary GetEnvironmentVariables(EnvironmentVariableTarget target);

        [SecuritySafeCritical]
        public static void SetEnvironmentVariable(string variable, string value);

        [SecuritySafeCritical]
        public static void SetEnvironmentVariable(string variable, string value, EnvironmentVariableTarget target);

        [SecuritySafeCritical]
        public static string[] GetLogicalDrives();

        [SecuritySafeCritical]
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static string GetFolderPath(Environment.SpecialFolder folder);

        [SecuritySafeCritical]
        public static string GetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option);
    }
}
