using System;
using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class King : Piece
    {
        public bool hasMoved;
        private List<Square> castleableRookSquares;

        public King(Player player)
            : base(player)
        {
            hasMoved = false;      
            castleableRookSquares = new List<Square>(); 
        }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var availableMoves = new List<Square>();
            var currentSquare = board.FindPiece(this);
            const int boardSize = GameSettings.BoardSize;
            
            // Set possible directions for the king to move.
            var directions = new[] {
                (1, 1),
                (1, 0),
                (1, -1),
                (-1, 1),
                (-1, 0),
                (-1, -1),
                (0, 1),
                (0, -1),
            };
            
            foreach (var(rowDir, colDir) in directions)
            {
                var newRow = currentSquare.Row + rowDir;
                var newCol = currentSquare.Col + colDir;
                
                if (newRow >= 0 && newRow < boardSize && newCol >= 0 && newCol < boardSize) 
                    if (board.GetPiece(Square.At(newRow, newCol)) == null || board.GetPiece(Square.At(newRow, newCol)).Player != this.Player)
                    {
                        availableMoves.Add(Square.At(newRow, newCol));
                    }
            }
            
            if (this.hasMoved == false)
            {
                var castlingDirs = new[] { 3, 4,-3, -4 };
                
                foreach (var colDir in castlingDirs)
                {
                    if (currentSquare.Col + colDir < 0 || currentSquare.Col + colDir >= boardSize) continue;
                    var possRookSquare = Square.At(currentSquare.Row, currentSquare.Col + colDir);
                    var possRook = board.GetPiece(possRookSquare);

                    if (possRook == null || possRook.GetType() != typeof(Rook) || possRook.Player != this.Player || ((Rook)possRook).hasMoved) 
                        continue;
                    
                    if (IsCastlingPossible(board, possRookSquare, currentSquare))
                    {
                        var dir = possRookSquare.Col - currentSquare.Col > 0 ? 1 : -1;
                        availableMoves.Add(Square.At(currentSquare.Row, currentSquare.Col + dir * 2));
                        castleableRookSquares.Add(possRookSquare);
                    }
                }
                
            }

            return availableMoves;
        }
        
        private static bool IsCastlingPossible(Board board, Square kingSquare, Square rookSquare)
        {
            var distance = Math.Abs(kingSquare.Col - rookSquare.Col);
            var dir = kingSquare.Col - rookSquare.Col > 0 ? -1 : 1;
            for (var i = 1; i < distance; i++)
            {
                if (board.GetPiece(Square.At(kingSquare.Row, kingSquare.Col + i * dir)) != null) return false;
            }
            return true;
        }
        
        public override void MoveTo(Board board, Square newSquare)
        {
            var currentSquare = board.FindPiece(this);
            board.MovePiece(currentSquare, newSquare);
            this.hasMoved = true;

            if (Math.Abs(newSquare.Col - currentSquare.Col) == 2)
            {
                board.SetCurrentPlayer(this.Player);
                foreach (var rookSquare in castleableRookSquares)
                {
                    if (rookSquare.Col < newSquare.Col && newSquare.Col < currentSquare.Col)
                        board.MovePiece(rookSquare, Square.At(rookSquare.Row, newSquare.Col + 1));
                    else if (currentSquare.Col < newSquare.Col && newSquare.Col < rookSquare.Col)
                        board.MovePiece(rookSquare, Square.At(rookSquare.Row, newSquare.Col - 1));;
                }
            }
        }
    }
}