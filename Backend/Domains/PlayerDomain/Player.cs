﻿using Backend.Domains.PieceDomain;
using Common.DTOs;
using Common.Enums;

namespace Backend.Domains.PlayerDomain
{

    public class Player
    {
        public int Id { get; init; }
        public ColourEnum Colour { get; init; }
        public List<Piece> Pieces { get; set; } = new List<Piece>();
        public bool IsTurn { get; set; }
        public PosIndex? StartTile { get; set; }
        public int LastRoll { get; set; }
        public Player(int id, ColourEnum colour)
        {
            Pieces.AddRange(new Piece[] { new Piece(0, colour), new Piece(1, colour), new Piece(2, colour), new Piece(3, colour) });
            if (colour == ColourEnum.None)
            {
                throw new Exception("A player must have a valid colour");
            }
            if (Pieces.Count != 4)
            {
                throw new Exception("A player must have 4 pieces");
            }
            Id = id;
            Colour = colour;
            IsTurn = false;
            StartTile = null;
        }

        public List<Piece> GetPiecesInPlay()
        {
            var pieces = Pieces.Where(p => p.IsInPlay).ToList();
            return pieces;
        }

        public bool AnyPiecesInPlay()
        {
            var pieces = Pieces.Where(p => p.IsInPlay).ToList();
            return pieces.Any();
        }

        public bool HasFinished()
        {
            return Pieces.All(p => p.IsFinished);
        }

        public void ReturnPieceHome(int pieceId)
        {
            var piece = Pieces.Find(p => p.ID == pieceId);
            piece!.PosIndex = StartTile;
            piece.IsInPlay = false;
        }
    }
}
