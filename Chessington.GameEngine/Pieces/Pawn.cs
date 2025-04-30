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
            const int boardSize = GameSettings.BoardSize;

            var dir = this.Player == Player.White ? -1 : 1;

            if (currentSquare.Row + dir >= boardSize || currentSquare.Row + dir < 0) return availableMoves;

            var frontSquare = Square.At(currentSquare.Row + dir, currentSquare.Col);
            if (board.GetPiece(frontSquare) == null)
                availableMoves.Add(frontSquare);

            var sides = new[] { 1, -1 };
            foreach (var side in sides)
            {
                var diagonalSquare = Square.At(currentSquare.Row + dir, currentSquare.Col + side);
                if (diagonalSquare.Col >= 0 && diagonalSquare.Col < boardSize)
                {
                    var diagonalPiece = board.GetPiece(diagonalSquare);
                    if (diagonalPiece != null && diagonalPiece.Player != this.Player)
                        availableMoves.Add(diagonalSquare);
                }
            }
            
            if (this.hasMoved == false)
            {
                if (currentSquare.Row + 2 * dir >= boardSize || currentSquare.Row + 2 * dir < 0) return availableMoves;
                
                var doubleFrontSquare = Square.At(currentSquare.Row + 2 * dir, currentSquare.Col);
                if (board.GetPiece(frontSquare) == null && board.GetPiece(doubleFrontSquare) == null)
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