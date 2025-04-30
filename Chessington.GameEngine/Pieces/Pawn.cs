using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {

        private bool hasMoved;
        public bool jumpedPrevTurn;
        public Pawn(Player player)
            : base(player)
        {
            hasMoved = false;
            jumpedPrevTurn = false;
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
                if (diagonalSquare.Col < 0 || diagonalSquare.Col >= boardSize) continue;
                
                var diagonalPiece = board.GetPiece(diagonalSquare);
                if (diagonalPiece != null && diagonalPiece.Player != this.Player)
                    availableMoves.Add(diagonalSquare);
                
                if (diagonalPiece == null)
                { 
                    var diagonalBehind = Square.At(diagonalSquare.Row - dir, diagonalSquare.Col);
                    var diagonalBehindPiece = board.GetPiece(diagonalBehind);
                    
                    if (diagonalBehindPiece != null && diagonalBehindPiece.GetType() == typeof(Pawn))
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
            
            if (newSquare.Row - currentSquare.Row == 2 || newSquare.Row - currentSquare.Row == -2)
            {
                this.jumpedPrevTurn = true;
            }
            else
            {
                this.jumpedPrevTurn = false;
            }
        }
    }
}