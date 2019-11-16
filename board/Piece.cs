namespace board {
    abstract class Piece {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int movementsCount { get; protected set; }
        public Board board { get; set; }

        public Piece (Board board, Color color) {
            this.position = null;
            this.board = board;
            this.color = color;
        }

        public void incrementMovementsCount () {
            movementsCount++;
        }
        public void decrementMovementsCount() {
            movementsCount--;
        }

        public bool existsPossibleMoves() {
            bool[,] mat = possibleMoves();
            for (int i  = 0; i<board.lines;i++ ) {
                for(int j = 0; j< board.columns;i++ ) {
                    if ( mat[ i , j ] ) {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool isPossibleMoveTo(Position pos ) {
            return possibleMoves()[ pos.line , pos.column ];
        }

        public abstract bool[, ] possibleMoves ();

    }
}
