using System;

using OpenTK;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;



namespace LearnOpenTK_1
{
    class Program
    {
        public class Game: GameWindow
        {
            public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
                : base(gameWindowSettings, nativeWindowSettings)
            {
                Console.WriteLine(GL.GetString(StringName.Version));
                Console.WriteLine(GL.GetString(StringName.Vendor));
                Console.WriteLine(GL.GetString(StringName.Renderer));
                Console.WriteLine(GL.GetString(StringName.ShadingLanguageVersion));
            }

            protected override void OnLoad()
            {
                base.OnLoad();
            }

            protected override void OnResize(ResizeEventArgs e)
            {
                base.OnResize(e);
            }

            protected override void OnUpdateFrame(FrameEventArgs args)
            {
                base.OnUpdateFrame(args);
            }

            protected override void OnRenderFrame(FrameEventArgs args)
            {
                GL.ClearColor(Color4.LightBlue);
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
