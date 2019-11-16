using System;

using board;

using chess;

namespace chess_console {
    class Screen {
        public static void printBoard (Board board) {

            for (int i = 0; i < board.lines; i++) {
                Console.Write (8 - i + " ");
                for (int j = 0; j < board.columns; j++) {

                    printPiece (board.piece (i, j));
                }
                Console.WriteLine ();
            }
            Console.WriteLine ("  a b c d e f g h");
        }

        public static void printBoard (Board board, bool[, ] possiblePositions) {

            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            ConsoleColor changedBackgroundColor = ConsoleColor.DarkGray;

            for (int i = 0; i < board.lines; i++) {
                Console.Write (8 - i + " ");
                for (int j = 0; j < board.columns; j++) {
                    if (possiblePositions[i, j]) {
                        Console.BackgroundColor = changedBackgroundColor;
                    } else {
                        Console.BackgroundColor = originalBackgroundColor;
                    }
                    printPiece (board.piece (i, j));
                    Console.BackgroundColor = originalBackgroundColor;
                }

                Console.WriteLine ();
            }
            Console.WriteLine ("  a b c d e f g h");
        }

        public static void printPiece (Piece p) {

            if (p == null) {
                Console.Write ("- ");
            } else {
                if (p.color == Color.White) {
                    Console.Write (p);
                } else {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write (p);
                    Console.ForegroundColor = aux;
                }
                Console.Write (" ");
            }
        }

        public static ChessPosition catchChessPosition () {
            string s = Console.ReadLine ();
            char column = s[0];
            int line = int.Parse (s[1] + "");
            return new ChessPosition (column, line);
        }
    }
}
