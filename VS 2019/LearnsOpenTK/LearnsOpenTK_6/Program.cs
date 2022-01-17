using System;

using OpenTK;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace LearnsOpenTK_6
{
    class Program
    {
        public class Game : GameWindow
        {
            private float frameTime = 0.0f;
            private int fps = 0;

            float[] vertices = new float[] {
                    -0.5f, -0.5f, 0.0f,
                     0.5f, -0.5f, 0.0f,
                    -0.5f,  0.5f, 0.0f,
                     0.5f,  0.5f, 0.0f
            };

            float[] colosrs = new float[] {
                    1.0f, 0.0f, 0.0f, 1.0f,
                    0.0f, 1.0f, 0.0f, 1.0f,
                    0.0f, 0.0f, 1.0f, 1.0f,
                    0.8f, 0.6f, 0.2f, 1.0f
            };

            uint[] indexes = new uint[]
            {
                0, 1, 2,
                0, 2, 3,
                3, 2, 4,
                3, 4, 5,
                5, 4, 6,
                5, 6, 7,

                1, 8, 9,
                1, 9, 2,
                2, 9, 10,
                2, 10, 4,
                4, 10, 11,
                4, 11, 6

            };

            float[] vert_colors = new float[]
            {
                // vertices           // colosrs 
                -0.8f,  0.6f, 0.0f,   1.0f, 0.0f, 0.0f, 1.0f,
                -0.8f,  0.0f, 0.0f,   0.0f, 1.0f, 0.0f, 1.0f,
                -0.2f,  0.0f, 0.0f,   0.0f, 0.0f, 1.0f, 1.0f,
                -0.2f,  0.6f, 0.0f,   0.8f, 0.6f, 0.2f, 1.0f,
                 0.2f,  0.0f, 0.0f,   0.8f, 0.6f, 0.2f, 1.0f,
                 0.2f,  0.6f, 0.0f,   0.8f, 0.6f, 0.2f, 1.0f,
                 0.8f,  0.0f, 0.0f,   0.8f, 0.6f, 0.2f, 1.0f,
                 0.8f,  0.6f, 0.0f,   0.8f, 0.6f, 0.2f, 1.0f,
                
                -0.8f,  -0.6f, 0.0f,   0.8f, 0.6f, 0.2f, 1.0f,
                -0.2f,  -0.6f, 0.0f,   0.8f, 0.6f, 0.2f, 1.0f,
                 0.2f,  -0.6f, 0.0f,   0.8f, 0.6f, 0.2f, 1.0f,
                 0.8f,  -0.6f, 0.0f,   0.8f, 0.6f, 0.2f, 1.0f
            };



            private int indDisplayList = 0;
            private int vboVertex = 0;
            private int vboColor = 0;
            private int vaoId = 0;

            private ShaderProgram shaderProgram;


            public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
                : base(gameWindowSettings, nativeWindowSettings)
            {
                Console.WriteLine(GL.GetString(StringName.Version));
                Console.WriteLine(GL.GetString(StringName.Vendor));
                Console.WriteLine(GL.GetString(StringName.Renderer));
                Console.WriteLine(GL.GetString(StringName.ShadingLanguageVersion));

                VSync = VSyncMode.On;
                CursorVisible = true;
            }

            protected override void OnLoad()
            {
                base.OnLoad();

                GL.ClearColor(173 / 255.0f, 216 / 255.0f, 230 / 255.0f, 255 / 255.0f);
                GL.Enable(EnableCap.CullFace);
                GL.CullFace(CullFaceMode.Back);
                GL.PolygonMode(MaterialFace.Front, PolygonMode.Line);
                // GL.PolygonMode(MaterialFace.Back, PolygonMode.Point);

                shaderProgram = new ShaderProgram(@"data\shaders\shader_base.vert", @"data\shaders\shader_base.frag");

                vaoId = CreateVAOShaders();

            }

            protected override void OnResize(ResizeEventArgs e)
            {
                base.OnResize(e);
            }

            protected override void OnUpdateFrame(FrameEventArgs args)
            {
                frameTime += (float)args.Time;
                fps++;
                if (frameTime >= 1.0f)
                {
                    Title = $"LearnOpenTK FPS - {fps}";
                    frameTime = 0.0f;
                    fps = 0;
                }

                var key = KeyboardState;

                if (key.IsKeyDown(Keys.Escape))
                {
                    Console.WriteLine(Keys.Escape.ToString());
                    Close();
                }

                base.OnUpdateFrame(args);
            }

            //-----------------------------------------------------------------------
            // Vertex Buffer Object (VBO)
            private int CreateVBO(float[] data)
            {
                int vbo = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
                GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                return vbo;
            }

            private int CreateEBO(uint[] data)
            {
                int ebo = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
                GL.BufferData(BufferTarget.ElementArrayBuffer, data.Length * sizeof(uint), data, BufferUsageHint.StaticDraw);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
                return ebo;
            }


            //-----------------------------------------------------------------------
            // Vertex Array Object (VAO + VBO shaders)
            private int CreateVAOShaders()
            {
                int vao = GL.GenVertexArray();
                GL.BindVertexArray(vao);

                int vboVC = CreateVBO(vert_colors);
                int ebo = CreateEBO(indexes);

                int VertexArray = shaderProgram.GetAttribProgram("aPosition");
                int ColorArray = shaderProgram.GetAttribProgram("aColor");

                GL.EnableVertexAttribArray(VertexArray);
                GL.EnableVertexAttribArray(ColorArray);

                GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
                GL.BindBuffer(BufferTarget.ArrayBuffer, vboVC);
                GL.VertexAttribPointer(VertexArray, 3, VertexAttribPointerType.Float, false, 7 * sizeof(float), 0);
                GL.VertexAttribPointer(ColorArray, 4, VertexAttribPointerType.Float, false, 7 * sizeof(float), 3 * sizeof(float));

                GL.BindVertexArray(0);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

                GL.DisableVertexAttribArray(VertexArray);
                GL.DisableVertexAttribArray(ColorArray);

                return vao;
            }

            private void DrawVAOShaders()
            {
                shaderProgram.ActiveProgram();
                GL.BindVertexArray(vaoId);
                // GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
                GL.DrawElements(PrimitiveType.Triangles, indexes.Length, DrawElementsType.UnsignedInt, 0);
                shaderProgram.DeactiveProgram();
            }

            private void DeleteVAOShaders()
            {
                GL.BindVertexArray(0);
                GL.DeleteVertexArray(vaoId);
                GL.DeleteBuffer(vboVertex);
                GL.DeleteBuffer(vboColor);
            }

            //-----------------------------------------------------------------------


            protected override void OnRenderFrame(FrameEventArgs args)
            {
                GL.Clear(ClearBufferMask.ColorBufferBit);

                DrawVAOShaders();

                SwapBuffers();
                base.OnRenderFrame(args);
            }

            protected override void OnUnload()
            {
                DeleteVAOShaders();

                shaderProgram.DeleteProgram();


                base.OnUnload();
            }

        }


        static void Main(string[] args)
        {
            var nativeWinSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(600, 450),
                Location = new Vector2i(370, 300),
                WindowBorder = WindowBorder.Resizable,
                WindowState = WindowState.Normal,
                Title = "LearnOpenTK - Creating a Window",

                // Flags = ContextFlags.ForwardCompatible,
                Flags = ContextFlags.Default,
                APIVersion = new Version(3, 3),
                // Profile = ContextProfile.Core,
                Profile = ContextProfile.Compatability,
                API = ContextAPI.OpenGL,

                IsFullscreen = true,
                NumberOfSamples = 0
            };


            using (Game game = new Game(GameWindowSettings.Default, nativeWinSettings))
            {
                game.Run();
            }
        }
    }
}
