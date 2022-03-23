using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Playground
{
    public class Shader
    {
        public readonly int Handle;

        public Shader(string vertexPath, string fragmentPath)
        {
            var shaderSource = File.ReadAllText(vertexPath);
            var vertexShader = CreateShader(ShaderType.VertexShader, shaderSource);

            shaderSource = File.ReadAllText(fragmentPath);
            var fragmentShader = CreateShader(ShaderType.FragmentShader, shaderSource);

            Handle = GL.CreateProgram();
            
            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);

            GL.LinkProgram(Handle);

            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);
        }
        public void Use()
        {
            GL.UseProgram(Handle);
            
        }

        int CreateShader(ShaderType shaderType, string shaderSource)
        {
            int shader = GL.CreateShader(shaderType);
            GL.ShaderSource(shader, shaderSource);
            GL.CompileShader(shader);

            string info = GL.GetShaderInfoLog(shader);
            if (info.Length > 0)
                Console.WriteLine(info + " " + shaderType);
            else
                Console.WriteLine($"{shaderType} ID: {shader}");

            return shader;
        }

        public void ChangeColor(string uniformString, float r, float g, float b)
        {
            int vertexColorLocation = GL.GetUniformLocation(Handle, uniformString);
            GL.Uniform4(vertexColorLocation, r, g, b, 1);
        }
    }
}
