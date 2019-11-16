using board;

namespace chess {
    class King : Piece {
        public King (Board board, Color color) : base (board, color) { }

        public override string ToString () {
            return "K";
        }

        private bool canMove (Position pos) {
            Piece p = board.piece (pos);
            return p == null || p.color != this.color;
        }
        public override bool[, ] possibleMoves () {
            bool[, ] mat = new bool[board.lines, board.columns];

            Position pos = new Position (0, 0);

            pos.setValues (this.position.line - 1, this.position.column);
            if (board.positionIsValid (pos) && canMove (pos)) {
                mat[pos.line, pos.column] = true;
            }

            pos.setValues (this.position.line - 1, this.position.column + 1);
            if (board.positionIsValid (pos) && canMove (pos)) {
                mat[pos.line, pos.column] = true;
            }

            pos.setValues (this.position.line, this.position.column + 1);
            if (board.positionIsValid (pos) && canMove (pos)) {
                mat[pos.line, pos.column] = true;
            }

            pos.setValues (this.position.line + 1, this.position.column + 1);
            if (board.positionIsValid (pos) && canMove (pos)) {
                mat[pos.line, pos.column] = true;
            }

            pos.setValues (this.position.line + 1, this.position.column);
            if (board.positionIsValid (pos) && canMove (pos)) {
                mat[pos.line, pos.column] = true;
            }

            pos.setValues (this.position.line + 1, this.position.column - 1);
            if (board.positionIsValid (pos) && canMove (pos)) {
                mat[pos.line, pos.column] = true;
            }

            pos.setValues (this.position.line, this.position.column - 1);
            if (board.positionIsValid (pos) && canMove (pos)) {
                mat[pos.line, pos.column] = true;
            }

            pos.setValues (this.position.line - 1, this.position.column - 1);
            if (board.positionIsValid (pos) && canMove (pos)) {
                mat[pos.line, pos.column] = true;
            }

            return mat;
        }
    }
}
