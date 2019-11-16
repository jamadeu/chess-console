using board;

namespace chess {
    class ChessMatch {
        public Board board { get; private set; }
        public int shift { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool finished { get; private set; }

        public ChessMatch () {
            board = new Board (8, 8);
            shift = 1;
            currentPlayer = Color.White;
            finished = false;
            putPieces ();
        }

        private void putPieces () {
            board.putPiece (new Rook (board, Color.White), new ChessPosition ('a', 1).toPosition ());

            board.putPiece (new Rook (board, Color.Black), new ChessPosition ('a', 8).toPosition ());
        }

        public void executeMovement (Position origin, Position destiny) {
            Piece p = board.removePiece (origin);
            p.incrementMovementsCount ();
            Piece pieceCaptured = board.removePiece (destiny);
            board.putPiece (p, destiny);
        }

        public void executePlay (Position origin, Position destiny) {
            executeMovement (origin, destiny);
            shift++;
            changePlayer ();

        }

        private void changePlayer () {
            if (currentPlayer == Color.White) {
                currentPlayer = Color.Black;
            } else {
                currentPlayer = Color.White;
            }
        }
    }
}
