using Backend.Domains.Board;
using Backend.Domains.PieceDomain;
using Backend.Domains.TileDomain;
using Backend.Services.BoardServices.Interfaces;
using Common.DTOs;
using Common.Enums;

namespace Backend.Services.BoardServices
{
    public class BoardService : IBoardService
    {
        Task<List<Piece>> IBoardService.FindValidPicesToMove(List<Piece> pieces, ColourEnum colour, int diceRoll, List<Tile> tiles, List<Tile> playerZone)
        {
            var colourPieces = pieces.Where(x => x.Colour == colour).ToList();

            var validPieces = new List<Piece>();

            if (colourPieces is null)
                return Task.FromResult(validPieces);

            if (colourPieces.Count == 1)
                return Task.FromResult(colourPieces);

            foreach (var piece in colourPieces)
            {
                var piecePosIndex = piece.PosIndex;
                var directions = tiles.ElementAt(piecePosIndex.Index).Directions;
                var tilesToCross = new List<PosIndex>();
                bool movingBackwards = false;

                for (int i = 0; i < diceRoll; i++)
                {
                    if (movingBackwards && directions.TryGetValue(DirectionEnum.Backward, out var movingBackwardsPos))
                    {
                        var playerTile = playerZone.Find(x => x.PosIndex == movingBackwardsPos);
                        tilesToCross.Add(playerTile.PosIndex);
                        directions = playerTile.Directions;
                        continue;
                    }

                    if (directions.TryGetValue(DirectionEnum.ToColourZone, out var colourZonePos) && colourZonePos.Colour == colour)
                    {
                        var playerTile = playerZone.Find(x => x.PosIndex == colourZonePos);
                        tilesToCross.Add(playerTile.PosIndex);
                        directions = playerTile.Directions;
                        continue;
                    }

                    if (directions.TryGetValue(DirectionEnum.Backward, out var backwardPos) && backwardPos.Index == playerZone.Count - 1)
                    {
                        var playerTile = playerZone.Find(x => x.PosIndex == backwardPos);
                        tilesToCross.Add(playerTile.PosIndex);
                        directions = playerTile.Directions;
                        movingBackwards = true;
                        continue;
                    }

                    if (directions.TryGetValue(DirectionEnum.Forward, out var forwardPos))
                    {
                        if (forwardPos.Colour != ColourEnum.None)
                        {
                            var playerTile = playerZone.Find(x => x.PosIndex == forwardPos);
                            tilesToCross.Add(playerTile.PosIndex);
                            directions = playerTile.Directions;
                        }
                        else
                        {
                            var tile = tiles.Find(x => x.PosIndex == forwardPos);
                            tilesToCross.Add(tile.PosIndex);
                            directions = tile.Directions;
                        }
                        continue;
                    }
                }

                if (colourPieces.Any(x => tilesToCross.Contains(x.PosIndex) && x.ID != piece.ID))
                    validPieces.Add(piece);
            }
            return Task.FromResult(validPieces);
        }

        Task<bool> IBoardService.GetPiecesInGoal(List<Tile> tiles, ColourEnum colour, int pieceId)
        {
            throw new NotImplementedException();
        }

        Task<PosIndex> IBoardService.GetStartTilePos(List<Tile> tiles, ColourEnum colour)
        {

            var startTile = tiles.Find(x => x.Colour == colour && x.IsStartTile == true);
            if (startTile is null)
                throw new Exception($"Could not find startTile for player: {colour.ToString()}");

            var startPosIndex = startTile.PosIndex;
            return Task.FromResult(startPosIndex);
        }

        Task<PosIndex> IBoardService.GetTileEndPos(List<Tile> tiles, PosIndex piecePosIndex, ColourEnum pieceColour, int diceRoll)
        {
            throw new NotImplementedException();
        }

        Task<Piece> IBoardService.MovePiece(Piece piece)
        {
            throw new NotImplementedException();
        }
    }
}
