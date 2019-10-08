using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadder
{
    class Game
    {
        Player currentPlayer;
        Cell[] board;
        Player[] playerQueue;
        int totalPlayers;
        int totalPositionExced;
        public Game(int BoardSize, int NumberOfPlayers)
        {
            totalPlayers = NumberOfPlayers;
            board = CreateBoard(BoardSize);
            playerQueue = AssignPlayers(totalPlayers);
        }
        private Cell[] CreateBoard(int boardSize)
        {
            Cell[] board = new Cell[boardSize];
            for (int i = 0; i < boardSize; i++)
            {
                Cell c = new Cell();
                c.CellNumber = i + 1;
                board[i] = c;
            }
            bool isSnakeCellLeft = true;
            while (isSnakeCellLeft)
            {
                Console.WriteLine("Quieres definir el obstaculo de la serpiente? y/n");
                if (Console.ReadLine().ToLower() == "y")
                {
                    Console.WriteLine("Numero de posicion para obstaculo de la serpiente");
                    int snakeCellNumber = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Numero de posicion de penalizacion");
                    int penaltyCellNumber = Convert.ToInt32(Console.ReadLine());
                    SnakeCell s = new SnakeCell();
                    s.CellNumber = snakeCellNumber;
                    s.PenaltyCell = penaltyCellNumber;
                    board[snakeCellNumber - 1] = s;
                }
                else
                {
                    isSnakeCellLeft = false;
                }
            }

            bool isLadderCellLeft = true;
            while (isLadderCellLeft)
            {
                Console.WriteLine("Quieres definir una ayuda de escalera? y/n");
                if (Console.ReadLine().ToLower() == "y")
                {
                    Console.WriteLine("Numero de posicion para la escalera");
                    int ladderCellNumber = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Numero de posicion de ventaja");
                    int advantageCellNumber = Convert.ToInt32(Console.ReadLine());
                    LadderCell l = new LadderCell();
                    l.CellNumber = ladderCellNumber;
                    l.AdvantageCell = advantageCellNumber;
                    board[ladderCellNumber - 1] = l;
                }
                else
                {
                    isLadderCellLeft = false;
                }
            }
            return board;
        }
        private Player[] AssignPlayers(int numberOfPlayers)
        {
            Player[] players = new Player[numberOfPlayers];
            for (int i = 0; i < numberOfPlayers; i++)
            {
                players[i] = new Player();
                players[i].CurrentCellPosition = 0;
                players[i].PlayerNumber = i + 1;
                Console.WriteLine("Ingrese su nombre");
                players[i].PlayerName = Console.ReadLine();
            }
            return players;
        }
        private int RollDice(Random rnd)
        {
            return rnd.Next(1, 6);
        }
        private void NextChance()
        {
            if (currentPlayer.PlayerNumber < totalPlayers)
            {
                currentPlayer = playerQueue[(currentPlayer.PlayerNumber - 1) + 1];
            }
            else
            {
                currentPlayer = playerQueue[0];
            }
        }
        private void CalculatePlayerPosition(int diceNumber)
        {
            Console.WriteLine(currentPlayer.PlayerName + ", su lanzamiento muestra " + diceNumber);
            int moveLocation = currentPlayer.CurrentCellPosition;
            if ((moveLocation + diceNumber) <= board.Length)
            {
                moveLocation = moveLocation + diceNumber;
                Console.WriteLine(currentPlayer.PlayerName + ", avanza a la posicion " + moveLocation);
            }
            else
            {
                totalPositionExced = (board.Length- currentPlayer.CurrentCellPosition);
                moveLocation = board.Length-   (diceNumber-totalPositionExced);
                Console.WriteLine(currentPlayer.PlayerName + ", se ubica en la posicion " + moveLocation);
            }

            while (board[moveLocation - 1].GetType() == typeof(SnakeCell) || board[moveLocation - 1].GetType() == typeof(LadderCell))
            {
                if (board[moveLocation - 1].GetType() == typeof(SnakeCell))
                {
                    moveLocation = (board[moveLocation - 1] as SnakeCell).PenaltyCell;
                    Console.WriteLine(currentPlayer.PlayerName + "!!! Posicion del obstaculo de la serpiente :( , baja a  la posicion " + moveLocation);
                }
                if (board[moveLocation - 1].GetType() == typeof(LadderCell))
                {
                    moveLocation = (board[moveLocation - 1] as LadderCell).AdvantageCell;
                    Console.WriteLine(currentPlayer.PlayerName + " Encontraste una escalera :D , sube a la posicion " + moveLocation);
                }
            }
            currentPlayer.CurrentCellPosition = moveLocation;
        }
        public void Play()
        {
            currentPlayer = playerQueue[0];
            bool isFirstMove = true;
            Random rnd = new Random();
            while (currentPlayer.CurrentCellPosition != board.Length)
            {
                if (!isFirstMove)
                {
                    NextChance();
                }
                isFirstMove = false;
                CalculatePlayerPosition(RollDice(rnd));
            }
            Console.WriteLine(currentPlayer.PlayerName + " gana");
            foreach (Player p in playerQueue)
            {
                Console.WriteLine(p.PlayerName + " llego a " + p.CurrentCellPosition);
            }
            Console.WriteLine("Fin del juego!!!");
        }
    }
}
