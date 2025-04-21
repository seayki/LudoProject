using Backend.Domains.PieceDomain;
using Backend.Domains.TileDomain;
using Backend.Services.BoardServices.Interfaces;
using Common.DTOs;
using Common.Enums;

namespace Backend.Services.BoardServices
{
    public class BoardService : IBoardService
    {
        PosIndex IBoardService.GetStartTilePos(List<Tile> tiles, ColourEnum colour)
        {
            var startTilePos = GetStartTileForColour(colour, tiles);
            if (startTilePos is null)
                throw new Exception($"Could not find startTile for player: {colour.ToString()}");

            return startTilePos;
        }

        List<Piece> IBoardService.FindValidPicesToMove(List<Piece> pieces, ColourEnum colour, int diceRoll, List<Tile> tiles, List<Tile> playerZone)
        {
            var colourPieces = pieces.Where(x => x.Colour == colour && x.IsFinished == false).ToList();

            var validPieces = new List<Piece>();

            if (diceRoll == 6)
                validPieces.AddRange(colourPieces.Where(x => x.IsInPlay == false));

            if (colourPieces is null)
                return validPieces;

            if (colourPieces.Count == 1)
                return colourPieces;

            foreach (var piece in colourPieces.Where(x => x.PosIndex != null))
            {
                var tilesToCross = GetTilesToCross(piece, colour, diceRoll, tiles, playerZone);
                tilesToCross.RemoveAt(tilesToCross.Count - 1);

                if (!colourPieces.Any(x => tilesToCross.Contains(x.PosIndex) && x.ID != piece.ID))
                    validPieces.Add(piece);
            }
            return validPieces;
        }

        List<Piece> IBoardService.MovePiece(List<Piece> pieces, Piece piece, ColourEnum colour, int diceRoll, List<Tile> tiles, List<Tile> playerZone)
        {
            List<Piece> piecesChanged = new List<Piece>();

            if (piece.IsInPlay == false)
            {
                piece.IsInPlay = true;
                piece.PosIndex = GetStartTileForColour(colour, tiles);
                piecesChanged.Add(piece);
                return piecesChanged;
            }

            var tilesToCross = GetTilesToCross(piece, colour, diceRoll, tiles, playerZone);

            var tileToMoveTo = tilesToCross.Last();

            var pieceToSendHome = CheckTileForPieceToSendHome(pieces, piece, tileToMoveTo);

            if (pieceToSendHome is not null)
            {
                SendPieceHome(pieceToSendHome);
                piecesChanged.Add(pieceToSendHome);
            }

            if (pieceToSendHome is not null && pieceToSendHome.ID == piece.ID)
                return piecesChanged;

            piecesChanged.Add(piece);
            if (tileToMoveTo == playerZone.Last().PosIndex)
            {
                piece.PosIndex = tileToMoveTo;
                piece.IsFinished = true;
                return piecesChanged;
            }

            piece.PosIndex = tileToMoveTo;
            return piecesChanged;
        }

        void IBoardService.SendPieceHome(Piece piece)
        {
            SendPieceHome(piece);
        }

        private PosIndex? GetStartTileForColour(ColourEnum colour, List<Tile> tiles)
        {
            var startTile = tiles.Find(x => x.Colour == colour && x.IsStartTile == true);
            return startTile?.PosIndex;
        }

        private Piece? CheckTileForPieceToSendHome(List<Piece> pieces, Piece piece, PosIndex posIndex)
        {
            var piecesOnTile = pieces.Where(x => x.PosIndex == posIndex && x.IsInPlay == true && x.IsFinished == false).ToList();

            if (piecesOnTile is null || piecesOnTile.Count < 1 || piecesOnTile.First().Colour == piece.Colour)
                return null;

            if (piecesOnTile.First().Colour != piece.Colour && piecesOnTile.Count > 1)
                return piece;

            if (piecesOnTile.First().Colour != piece.Colour && piecesOnTile.Count == 1)
                return piecesOnTile.First();

            return null;
        }

        private void SendPieceHome(Piece piece)
        {
            piece.IsInPlay = false;
            piece.PosIndex = null;
        }

        private List<PosIndex> GetTilesToCross(Piece piece, ColourEnum colour, int diceRoll, List<Tile> tiles, List<Tile> playerZone)
        {
            var piecePosIndex = piece.PosIndex;
            var directions = piece.PosIndex.Colour != ColourEnum.None ? playerZone.ElementAt(piecePosIndex.Index).Directions : tiles.ElementAt(piecePosIndex.Index).Directions;
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

                if (directions.TryGetValue(DirectionEnum.Backward, out var backwardPos) && (!tilesToCross.Any() ? piecePosIndex.Index : tilesToCross.Last().Index) == playerZone.Count - 1)
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
            return tilesToCross;
        }

        public Task<PosIndex> GetTileEndPos(PosIndex piecePosIndex, ColourEnum pieceColour, int diceRoll)
        {
            throw new NotImplementedException();
        }
    }
}
