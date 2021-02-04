using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using Somi.Core;
using Somi.Core.Graphics;
using System;
using System.Numerics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Somi.OpenTK.Windowing
{
    public class Window : IWindow
    {
        internal NativeWindow NativeWindow;
        public Input Input { get; private set; }
        public bool IsFocused => NativeWindow.IsFocused;
        public float DeltaTime { get; set; }
        public bool IsVisible
        {
            get { return NativeWindow.IsVisible; }
            set { NativeWindow.IsVisible = value; }
        }

        public Window(string title, Vector2I size)
        {
            NativeWindow = new NativeWindow(new NativeWindowSettings
            {
                IsEventDriven = true,
                Size = new global::OpenTK.Mathematics.Vector2i(size.X, size.Y),
                Title = title,
                NumberOfSamples = 0,
                StartVisible = false,
            });
            NativeWindow.MakeCurrent();
            NativeWindow.CenterWindow();
            Input = new OpenTKInput(this);
            GL.ClearColor(0, 0, 0, 1);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Blend);

            //Vsync enablen voor minder power consumption.
           GLFW.SwapInterval(0);
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
            if (NativeWindow.IsFocused)
                Input.RefreshInput();

            NativeWindow.ProcessEvents();

            NativeWindow.IsEventDriven = !NativeWindow.IsFocused;

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
            return Matrix4x4.CreateOrthographicOffCenter(0, Application.Window.Size.X, Application.Window.Size.Y, 0, 0.1f, 100);
        }
    }
}