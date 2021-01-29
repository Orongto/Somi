namespace Somi.OpenTK.Drawing
{
    internal struct ShaderConstants
    {
        public const string ModelUniformName = "model";
        public const string ProjectionUniformName = "projection";
        public const string TintUniformName = "tint";
        public const string MainTexUniformName = "mainTex";

        public const string WorldSpaceVertex =
@"#version 460

layout(location = 0) in vec3 position;
layout(location = 1) in vec2 texcoord;
layout(location = 2) in vec4 color;

out vec2 uv;
out vec4 vertexColor;

uniform mat4 model;
uniform mat4 projection;
uniform vec4 tint;

void main()
{
   uv = texcoord;
   vertexColor = color * tint;
   gl_Position = projection * model * vec4(position, 1.0);
}";

        public const string TexturedFragment =
@"#version 460

in vec2 uv;
in vec4 vertexColor;

out vec4 color;

uniform sampler2D mainTex;

void main()
{
    color = vertexColor * texture(mainTex, uv);
}";

    }
}
