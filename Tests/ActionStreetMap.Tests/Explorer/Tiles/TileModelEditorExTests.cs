﻿using System.Collections.Generic;
using System.Linq;
using ActionStreetMap.Core.Geometry;
using ActionStreetMap.Core.Scene;
using ActionStreetMap.Core.Tiling;
using ActionStreetMap.Core.Utils;
using ActionStreetMap.Explorer.Tiling;
using ActionStreetMap.Infrastructure.Dependencies;
using ActionStreetMap.Infrastructure.Reactive;
using ActionStreetMap.Maps.Data;
using ActionStreetMap.Maps.Entities;
using NUnit.Framework;

namespace ActionStreetMap.Tests.Explorer.Tiles
{
    [TestFixture (Description = "This test class does not use mocks.")]
    public class TileModelEditorExTests
    {
        private ITileModelEditor _tileEditor;
        private IElementSourceProvider _elementSourceProvider;
        private IContainer _container;
        private ITileController _tileController;

        [SetUp]
        public void Setup()
        {
            _container = new Container();
            TestHelper.GetGameRunner(_container)
                      .RunGame(TestHelper.BerlinTestFilePoint);
            _tileEditor = _container.Resolve<ITileModelEditor>();
            _elementSourceProvider = _container.Resolve<IElementSourceProvider>();
            _tileController = _container.Resolve<ITileController>();
        }

        [TearDown]
        public void TearDown()
        {
            _container.Dispose();
        }

        [Test]
        public void CanAddBuilding()
        {
            // ARRANGE
            var building = CreateBuilding();

            // ACT
            _tileEditor.AddBuilding(building);

            // ASSERT
            Assert.AreEqual(1, GetElementById(0).Count());
        }

        [Test]
        public void CanDeleteBuilding()
        {
            // ARRANGE
            var testBuilding = GetSomeBuildingFromTestData();
            var rectangle = GetMapRectangleFromWay(testBuilding);

            // ACT
            _tileEditor.DeleteBuilding(testBuilding.Id, rectangle);

            // ASSERT
            var buildings = GetElementById(testBuilding.Id).ToList();
            Assert.AreEqual(2, buildings.Count());
            Assert.IsTrue(buildings[0].Tags
                .ContainsKey(ActionStreetMap.Maps.Strings.DeletedElementTagKey));
        }

        #region Helpers

        private IEnumerable<Element> GetElementById(long id)
        {
            var boundingBox = _tileController.CurrentTile.BoundingBox;

            var elementSources = _elementSourceProvider.Get(boundingBox).ToList().Wait();

            return elementSources.SelectMany(es => es.Get(boundingBox, MapConsts.MaxZoomLevel)
                .ToArray().Wait().Where(e => e.Id == id));
        }

        private Way GetSomeBuildingFromTestData()
        {
            var boundingBox = _tileController.CurrentTile.BoundingBox;

            var elementSources = _elementSourceProvider.Get(boundingBox).ToList().Wait();

            return elementSources.SelectMany(es => es.Get(boundingBox, MapConsts.MaxZoomLevel)
                .ToArray().Wait().Where(e => e.Tags.ContainsKey("building")).OfType<Way>()).First();
        }

        private Rectangle2d GetMapRectangleFromWay(Way way)
        {
            var relativeNullPoint = _tileController.CurrentTile.RelativeNullPoint;
            var somePoint = GeoProjection.ToMapCoordinate(relativeNullPoint, way.Coordinates[0]);
            // NOTE change this code if it does matter
            return new Rectangle2d(somePoint.X, somePoint.Y, 10, 10);
        }

        private Building CreateBuilding()
        {
            return new Building()
            {
                Footprint = new List<Vector2d>()
                {
                    new Vector2d(0, 0),
                    new Vector2d(0, 50),
                    new Vector2d(50, 50),
                    new Vector2d(50, 0),
                }
            };
        }

        #endregion

    }
}
