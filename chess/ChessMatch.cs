using board;
using System.Collections.Generic;

namespace chess {
    class ChessMatch {
        public Board Board { get; private set; }
        public int Shift { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> caught;
        public bool InCheck { get; set; }

        public ChessMatch () {
            Board = new Board (8, 8);
            Shift = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            InCheck = false;
            pieces = new HashSet<Piece>();
            caught = new HashSet<Piece>();
            putPieces ();
        }

        public void putNewPiece(char coluna, int linha, Piece piece ) {
            Board.putPiece( piece , new ChessPosition( coluna , linha ).toPosition() );
            pieces.Add( piece );
        }
        private void putPieces () {

            putNewPiece( 'a' , 1 , new Rook( Board , Color.White ));
            putNewPiece( 'b' , 1 , new Knight( Board , Color.White ));
            putNewPiece( 'c' , 1 , new Bishop( Board , Color.White ));
            putNewPiece( 'd' , 1 , new Queen( Board , Color.White ));
            putNewPiece( 'e' , 1 , new King( Board , Color.White ));
            putNewPiece( 'f' , 1 , new Bishop( Board , Color.White ));
            putNewPiece( 'g' , 1 , new Knight( Board , Color.White ));
            putNewPiece( 'h' , 1 , new Rook( Board , Color.White ));
            putNewPiece( 'a' , 2 , new Pawn( Board , Color.White ));
            putNewPiece( 'b' , 2 , new Pawn( Board , Color.White ));
            putNewPiece( 'c' , 2 , new Pawn( Board , Color.White ));
            putNewPiece( 'd' , 2 , new Pawn( Board , Color.White ));
            putNewPiece( 'e' , 2 , new Pawn( Board , Color.White ));
            putNewPiece( 'f' , 2 , new Pawn( Board , Color.White ));
            putNewPiece( 'g' , 2 , new Pawn( Board , Color.White ));
            putNewPiece( 'h' , 2 , new Pawn( Board , Color.White ));

            putNewPiece( 'a' , 8 , new Rook( Board , Color.Black ) );
            putNewPiece( 'b' , 8 , new Knight( Board , Color.Black ) );
            putNewPiece( 'c' , 8 , new Bishop( Board , Color.Black ) );
            putNewPiece( 'd' , 8 , new Queen( Board , Color.Black ) );
            putNewPiece( 'e' , 8 , new King( Board , Color.Black ) );
            putNewPiece( 'f' , 8 , new Bishop( Board , Color.Black ) );
            putNewPiece( 'g' , 8 , new Knight( Board , Color.Black ) );
            putNewPiece( 'h' , 8 , new Rook( Board , Color.Black ) );
            putNewPiece( 'a' , 7 , new Pawn( Board , Color.Black ) );
            putNewPiece( 'b' , 7 , new Pawn( Board , Color.Black ) );
            putNewPiece( 'c' , 7 , new Pawn( Board , Color.Black ) );
            putNewPiece( 'd' , 7 , new Pawn( Board , Color.Black ) );
            putNewPiece( 'e' , 7 , new Pawn( Board , Color.Black ) );
            putNewPiece( 'f' , 7 , new Pawn( Board , Color.Black ) );
            putNewPiece( 'g' , 7 , new Pawn( Board , Color.Black ) );
            putNewPiece( 'h' , 7 , new Pawn( Board , Color.Black ) );
        }

        public void checkOriginPosition(Position pos ) {
            if(Board.piece(pos) == null ) {
                throw new BoardException( "There is no piece in the chosen origin position" );
            }

            if(CurrentPlayer != Board.piece( pos ).color ) {
                throw new BoardException( "The piece chosen is not yours" );
            }

            if ( !Board.piece( pos ).existsPossibleMoves() ) {
                throw new BoardException( "There are no possible moves for the chosen piece" );
            }
        }

        public void checkDestinyPosition(Position origin, Position destiny ) {
            if ( !Board.piece( origin ).isPossibleMoveTo( destiny ) ) {
                throw new BoardException( "Invalid destiny position" );
            }
        }

        public Piece executeMovement (Position origin, Position destiny) {
            Piece p = Board.removePiece (origin);
            p.incrementMovementsCount ();
            Piece pieceCaptured = Board.removePiece (destiny);
            Board.putPiece (p, destiny);
            if( pieceCaptured != null ) {
                caught.Add( pieceCaptured );
            }
            return pieceCaptured;
        }

        public void undoMovement(Position origin, Position destiny, Piece pieceCaptured ) {
            Piece p = Board.removePiece( destiny );
            p.decrementMovementsCount();
            if ( pieceCaptured != null ) {
                Board.putPiece( pieceCaptured , destiny );
                caught.Remove( pieceCaptured );
            }
            Board.putPiece( p , origin );
        }

        public void executePlay (Position origin, Position destiny) {
            Piece pieceCaptured = executeMovement (origin, destiny);

            if ( isInCheck( CurrentPlayer ) ) {
                undoMovement(origin, destiny, pieceCaptured);
                throw new BoardException( "You can't check yourself!" );
            }

            if ( isInCheck( opponent( CurrentPlayer ) ) ) {
                InCheck = true;
            } else {
                InCheck = false;
            }

            if ( isCheckMate( opponent( CurrentPlayer ) ) ) {
                Finished = true;
            }
            Shift++;
            changePlayer ();

        }

        private void changePlayer () {
            if (CurrentPlayer == Color.White) {
                CurrentPlayer = Color.Black;
            } else {
                CurrentPlayer = Color.White;
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
            foreach ( Piece p in pieces ) {
                if ( p.color == color ) {
                    aux.Add( p );
                }
            }

            aux.ExceptWith( piecesCaptured( color ) );
            return aux;
        }

        public bool isCheckMate(Color color ) {
            if ( !isInCheck( color ) ) {
                return false;
            }

            foreach(Piece p in piecesInGame( color ) ) {
                bool[,] mat = p.possibleMoves();
                for ( int i = 0; i< Board.lines; i++ ) {
                    for ( int j = 0; j< Board.columns; i++ ) {
                        if ( mat[ i , j ] ) {
                            Position origin = p.position;
                            Position destiny = new Position( i , j );
                            Piece pieceCaptured = executeMovement( origin, destiny );
                            bool testCheck = isInCheck( color );
                            undoMovement( origin , destiny , pieceCaptured );
                            if ( !testCheck ) {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        private Color opponent(Color color ) {
            if(color == Color.Black ) {
                return Color.White;
            } 
            return Color.Black;
        }

        private Piece king(Color color ) {
            foreach(Piece p in piecesInGame( color ) ) {
                if( p is King ) {
                    return p;
                }
            }
            return null;
        }

        public bool isInCheck(Color color ) {
            Piece k = king( color );
            if( k == null ) {
                throw new BoardException( "There is no king of color" + color + " on the board " );
            }

            foreach(Piece p in piecesInGame( opponent( color ) ) ) {
                bool[,] mat = p.possibleMoves();
                if(mat[k.position.line, k.position.column ] ) {
                    return true;
                }
            }

            return false;
        }
    }
}
