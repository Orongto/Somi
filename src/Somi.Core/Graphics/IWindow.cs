﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Somi.Core.Graphics
{
    public interface IWindow : IDisposable
    {
        public Vector2I Position { get; set; }
        public Vector2I Size { get; set; }
        public bool IsOpen { get; }
        public bool IsVisible { get; set; }
        public bool Resizable { get; set; }
        public string Title { get; set; }
        public Input Input { get; }
        bool IsFocused { get; }
        public void ProcessEvents();
        public void Render();
        public float DeltaTime { get; set; }
        public void Close();

        public Matrix4x4 CalculateProjection();
    }
}
