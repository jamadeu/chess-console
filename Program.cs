﻿using System;

using board;

using chess;

namespace chess_console {
    class Program {
        static void Main (string[] args) {

            try {
                ChessMatch game = new ChessMatch ();

                while (!game.finished) {
                    Console.Clear ();
                    Screen.printBoard (game.board);
                    Console.WriteLine ();
                    Console.WriteLine ("Shift: " + game.shift);
                    Console.WriteLine ("Waiting player " + game.currentPlayer);

                    Console.WriteLine ();
                    Console.Write ("Origin: ");
                    Position origin = Screen.catchChessPosition ().toPosition ();

                    bool[, ] possiblePositions = game.board.piece (origin).possibleMoves ();

                    Console.Clear ();
                    Screen.printBoard (game.board, possiblePositions);

                    Console.WriteLine ();
                    Console.Write ("Destiny: ");
                    Position destiny = Screen.catchChessPosition ().toPosition ();

                    game.executePlay (origin, destiny);
                }

            } catch (BoardException e) {
                Console.WriteLine (e);
            }

            Console.ReadLine ();
        }
    }
}