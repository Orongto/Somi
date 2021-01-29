using OpenTK.Graphics.OpenGL;
using Somi.Core.Graphics;
using System;

namespace Somi.OpenTK.Drawing
{
    internal class MeshCache : Cache<Mesh, MeshHandles>
    {
        private const int sizeofColor = sizeof(float) * 4;
        private const int sizeofVector3 = sizeof(float) * 3;
        private const int sizeofVector2 = sizeof(float) * 2;
        private const int sizeofVertex = sizeofVector3 + sizeofVector2 + sizeofColor;

        protected override MeshHandles CreateNew(Mesh mesh)
        {
            var handles = CreateObjects();

            GL.BindBuffer(BufferTarget.ArrayBuffer, handles.VBO);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeofVertex, 0); //position
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, sizeofVertex, sizeofVector3); //texcoords
            GL.VertexAttribPointer(2, 4, VertexAttribPointerType.Float, false, sizeofVertex, sizeofVector3 + sizeofVector2); //color

            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.EnableVertexAttribArray(2);

            UploadData(mesh, handles);

            return handles;
        }

        private static MeshHandles CreateObjects()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

            int ibo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo);

            return new MeshHandles
            {
                VAO = vao,
                VBO = vbo,
                IBO = ibo,
            };
        }

        public static void UploadData(Mesh mesh, MeshHandles handles)
        {
            //upload vertices
            GL.BindBuffer(BufferTarget.ArrayBuffer, handles.VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(mesh.Vertices.Length * sizeofVertex), mesh.Vertices, BufferUsageHint.StaticDraw);

            //upload indices
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, handles.IBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(mesh.IndexCount * sizeof(uint)), mesh.Indices, BufferUsageHint.StaticDraw);

            mesh.NeedsUpdate = false;
        }

        protected override void DisposeOf(MeshHandles loaded)
        {
            GL.DeleteBuffer(loaded.IBO);
            GL.DeleteBuffer(loaded.VBO);
            GL.DeleteVertexArray(loaded.VAO);
        }
    }
}
