using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Queen : Piece
    {
        public Queen(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var availableMoves = new List<Square>();
            var currentSquare = board.FindPiece(this);
            var boardSize = GameSettings.BoardSize;
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
            
            for (var i = 0; i < 8; i++)
            {
                if (i != currentSquare.Col) availableMoves.Add(Square.At(currentSquare.Row, i));
                if (i != currentSquare.Row) availableMoves.Add(Square.At(i, currentSquare.Col));
            }

            return availableMoves;
        }
    }
}