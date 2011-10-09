// Type: System.Console
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System
{
    public static class Console
    {
        public static TextWriter Error { [SecuritySafeCritical, HostProtection(SecurityAction.LinkDemand, UI = true)]
        get; }

        public static TextReader In { [SecuritySafeCritical, HostProtection(SecurityAction.LinkDemand, UI = true)]
        get; }

        public static TextWriter Out { [SecuritySafeCritical, HostProtection(SecurityAction.LinkDemand, UI = true)]
        get; }

        public static Encoding InputEncoding { [SecuritySafeCritical]
        get; [SecuritySafeCritical]
        set; }

        public static Encoding OutputEncoding { [SecuritySafeCritical]
        get; [SecuritySafeCritical]
        set; }

        public static ConsoleColor BackgroundColor { [SecuritySafeCritical]
        get; [SecuritySafeCritical]
        set; }

        public static ConsoleColor ForegroundColor { [SecuritySafeCritical]
        get; [SecuritySafeCritical]
        set; }

        public static int BufferHeight { [SecuritySafeCritical]
        get; [SecuritySafeCritical]
        set; }

        public static int BufferWidth { [SecuritySafeCritical]
        get; [SecuritySafeCritical]
        set; }

        public static int WindowHeight { [SecuritySafeCritical]
        get; [SecuritySafeCritical]
        set; }

        public static int WindowWidth { [SecuritySafeCritical]
        get; [SecuritySafeCritical]
        set; }

        public static int LargestWindowWidth { [SecuritySafeCritical]
        get; }

        public static int LargestWindowHeight { [SecuritySafeCritical]
        get; }

        public static int WindowLeft { [SecuritySafeCritical]
        get; [SecuritySafeCritical]
        set; }

        public static int WindowTop { [SecuritySafeCritical]
        get; [SecuritySafeCritical]
        set; }

        public static int CursorLeft { [SecuritySafeCritical]
        get; [SecuritySafeCritical]
        set; }

        public static int CursorTop { [SecuritySafeCritical]
        get; [SecuritySafeCritical]
        set; }

        public static int CursorSize { [SecuritySafeCritical]
        get; [SecuritySafeCritical]
        set; }

        public static bool CursorVisible { [SecuritySafeCritical]
        get; [SecuritySafeCritical]
        set; }

        public static string Title { [SecuritySafeCritical]
        get; [SecuritySafeCritical]
        set; }

        public static bool KeyAvailable { [SecuritySafeCritical, HostProtection(SecurityAction.LinkDemand, UI = true)]
        get; }

        public static bool NumberLock { [SecuritySafeCritical]
        get; }

        public static bool CapsLock { [SecuritySafeCritical]
        get; }

        public static bool TreatControlCAsInput { [SecuritySafeCritical]
        get; [SecuritySafeCritical]
        set; }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Beep();

        [SecuritySafeCritical]
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Beep(int frequency, int duration);

        [SecuritySafeCritical]
        public static void Clear();

        [SecuritySafeCritical]
        public static void ResetColor();

        [SecuritySafeCritical]
        public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop);

        [SecuritySafeCritical]
        public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor);

        [SecuritySafeCritical]
        public static void SetBufferSize(int width, int height);

        [SecuritySafeCritical]
        public static void SetWindowSize(int width, int height);

        [SecuritySafeCritical]
        public static void SetWindowPosition(int left, int top);

        [SecuritySafeCritical]
        public static void SetCursorPosition(int left, int top);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static ConsoleKeyInfo ReadKey();

        [SecuritySafeCritical]
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static ConsoleKeyInfo ReadKey(bool intercept);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static Stream OpenStandardError();

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static Stream OpenStandardError(int bufferSize);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static Stream OpenStandardInput();

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static Stream OpenStandardInput(int bufferSize);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static Stream OpenStandardOutput();

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static Stream OpenStandardOutput(int bufferSize);

        [SecuritySafeCritical]
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void SetIn(TextReader newIn);

        [SecuritySafeCritical]
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void SetOut(TextWriter newOut);

        [SecuritySafeCritical]
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void SetError(TextWriter newError);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static int Read();

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static string ReadLine();

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine();

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(bool value);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(char value);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(char[] buffer);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(char[] buffer, int index, int count);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(decimal value);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(double value);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(float value);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(int value);

        [CLSCompliant(false)]
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(uint value);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(long value);

        [CLSCompliant(false)]
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(ulong value);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(object value);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(string value);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(string format, object arg0);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(string format, object arg0, object arg1);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(string format, object arg0, object arg1, object arg2);

        [CLSCompliant(false)]
        [SecuritySafeCritical]
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(string format, object arg0, object arg1, object arg2, object arg3);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(string format, params object[] arg);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(string format, object arg0);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(string format, object arg0, object arg1);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(string format, object arg0, object arg1, object arg2);

        [SecuritySafeCritical]
        [CLSCompliant(false)]
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(string format, object arg0, object arg1, object arg2, object arg3);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(string format, params object[] arg);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(bool value);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(char value);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(char[] buffer);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(char[] buffer, int index, int count);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(double value);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(decimal value);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(float value);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(int value);

        [CLSCompliant(false)]
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(uint value);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(long value);

        [CLSCompliant(false)]
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(ulong value);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(object value);

        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(string value);

        public static event ConsoleCancelEventHandler CancelKeyPress;
    }
}
