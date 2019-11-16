using System;

using board;

using chess;

namespace chess_console {
    class Program {
        static void Main (string[] args) {

            try {
                ChessMatch game = new ChessMatch ();

                while (!game.finished) {

                    try {
                        Console.Clear();
                        Screen.printMatch( game );

                        Console.WriteLine();
                        Console.Write( "Origin: " );
                        Position origin = Screen.catchChessPosition().toPosition();

                        game.checkOriginPosition( origin );
                        bool[,] possiblePositions = game.board.piece( origin ).possibleMoves();

                        Console.Clear();
                        Screen.printBoard( game.board , possiblePositions );

                        Console.WriteLine();
                        Console.Write( "Destiny: " );
                        Position destiny = Screen.catchChessPosition().toPosition();
                        game.checkDestinyPosition( origin , destiny );

                        game.executePlay( origin , destiny );
                    }catch(BoardException e ) {
                        Console.WriteLine( e.Message );
                        Console.ReadLine();
                    }
                }

            } catch (BoardException e) {
                Console.WriteLine (e);
            }

            Console.ReadLine ();
        }
    }
}
