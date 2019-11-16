using board;

namespace chess {
    class Knight : Piece {
        public Knight (Board board, Color color) : base (board, color) { }

        public override string ToString () {
            return "N";
        }

        public override bool[, ] possibleMoves () {
            bool[, ] mat = new bool[board.lines, board.columns];
            return mat;
        }
    }
}
