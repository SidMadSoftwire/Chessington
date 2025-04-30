using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Player player) 
            : base(player) { }

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
   
            if ((dir == 1 && currentSquare.Row == 1) || (dir == -1 && currentSquare.Row == GameSettings.BoardSize - 2))
            {
                var doubleFrontSquare = Square.At(currentSquare.Row + 2 * dir, currentSquare.Col);
                if (board.GetPiece(doubleFrontSquare) == null)
                    availableMoves.Add(doubleFrontSquare);
            }
            
            return availableMoves;
        }
    }
}