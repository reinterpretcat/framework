﻿using System.Collections.Generic;
using Mercraft.Core.Tiles;
using Mercraft.Core.World.Roads;
using Mercraft.Models.Details;
using Mercraft.Models.Roads;
using UnityEngine;

namespace Mercraft.Models.Terrain
{
    public class TerrainSettings
    {
        public Tile Tile;

        /// <summary>
        /// This is the control map that controls how the splat textures will be blended
        /// </summary>
        public int AlphaMapSize = 512;

        /// <summary>
        /// A lower pixel error will draw terrain at a higher Level of detail but will be slower
        /// </summary>
        public float PixelMapError = 100f;

        /// <summary>
        /// The distance at which the low res base map will be drawn. Decrease to increase performance
        /// </summary>
        public float BaseMapDist = 200.0f;

        public List<List<string>> TextureParams;

        public IEnumerable<AreaSettings> Areas;
        public IEnumerable<Road> Roads;
        public IEnumerable<AreaSettings> Elevations;
        public IEnumerable<TreeDetail> Trees;

        public IRoadBuilder RoadBuilder;
        public IRoadStyleProvider RoadStyleProvider;

        public float ZIndex;
        public Vector2 CenterPosition;
        public Vector2 CornerPosition;
    }
}
