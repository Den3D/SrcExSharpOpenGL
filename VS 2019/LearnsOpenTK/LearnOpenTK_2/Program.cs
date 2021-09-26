using System;

using OpenTK;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace LearnOpenTK_2
{
    class Program
    {
        public class Game : GameWindow
        {
            private float factor = 0.0f;
            private float sinfactor = 0.0f;

            private float frameTime = 0.0f;
            private int fps = 0;

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

                //GL.ClearColor(Color4.LightBlue);
               // GL.ClearColor(173 / 255.0f, 216 / 255.0f, 230 / 255.0f, 255 / 255.0f);
            }

            protected override void OnResize(ResizeEventArgs e)
            {
                base.OnResize(e);
            }

            protected override void OnUpdateFrame(FrameEventArgs args)
            {
                // Title = $"LearnOpenTK FPS - {1 / args.Time}";

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
    


                factor += 0.001f;
                sinfactor = (float)Math.Sin((double)factor);

                base.OnUpdateFrame(args);
            }

            protected override void OnRenderFrame(FrameEventArgs args)
            {
                GL.ClearColor(sinfactor, sinfactor * 2, (float)Math.Cos((double)sinfactor), 1.0f);
                GL.Clear(ClearBufferMask.ColorBufferBit);


                SwapBuffers();
                base.OnRenderFrame(args);
            }

            protected override void OnUnload()
            {
                base.OnUnload();
            }

        }


        static void Main(string[] args)
        {
            var nativeWinSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(800, 600),
                Location = new Vector2i(370, 300),
                WindowBorder = WindowBorder.Resizable,
                WindowState = WindowState.Normal,
                Title = "LearnOpenTK - Creating a Window",

                Flags = ContextFlags.ForwardCompatible,
                APIVersion = new Version(3, 3),
                Profile = ContextProfile.Core,
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
