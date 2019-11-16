namespace board {
    class Board {
        public int lines { get; set; }
        public int columns { get; set; }
        private Piece[, ] pieces;

        public Board (int lines, int columns) {
            this.lines = lines;
            this.columns = columns;
            pieces = new Piece[lines, columns];
        }

        public Piece piece (int line, int column) {
            return pieces[line, column];
        }

        public Piece piece (Position p) {
            return pieces[p.line, p.column];
        }

        public bool positionIsFree (Position p) {
            checkPosition (p);
            return piece (p) != null;
        }

        public Piece removePiece (Position pos) {
            if (piece (pos) == null) {
                return null;
            }
            Piece aux = piece (pos);
            aux.position = null;
            pieces[pos.line, pos.column] = null;
            return aux;
        }

        public void putPiece (Piece p, Position pos) {
            if (positionIsFree (pos)) {
                throw new BoardException ("Position is occupied!");
            }
            pieces[pos.line, pos.column] = p;
            p.position = pos;
        }

        public bool positionIsValid (Position p) {
            if (p.line < 0 || p.line >= lines || p.column < 0 || p.column >= columns) {
                return false;
            }

            return true;
        }

        public void checkPosition (Position p) {
            if (!positionIsValid (p)) {
                throw new BoardException ("Position is invalid!");
            }
        }
    }
}
