using board;

namespace chess {
    class Pawn : Piece {
        public Pawn (Board board, Color color) : base (board, color) { }

        public override string ToString () {
            return "P";
        }

        private bool existsOpponent(Position pos) {
            Piece p = board.piece( pos );
            return p != null && p.color != color;
        }

        private bool free(Position pos) {
            return board.piece( pos ) == null;
        }
        
        public override bool[, ] possibleMoves () {
            bool[, ] mat = new bool[board.lines, board.columns];

            Position pos = new Position( 0 , 0 );
            if(color == Color.White ) {

                pos.setValues( this.position.line - 1 , this.position.column );
                if ( board.positionIsValid( pos ) && free( pos ) ) {
                    mat[ pos.line , pos.column ] = true;
                }

                pos.setValues( this.position.line - 2 , this.position.column );
                Position pos2 = new Position( position.line - 1 , position.column );
                if (board.positionIsValid(pos2) && free(pos2) && board.positionIsValid( pos ) && free( pos ) && movementsCount == 0 ) {
                    mat[ pos.line , pos.column ] = true;
                }

                pos.setValues( this.position.line - 1 , this.position.column - 1 );
                if ( board.positionIsValid( pos ) && existsOpponent( pos ) ) {
                    mat[ pos.line , pos.column ] = true;
                }

                pos.setValues( this.position.line - 1 , this.position.column + 1 );
                if ( board.positionIsValid( pos ) && existsOpponent( pos ) ) {
                    mat[ pos.line , pos.column ] = true;
                }
            } else {
                pos.setValues( this.position.line + 1 , this.position.column );
                if ( board.positionIsValid( pos ) && free( pos ) ) {
                    mat[ pos.line , pos.column ] = true;
                }

                pos.setValues( this.position.line + 2 , this.position.column );
                Position pos2 = new Position( position.line - 1 , position.column );
                if ( board.positionIsValid( pos2 ) && free( pos2 ) && board.positionIsValid( pos ) && free( pos ) && movementsCount == 0 ) {
                    mat[ pos.line , pos.column ] = true;
                }

                pos.setValues( this.position.line + 1 , this.position.column - 1 );
                if ( board.positionIsValid( pos ) && existsOpponent( pos ) ) {
                    mat[ pos.line , pos.column ] = true;
                }

                pos.setValues( this.position.line + 1 , this.position.column + 1 );
                if ( board.positionIsValid( pos ) && existsOpponent( pos ) ) {
                    mat[ pos.line , pos.column ] = true;
                }
            }
            

            return mat;
        }
    }
}
