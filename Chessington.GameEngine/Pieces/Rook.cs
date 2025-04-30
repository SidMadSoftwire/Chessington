using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Rook : Piece
    {
        public Rook(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var availableMoves = new List<Square>();
            var currentSquare = board.FindPiece(this);

            for (var i = 0; i < 8; i++)
            {
                if (i != currentSquare.Col) availableMoves.Add(Square.At(currentSquare.Row, i));
                if (i != currentSquare.Row) availableMoves.Add(Square.At(i, currentSquare.Col));
            }

            return availableMoves;
        }
    }
}