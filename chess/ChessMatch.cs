using board;
using System.Collections.Generic;

namespace chess {
    class ChessMatch {
        public Board board { get; private set; }
        public int shift { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool finished { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> caught;

        public ChessMatch () {
            board = new Board (8, 8);
            shift = 1;
            currentPlayer = Color.White;
            finished = false;
            pieces = new HashSet<Piece>();
            caught = new HashSet<Piece>();
            putPieces ();
        }

        public void putNewPiece(char coluna, int linha, Piece piece ) {
            board.putPiece( piece , new ChessPosition( coluna , linha ).toPosition() );
            pieces.Add( piece );
        }
        private void putPieces () {

            putNewPiece( 'a' , 1 , new Rook( board , Color.White ));
            putNewPiece( 'a' , 8 , new Rook( board , Color.Black ));
        }

        public void checkOriginPosition(Position pos ) {
            if(board.piece(pos) == null ) {
                throw new BoardException( "There is no piece in the chosen origin position" );
            }

            if(currentPlayer != board.piece( pos ).color ) {
                throw new BoardException( "The piece chosen is not yours" );
            }

            if ( !board.piece( pos ).existsPossibleMoves() ) {
                throw new BoardException( "There are no possible moves for the chosen piece" );
            }
        }

        public void checkDestinyPosition(Position origin, Position destiny ) {
            if ( !board.piece( origin ).isPossibleMoveTo( destiny ) ) {
                throw new BoardException( "Invalid destiny position" );
            }
        }

        public void executeMovement (Position origin, Position destiny) {
            Piece p = board.removePiece (origin);
            p.incrementMovementsCount ();
            Piece pieceCaptured = board.removePiece (destiny);
            board.putPiece (p, destiny);
            if( pieceCaptured != null ) {
                caught.Add( pieceCaptured );
            }
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

        public HashSet<Piece> piecesCaptured(Color color ) {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(Piece p in caught ) {
                if(p.color == color ) {
                    aux.Add( p );
                }
            }
            return aux;
        }

        public HashSet<Piece> piecesInGame( Color color ) {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach ( Piece p in caught ) {
                if ( p.color == color ) {
                    aux.Add( p );
                }
            }

            aux.ExceptWith( piecesCaptured( color ) );
            return aux;
        }
    }
}
