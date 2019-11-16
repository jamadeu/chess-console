using board;

namespace chess {
    class Rook : Piece {
        public Rook (Board board, Color color) : base (board, color) { }

        public override string ToString () {
            return "R";
        }

        private bool canMove (Position pos) {
            Piece p = board.piece (pos);
            return p == null || p.color != this.color;
        }

        public override bool[, ] possibleMoves () {
            bool[, ] mat = new bool[board.lines, board.columns];

            Position pos = new Position (0, 0);

            pos.setValues (this.position.line - 1, this.position.column);
            while (board.positionIsValid (pos) && canMove (pos)) {
                mat[pos.line, pos.column] = true;
                if (board.piece (pos) != null && board.piece (pos).color != this.color) {
                    break;
                }

                pos.line -= 1;
            }

            pos.setValues (this.position.line, this.position.column + 1);
            while (board.positionIsValid (pos) && canMove (pos)) {
                mat[pos.line, pos.column] = true;
                if (board.piece (pos) != null && board.piece (pos).color != this.color) {
                    break;
                }

                pos.column += 1;
            }

            pos.setValues (this.position.line + 1, this.position.column);
            while (board.positionIsValid (pos) && canMove (pos)) {
                mat[pos.line, pos.column] = true;
                if (board.piece (pos) != null && board.piece (pos).color != this.color) {
                    break;
                }

                pos.line += 1;
            }

            pos.setValues (this.position.line, this.position.column - 1);
            while (board.positionIsValid (pos) && canMove (pos)) {
                mat[pos.line, pos.column] = true;
                if (board.piece (pos) != null && board.piece (pos).color != this.color) {
                    break;
                }

                pos.column -= 1;
            }

            return mat;
        }
    }
}
