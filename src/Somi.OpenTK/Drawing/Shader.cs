namespace Somi.OpenTK.Drawing
{
    internal class Shader
    {
        public readonly string Vertex;
        public readonly string Fragment;

        public Shader(string vertex, string fragment)
        {
            Vertex = vertex;
            Fragment = fragment;
        }

        public static Shader Default = new Shader(
            ShaderConstants.WorldSpaceVertex,
            ShaderConstants.TexturedFragment
        );
    }
}
