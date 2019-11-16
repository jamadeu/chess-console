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

        public abstract bool[, ] possibleMoves ();

    }
}
