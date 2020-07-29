using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace LearnOpenTK_2
{

    public class Game : GameWindow
    {
        public Game(int w, int h, string title) : base( w, h,
                                                        GraphicsMode.Default,
                                                        title,
                                                        GameWindowFlags.FixedWindow,
                                                        DisplayDevice.Default, 
                                                        4, 0,
                                                        GraphicsContextFlags.ForwardCompatible)
        {
            Console.WriteLine(GL.GetString(StringName.Version));
            Console.WriteLine(GL.GetString(StringName.Vendor));
            Console.WriteLine(GL.GetString(StringName.Renderer));
            Console.WriteLine(GL.GetString(StringName.ShadingLanguageVersion));
        }

        protected override void OnLoad(EventArgs e)
        { }

        protected override void OnResize(EventArgs e)
        { }

        protected override void OnUpdateFrame(FrameEventArgs e)
        { }

        protected override void OnRenderFrame(FrameEventArgs e)
        { }

        protected override void OnUnload(EventArgs e)
        { }


    }

    class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game(400, 300, "LearnOpenTK TheMrDen3D"))
            {
                game.Run();
            }

        }
    }
}
