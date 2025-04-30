using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var availableMoves = new List<Square>();
            var currentSquare = board.FindPiece(this);
            const int boardSize = GameSettings.BoardSize;
            
            // Set possible directions for the bishop to move.
            var directions = new[] {
                (1, 1),
                (-1, 1),
                (1, -1),
                (-1, -1) 
            };
            
            
            foreach (var(rowDir, colDir) in directions)
            {
                for (var i = 1; i < boardSize; i++)
                {
                    var newRow = currentSquare.Row + i * rowDir;
                    var newCol = currentSquare.Col + i * colDir;
                    
                    if (newRow >= 0 && newRow < boardSize && newCol >= 0 && newCol < boardSize) 
                        availableMoves.Add(Square.At(newRow, newCol));
                }
            }

            return availableMoves;
        }
    }
}