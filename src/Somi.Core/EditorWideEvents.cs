using System;

namespace Somi.Core
{

    public class PathEventArgs : EventArgs
    {
        public string Path { get; init; }
    }
    
    public static class EditorWideEvents
    {
        public static event EventHandler<PathEventArgs> OnSelectedFileChanged;
    }
}