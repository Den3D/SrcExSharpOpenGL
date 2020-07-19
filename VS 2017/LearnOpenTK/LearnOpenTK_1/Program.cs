using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace LearnOpenTK_1
{
    public class Game: GameWindow
    {
        public Game() : base() { }
    }


    class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game())
            {
                game.Run();
            }

        }
    }
}
