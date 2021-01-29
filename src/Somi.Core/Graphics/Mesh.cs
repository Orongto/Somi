using System.Collections.Generic;

namespace Somi.Core.Graphics
{
    public class Mesh
    {
        private Vertex[] vertices;
        private uint[] indices;

        public Primitive PrimitiveType { get; set; } = Primitive.Triangles;

        public Mesh(Vertex[] vertices, uint[] indices)
        {
            this.vertices = vertices;
            this.indices = indices;
        }

        public Mesh(Vertex[] vertices)
        {
            Vertices = vertices;
            GenerateLinearIndices();
        }

        public Mesh()
        {
        }

        public bool NeedsUpdate { get; set; } = false;

        public Vertex[] Vertices
        {
            get => vertices;

            set
            {
                vertices = value;
                NeedsUpdate = true;
            }
        }

        public uint[] Indices
        {
            get => indices;

            set
            {
                indices = value;
                NeedsUpdate = true;
            }
        }

        public int IndexCount => Indices?.Length ?? 0;

        public int VertexCount => Vertices?.Length ?? 0;

        public void ForceUpdate()
        {
            NeedsUpdate = true;
        }

        public void GenerateLinearIndices()
        {
            indices = new uint[vertices.Length];
            for (uint i = 0; i < vertices.Length; i++)
                indices[i] = i;
            NeedsUpdate = true;
        }
    }
}
