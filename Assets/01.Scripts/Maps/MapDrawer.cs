using ExtensionMethods;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GIVIX.Map
{
    [System.Serializable]
    public class TilePrefabGroup
    {
        public Transform[] bottomTilePrefab;
        public Transform[] topTilePrefab;
        public Transform[] obstacleTilePrefab;
    }

    public class MapDrawer : MonoBehaviour
    {
        public MapType mapType;
        public TilePrefabGroup tilePrefabGroup = new TilePrefabGroup();
        protected MapTile[,,] _mapTileArr;
        protected MapTile[,] _obstacleArr;

        private Dictionary<MapTileType, MapTile> _mapTilesDic = new ();

        private Transform _mapTrm;

        private void Awake()
        {
            _mapTrm = GameObject.Find("MapTrm").transform;
        }

        public void TileGenerate()
        {
            _mapTilesDic.Clear();

            foreach (MapTileType mtt in Enum.GetValues(typeof(MapTileType)))
            {
                var mapTile = new MapTile();

                mapTile.TileType = mtt;
                _mapTilesDic.Add(mtt, mapTile);
            }
        }

        public void CreateMap((int, int, int) mapValueGroup)
        {
            _mapTileArr = new MapTile[mapValueGroup.Item1,
                                      mapValueGroup.Item2,
                                      mapValueGroup.Item3];

            _obstacleArr = new MapTile[mapValueGroup.Item1,
                                       mapValueGroup.Item2];

            for(var i = 0; i < _mapTileArr.GetLength(2); i++)
            {
                for(var j = 0; j < _mapTileArr.GetLength(1); j++)
                {
                    for(var k = 0; k < _mapTileArr.GetLength(0); k++)
                    {
                        MapTileType mtt;

                        if(i != _mapTileArr.GetLength(2) - 1)
                        {
                            mtt = MapTileType.Bottom;
                        }
                        else
                        {
                            mtt = MapTileType.Top;
                        }

                        _mapTileArr[k, j, i] = _mapTilesDic[mtt];
                        _mapTileArr[k, j, i].TileTransform = GetTileTransform(mtt, k, i, j);
                    }
                }
            }

            for(var i = 0; i < _obstacleArr.GetLength(0); i++)
            {
                for(var j = 0; j < _obstacleArr.GetLength(1); j++)
                {
                    if (Random.Range(0, 20) > 4) continue;

                    _obstacleArr[j, i] = _mapTilesDic[MapTileType.Obstacle];
                    _obstacleArr[j, i].TileTransform = 
                    GetTileTransform(MapTileType.Obstacle, j, mapValueGroup.Item3 - 1, i);
                }
            }
        }

        private Transform GetTileTransform(MapTileType mtt, int hor, int ver, int hei)
        {
            Transform[] tileTrmArr;
            Transform targetTrm;

            switch (mtt)
            {
                case MapTileType.Top:
                    tileTrmArr = tilePrefabGroup.topTilePrefab;
                    break;
                case MapTileType.Bottom:
                    tileTrmArr = tilePrefabGroup.bottomTilePrefab;
                    break;
                case MapTileType.Obstacle:
                    tileTrmArr = tilePrefabGroup.obstacleTilePrefab;
                    break;
                default:
                    tileTrmArr = null;
                    break;
            }

            targetTrm = tileTrmArr.GetRandomInstance();
            Instantiate(targetTrm, _mapTrm).position = new Vector3(hor, ver * 0.8f, hei);

            return targetTrm;
        }
    }
}
