using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese tamaño del tablero.");
            int boardsize = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Numero de jugadores");
            int players = Convert.ToInt32(Console.ReadLine());
            Game g = new Game(boardsize, players);
            g.Play();
            Console.ReadKey();
        }
    }
}
