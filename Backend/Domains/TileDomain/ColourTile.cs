using Backend.Services.TileServices;
using Common.Enums;

namespace Backend.Domains.TileDomain
{
    public class ColourTile
    {
        public ColourEnum colour;
        public bool isGoalTile;
        public bool isStartTile;
        private readonly IColourTileService colourTileService;
        public ColourTile(IColourTileService colourTileService)
        {
            
        }
        

    }
}
