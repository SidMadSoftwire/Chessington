using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {

        private bool hasMoved;
        public Pawn(Player player)
            : base(player)
        {
            hasMoved = false;
        }
        
        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {   
            var availableMoves = new List<Square>();
            var currentSquare = board.FindPiece(this);

            var dir = this.Player == Player.White ? -1 : 1;

            if (currentSquare.Row + dir >= GameSettings.BoardSize || currentSquare.Row + dir < 0) return availableMoves;

            var frontSquare = Square.At(currentSquare.Row + dir, currentSquare.Col);
            if (board.GetPiece(frontSquare) == null)
                availableMoves.Add(frontSquare);
               
            var diagonalLeftSquare = Square.At(currentSquare.Row + dir, currentSquare.Col - 1);
            if (diagonalLeftSquare.Col >= 0 &&board.GetPiece(diagonalLeftSquare) != null)
                availableMoves.Add(diagonalLeftSquare);
               
            var diagonalRightSquare = Square.At(currentSquare.Row + dir, currentSquare.Col + 1);
            if (diagonalRightSquare.Col < GameSettings.BoardSize && board.GetPiece(diagonalRightSquare) != null)
                availableMoves.Add(diagonalRightSquare);
   
            if (this.hasMoved == false)
            {
                if (currentSquare.Row + 2 * dir >= GameSettings.BoardSize || currentSquare.Row + 2 * dir < 0) return availableMoves;
                
                var doubleFrontSquare = Square.At(currentSquare.Row + 2 * dir, currentSquare.Col);
                if (board.GetPiece(doubleFrontSquare) == null)
                    availableMoves.Add(doubleFrontSquare);
            }
            
            return availableMoves;
        }
        
        public void MoveTo(Board board, Square newSquare)
        {
            var currentSquare = board.FindPiece(this);
            board.MovePiece(currentSquare, newSquare);
            this.hasMoved = true;
        }
    }
}