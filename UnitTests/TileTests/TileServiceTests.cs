using Backend.Domains.TileDomain;
using Backend.Services.TileServices;
using Common.DTOs;
using Common.Enums;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.TileTests
{

    public class TileServiceTests
    {


        [Fact]
        public async void Test_CheckNextTile_ReturnsTheForwardTilePosIndex_WhenPieceColorIsNotTheColorOnTheCurrentPos()
        {
            //Arrange
            ITileService service = new TileService();
            PosIndex currentPos = new PosIndex() { Index = 0, Colour = ColourEnum.None };
            PosIndex forwardTilePosIndex = new PosIndex() { Index = 1, Colour = ColourEnum.None };
            Dictionary<DirectionEnum, PosIndex> directions = new Dictionary<DirectionEnum, PosIndex>() { { DirectionEnum.Forward, forwardTilePosIndex } };
            ColourEnum pieceColor = ColourEnum.Blue;
            Tile tile = new Tile(service, currentPos, directions);

            PosIndex expectedPosIndex = forwardTilePosIndex;

            //Act
            PosIndex nextTilePosIndex = await tile.tileService.CheckNextTile(currentPos, pieceColor, tile.directions, DirectionEnum.Forward);
            //Assert
            nextTilePosIndex.Should().BeEquivalentTo(expectedPosIndex);

        }

        [Fact]
        public async void Test_CheckNextTile_ReturnsAColoredTilePosIndex_WhenPieceColorIsTheColorOnTheCurrentPos()
        {
            //Arrange
            ITileService service = new TileService();
            ColourEnum pieceColor = ColourEnum.Blue;
            PosIndex currentPos = new PosIndex() { Index = 0, Colour = pieceColor };
            PosIndex forwardTilePosIndex = new PosIndex() { Index = 1, Colour = ColourEnum.None };
            PosIndex blueColoredTilePosIndex= new PosIndex() { Index = 1, Colour = ColourEnum.Blue };
            Dictionary<DirectionEnum, PosIndex> directions = new Dictionary<DirectionEnum, PosIndex>() { { DirectionEnum.Forward, forwardTilePosIndex },{ DirectionEnum.GoToColourTiles, blueColoredTilePosIndex } };
       
            Tile tile = new Tile(service, currentPos, directions);

            PosIndex expectedPosIndex = blueColoredTilePosIndex;

            //Act
            PosIndex nextTilePosIndex = await tile.tileService.CheckNextTile(currentPos, pieceColor, tile.directions, DirectionEnum.Forward);
            //Assert
            nextTilePosIndex.Should().BeEquivalentTo(expectedPosIndex);
        }

        [Fact]
        public async void Test_CheckNextTile_ReturnsAColoredTilePosIndex_WhenPieceColorIsTheColorOnTheCurrentPos_AndDirectionIsBackwards()
        {
            //Arrange
            ITileService service = new TileService();
            ColourEnum pieceColor = ColourEnum.Blue;
            PosIndex currentPos = new PosIndex() { Index = 0, Colour = pieceColor };
           
            PosIndex backWardsTilePosIndex = new PosIndex() { Index=5, Colour = ColourEnum.Blue };
            
            Dictionary<DirectionEnum, PosIndex> directions = new Dictionary<DirectionEnum, PosIndex>() { { DirectionEnum.Backward, backWardsTilePosIndex }};

            Tile tile = new Tile(service, currentPos, directions);

            PosIndex expectedPosIndex = backWardsTilePosIndex;

            //Act
            PosIndex nextTilePosIndex = await tile.tileService.CheckNextTile(currentPos, pieceColor, tile.directions, DirectionEnum.Backward);
            //Assert
            nextTilePosIndex.Should().BeEquivalentTo(expectedPosIndex);
        }


    }

   
}
