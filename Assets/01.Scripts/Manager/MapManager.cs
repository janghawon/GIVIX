using GIVIX.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapGenerator))]
public class MapManager : MonoSingleton<MapManager>
{
    private MapGenerator _mapGenerator;
    private Dictionary<Vector3, MapTile> _mapTileDic = new();

    private Vector3 _firstPos;
    private Vector3 _lastPos;

    public MapTile LastGetTile { get; private set; }

    private void Awake()
    {
        _mapGenerator = GetComponent<MapGenerator>();   
    }

    public void ClearTileInfo()
    {
        _mapTileDic.Clear();
    }

    public void SetTileInfo(MapTile tileInfo)
    {
        if(_mapTileDic.Count == 0)
        {
            _firstPos = tileInfo.TileTransform.position;
        }

        _mapTileDic.Add(tileInfo.TileTransform.position, tileInfo);

        _lastPos = tileInfo.TileTransform.position;
    }

    public MapTile GetTileInfo(Vector3 pos)
    {
        Vector3 tilingPos = pos.RoundToIntVector();
        tilingPos = tilingPos.Clamp(_firstPos, _lastPos);

        LastGetTile = _mapTileDic[tilingPos];

        return _mapTileDic[tilingPos];
    }

    [ContextMenu("TestCreateMap")]
    public void TestCreateMap()
    {
        CreateMap(MapType.Desrt, 10, 3, 10, new Vector3(-5f, -1.5f, 0f));
    }

    public void CreateMap(MapType mapType, int deltaX, int deltaY, int deltaZ, Vector3 position)
    {
        _mapGenerator.MapSet((deltaX, deltaZ, deltaY));
        _mapGenerator.DrawMap(mapType, position);
    }
}
