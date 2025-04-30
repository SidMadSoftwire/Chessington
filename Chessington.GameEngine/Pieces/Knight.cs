using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Knight : Piece
    {
        public Knight(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var availableMoves = new List<Square>();
            var currentSquare = board.FindPiece(this);
            var boardSize = GameSettings.BoardSize;
            
            // Set possible directions for the knight to move.
            var directions = new[] {
                (2, 1),
                (2, -1),
                (-2, 1),
                (-2, -1),
                (1, 2),
                (-1, 2),
                (1, -2),
                (-1, -2)
            };
            
            
            foreach (var(rowDir, colDir) in directions)
            {
                var newRow = currentSquare.Row + rowDir;
                var newCol = currentSquare.Col + colDir;
                
                if (newRow >= 0 && newRow < boardSize && newCol >= 0 && newCol < boardSize) 
                    availableMoves.Add(Square.At(newRow, newCol));
            }

            return availableMoves;
        }
    }
}