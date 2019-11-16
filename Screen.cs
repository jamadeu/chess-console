using System;
using System.Collections.Generic;
using board;

using chess;

namespace chess_console {
    class Screen {

        public static void printCapturedPieces(ChessMatch match ) {
            Console.WriteLine( "Captured pieces:" );
            Console.Write( "Whites: " );
            printHashSet( match.piecesCaptured( Color.White ) );
            Console.WriteLine();

            Console.Write( "Pretas: " );
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            printHashSet( match.piecesCaptured( Color.Black ) );
            Console.ForegroundColor = aux;

            Console.WriteLine();
        }

        public static void printMatch(ChessMatch match ) {
            printBoard( match.Board );
            Console.WriteLine();
            printCapturedPieces( match );
            Console.WriteLine();
            Console.WriteLine( "Shift: " + match.Shift);

            if ( match.InCheck ) {
                Console.WriteLine( "Waiting player " + match.CurrentPlayer );
                if ( match.InCheck ) {
                    Console.WriteLine( "Check!" );
                }
            } else {
                Console.WriteLine( "CHECKMATE!" );
                Console.WriteLine( "Winner player " + match.CurrentPlayer );
            }
        }

        public static void printHashSet(HashSet<Piece> hash ) {
            Console.Write( "[" );
            foreach(Piece p in hash ) {
                Console.Write( p + " " );
            }
            Console.Write( "]" );
        }
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
