/*
 * Created by SharpDevelop.
 * User: user
 * Date: 16.07.2020
 * Time: 9:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace LernOpenTK_1
{
	public class Game: GameWindow
    {
        public Game() : base() { }
    }
	
	class Program
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