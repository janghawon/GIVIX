using DG.Tweening;
using ExtensionMethods;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
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
        [SerializeField] private float _tileSpawnUpperValue;

        public MapType mapType;
        public TilePrefabGroup tilePrefabGroup = new TilePrefabGroup();
        protected MapTile[,,] _mapTileArr;
        protected MapTile[,] _obstacleArr;

        private Dictionary<MapTileType, MapTile> _mapTilesDic = new ();

        private Transform _mapTrm;
        [SerializeField] private Transform _mapFloorTrm;

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

        public void CreateMap((int, int, int) mapValueGroup, Vector3 pos)
        {
            _mapTileArr = new MapTile[mapValueGroup.Item1,
                                      mapValueGroup.Item2,
                                      mapValueGroup.Item3];

            _obstacleArr = new MapTile[mapValueGroup.Item1,
                                       mapValueGroup.Item2];

            StartCoroutine(CreateTileCO(mapValueGroup, pos));
        }

        private IEnumerator CreateTileCO((int, int, int) mapValueGroup, Vector3 pos)
        {
            for (var i = 0; i < _mapTileArr.GetLength(2); i++)
            {
                for (var j = 0; j < _mapTileArr.GetLength(1); j++)
                {
                    for (var k = 0; k < _mapTileArr.GetLength(0); k++)
                    {
                        MapTileType mtt;

                        if (i != _mapTileArr.GetLength(2) - 1)
                        {
                            mtt = MapTileType.Bottom;
                        }
                        else
                        {
                            mtt = MapTileType.Top;
                        }

                        _mapTileArr[k, j, i] = _mapTilesDic[mtt];
                        _mapTileArr[k, j, i].TileTransform = GetTileTransform(mtt, k, i, j, pos);

                        MapManager.Instance.SetTileInfo(_mapTileArr[k, j, i]);
                        _mapTileArr[k, j, i].TileTransform.DOMoveY(i * 0.8f + pos.y, 0.4f).SetEase(Ease.InQuart);
                    }
                }

                yield return new WaitForSeconds(0.4f);

                PoolableParticle fieldDust = PoolManager.Instance.Pop("FieldDust") as PoolableParticle;
                fieldDust.transform.position = new Vector3(pos.x - 0.5f, (i-1) * 0.8f + pos.y, pos.z - 0.5f);
                Debug.Log(fieldDust);
                fieldDust.Play();
            }

            for (var i = 0; i < _obstacleArr.GetLength(0); i++)
            {
                for (var j = 0; j < _obstacleArr.GetLength(1); j++)
                {
                    if (Random.Range(0, 20) > 4) continue;

                    _obstacleArr[j, i] = _mapTilesDic[MapTileType.Obstacle];
                    _obstacleArr[j, i].TileTransform =
                    GetTileTransform(MapTileType.Obstacle, j, mapValueGroup.Item3 - 1, i, pos);

                    _obstacleArr[j, i].TileTransform.DOMoveY(_obstacleArr[j, i].TileTransform.position.y - _tileSpawnUpperValue, 0.4f).SetEase(Ease.InExpo);
                }
            }
        }

        private Transform GetTileTransform(MapTileType mtt, int hor, int ver, int hei, Vector3 pos)
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

            targetTrm = Instantiate(tileTrmArr.GetRandomInstance(), _mapTrm);
            targetTrm.position = new Vector3(hor, ver * 0.8f, hei) + pos + new Vector3(0, _tileSpawnUpperValue, 0);

            return targetTrm;
        }
    }
}
