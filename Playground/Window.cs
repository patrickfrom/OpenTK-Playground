using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Playground
{
    public class Window : GameWindow
    {
        private readonly float[] _vertices =
        {
            -0.2f, -0.2f, 0.0f,
            0.2f, -0.2f, 0.0f,
            0.0f, 0.2f, 0.0f,
        };

        private int _vertexBufferObject;
        private int _vertexArrayObject;

        private Shader _shader;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {}

        protected override void OnLoad()
        {
            base.OnLoad();
            Console.WriteLine("OpenGL Version: " + APIVersion);

            GL.ClearColor(new Color4(_vertices[0] + 1f, _vertices[3], _vertices[7], 0));

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StreamDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // Ah yes, Art. (If anyone sees this, please show me a better way)
            _shader = new Shader("../../../Shaders/shader.vert", "../../../Shaders/shader.frag");
            _shader.Use();
        }

        float _time;
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Lines, 0, 3);

            _shader.Use();

            GL.ClearColor(new Color4(43, 43, 23, 0));

            SwapBuffers();
        }

        bool _isOn;
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            var input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            if (input.IsKeyDown(Keys.E))
            {
                if(_isOn)
                {
                    _vertices[0] -= 0.0001f;
                    _vertices[3] += 0.0001f;
                    _vertices[7] += 0.0001f;
                
                    GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)0, _vertices.Length * sizeof(float), _vertices);
                }
            }

            if(input.IsKeyPressed(Keys.B)) {
                if(!_isOn)
                {
                    _isOn = true;
                    _shader.ChangeColor("ourColor", 0.0f, 1.0f, 0.4f);
                } else
                {
                    _isOn = false;
                    _shader.ChangeColor("ourColor", 0.0f, 0.0f, 0.0f);
                }
            }
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
        }
    }
}
