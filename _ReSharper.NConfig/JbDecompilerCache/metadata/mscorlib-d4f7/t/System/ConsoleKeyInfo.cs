// Type: System.ConsoleKeyInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime;

namespace System
{
    [Serializable]
    public struct ConsoleKeyInfo
    {
        public ConsoleKeyInfo(char keyChar, ConsoleKey key, bool shift, bool alt, bool control);

        public char KeyChar { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public ConsoleKey Key { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public ConsoleModifiers Modifiers { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public static bool operator ==(ConsoleKeyInfo a, ConsoleKeyInfo b);
        public static bool operator !=(ConsoleKeyInfo a, ConsoleKeyInfo b);
        public override bool Equals(object value);
        public bool Equals(ConsoleKeyInfo obj);
        public override int GetHashCode();
    }
}
