using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace LernOpenTK_1
{
    public class Game : GameWindow
	{
		public Game() : base() { }
	}


	class MainClass
	{
		public static void Main(string[] args)
		{
			using (Game game = new Game())
			{
				game.Run();
			}
		}
	}
}
