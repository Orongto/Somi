using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using Somi.Core;
using Somi.Core.Graphics;
using System;
using System.Numerics;

namespace Somi.OpenTK.Windowing
{
    public class Window : IWindow
    {
        internal NativeWindow NativeWindow;

        public Window(string title, Vector2I size)
        {
            NativeWindow = new NativeWindow(new NativeWindowSettings
            {
                IsEventDriven = true,
                Size = new global::OpenTK.Mathematics.Vector2i(size.X, size.Y),
                Title = title,
                NumberOfSamples = 16,
            });

            NativeWindow.CenterWindow();
            NativeWindow.MakeCurrent();
            GL.ClearColor(0, 0, 0, 1);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Blend);
        }

        public Vector2I Position
        {
            get => new Vector2I
            {
                X = NativeWindow.Location.X,
                Y = NativeWindow.Location.Y
            };

            set => NativeWindow.Location = new global::OpenTK.Mathematics.Vector2i(value.X, value.Y);
        }

        public Vector2I Size
        {
            get => new Vector2I
            {
                X = NativeWindow.Size.X,
                Y = NativeWindow.Size.Y
            };

            set => NativeWindow.Size = new global::OpenTK.Mathematics.Vector2i(value.X, value.Y);
        }

        public bool Resizable
        {
            get => NativeWindow.WindowBorder == WindowBorder.Resizable;

            set => NativeWindow.WindowBorder = value ? WindowBorder.Resizable : WindowBorder.Fixed;
        }

        public string Title
        {
            get => NativeWindow.Title;
            set => NativeWindow.Title = value;
        }

        public bool IsOpen => NativeWindow.Exists;

        public void Close()
        {
            NativeWindow.Close();
        }

        public void ProcessEvents()
        {
            NativeWindow.ProcessEvents();
        }

        public void Render()
        {
            NativeWindow.Context.SwapBuffers();

            GL.Clear(ClearBufferMask.ColorBufferBit);
            var size = Size;
            GL.Viewport(0, 0, size.X, size.Y);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            NativeWindow.Dispose();
        }

        public Matrix4x4 CalculateProjection()
        {
            var size = Size;
            return Matrix4x4.CreateOrthographic(size.X, size.Y, 0.1f, 100);
        }
    }
}
