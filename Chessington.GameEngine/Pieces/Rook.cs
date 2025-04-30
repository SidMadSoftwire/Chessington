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
            const int boardSize = GameSettings.BoardSize;
            
            // Set possible directions for the rook to move.
            var directions = new[] {
                (0, 1),
                (0, -1),
                (1, 0),
                (-1, 0) 
            };

            foreach (var (rowDir, colDir) in directions)
            {
                for (var i = 1; i < boardSize; i++)
                {
                    var newRow = currentSquare.Row + i * rowDir;
                    var newCol = currentSquare.Col + i * colDir;

                    if (newRow < 0 || newRow >= boardSize || newCol < 0 || newCol >= boardSize) break;

                    if (board.GetPiece(Square.At(newRow, newCol)) == null || board.GetPiece(Square.At(newRow, newCol)).Player != this.Player)
                    {
                        availableMoves.Add(Square.At(newRow, newCol));
                    }
                    if (board.GetPiece(Square.At(newRow, newCol)) != null) break;
                }
            }

            return availableMoves;
        }
    }
}