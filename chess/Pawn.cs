using board;

namespace chess {
    class Pawn : Piece {
        public Pawn (Board board, Color color) : base (board, color) { }

        public override string ToString () {
            return "P";
        }

        public override bool[, ] possibleMoves () {
            bool[, ] mat = new bool[board.lines, board.columns];
            return mat;
        }
    }
}
