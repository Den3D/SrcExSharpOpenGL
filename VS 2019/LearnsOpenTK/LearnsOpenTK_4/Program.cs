using System;

using OpenTK;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace LearnOpenTK_4
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



            private int indDisplayList = 0;
            private int vboVertex = 0;
            private int vboColor = 0;
            private int vaoId = 0;


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
                // GL.PolygonMode(MaterialFace.Front, PolygonMode.Line);
                // GL.PolygonMode(MaterialFace.Back, PolygonMode.Point);

                // indDisplayList = CreateDisplayList();
                // InitVBOs();
                vaoId = CreateVAOnoShaders();

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
            // Begin / End
            private void DrawBeginEnd()
            {
                GL.Color3(1.0f, 0.0f, 0.0f);

                GL.Begin(PrimitiveType.TriangleStrip);

                GL.Vertex2(-0.8f, 0.5f);
                GL.Vertex2(-0.8f, -0.5f);
                GL.Vertex2(-0.5f, 0.5f);

                //---
                GL.Vertex2(-0.5f, -0.5f);
                //--
                GL.Vertex2(-0.3f, 0.5f);
                GL.Vertex2(-0.3f, -0.5f);

                GL.Vertex2(0.3f, 0.5f);
                GL.Vertex2(0.3f, -0.5f);

                GL.Vertex2(0.5f, 0.5f);
                GL.Vertex2(0.5f, -0.5f);

                GL.Vertex2(0.7f, 0.5f);
                GL.Vertex2(0.7f, -0.5f);

                GL.Vertex2(0.9f, 0.5f);
                GL.Vertex2(0.9f, -0.5f);

                GL.End();
            }

            //-----------------------------------------------------------------------
            // Dislay List
            private int CreateDisplayList()
            {
                int index = GL.GenLists(1);
                GL.NewList(index, ListMode.Compile);
                GL.Color3(1.0f, 0.0f, 0.0f);
                GL.Begin(PrimitiveType.TriangleStrip);
                GL.Vertex3(-0.5f, -0.5f, 0.0f);
                GL.Vertex3( 0.5f, -0.5f, 0.0f);
                GL.Vertex3(-0.5f,  0.5f, 0.0f);
                GL.Vertex3( 0.5f,  0.5f, 0.0f);
                GL.End();

                GL.EndList();

                return index;
            }

            private void DrawDisplayList()
            {
                GL.CallList(indDisplayList);
            }

            private void DeleteDisplayList()
            {
                GL.DeleteLists(indDisplayList, 1);
            }

            //-----------------------------------------------------------------------
            // Vertex Array (VA)
            private void DrawVertexArray()
            {
                GL.EnableClientState(ArrayCap.VertexArray);
                GL.VertexPointer(3, VertexPointerType.Float, 0, vertices);

                GL.EnableClientState(ArrayCap.ColorArray);
                GL.ColorPointer(4, ColorPointerType.Float, 0, colosrs);

                GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);

                GL.DisableClientState(ArrayCap.VertexArray);
                GL.DisableClientState(ArrayCap.ColorArray);
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

            private void InitVBOs()
            {
                vboVertex = CreateVBO(vertices);
                vboColor = CreateVBO(colosrs);
            }

            private void DrawVBOs()
            {
                GL.EnableClientState(ArrayCap.VertexArray);
                GL.EnableClientState(ArrayCap.ColorArray);

                GL.BindBuffer(BufferTarget.ArrayBuffer, vboVertex);
                GL.VertexPointer(3, VertexPointerType.Float, 0, 0);

                GL.BindBuffer(BufferTarget.ArrayBuffer, vboColor);
                GL.ColorPointer(4, ColorPointerType.Float, 0, 0);
                
                GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);

                // GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                GL.DisableClientState(ArrayCap.VertexArray);
                GL.DisableClientState(ArrayCap.ColorArray);
            }

            private void DeleteVBOs()
            {
                GL.DeleteBuffer(vboVertex);
                GL.DeleteBuffer(vboColor);
            }

            //-----------------------------------------------------------------------
            // Vertex Array Object (VAO + VBO no shaders)
            private int CreateVAOnoShaders()
            {
                int vao = GL.GenVertexArray();
                GL.BindVertexArray(vao);

                int vboV = CreateVBO(vertices);
                int vboC = CreateVBO(colosrs);

                GL.EnableClientState(ArrayCap.VertexArray);
                GL.EnableClientState(ArrayCap.ColorArray);

                GL.BindBuffer(BufferTarget.ArrayBuffer, vboV);
                GL.VertexPointer(3, VertexPointerType.Float, 0, 0);

                GL.BindBuffer(BufferTarget.ArrayBuffer, vboC);
                GL.ColorPointer(4, ColorPointerType.Float, 0, 0);

                GL.BindVertexArray(0);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                GL.DisableClientState(ArrayCap.VertexArray);
                GL.DisableClientState(ArrayCap.ColorArray);

                return vao;
            }

            private void DrawVAOnoShaders()
            {
                GL.BindVertexArray(vaoId);
                GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
            }

            private void DeleteVAOnoShaders()
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

                // DrawDisplayList();
                // DrawVertexArray();
                // DrawVBOs();
                DrawVAOnoShaders();

                SwapBuffers();
                base.OnRenderFrame(args);
            }

            protected override void OnUnload()
            {
                // DeleteDisplayList();
                // DeleteVBOs();
                DeleteVAOnoShaders();
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
