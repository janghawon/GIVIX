using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GIVIX.Map
{
    public enum MapType
    {
        Desrt,
        Cave,
        Ice,
        Lava,
        Grave,
        Fort
    }

    public class MapGenerator : MonoBehaviour
    {
        private MapDrawer[] _mapDrawerArr;
        private (int, int, int) _mapValueGroup;

        private void Awake()
        {
            _mapDrawerArr = GetComponentsInChildren<MapDrawer>();

            foreach(MapDrawer drawer in _mapDrawerArr)
            {
                drawer.TileGenerate();
            }
        }

        private void Start()
        {
            MapSet((10, 10, 2));
            DrawMap(MapType.Desrt);
        }

        public void MapSet((int, int, int) mapValueGroup)
        {
            _mapValueGroup = mapValueGroup;
        }

        public void DrawMap(MapType mapType)
        {
            MapDrawer drawer = _mapDrawerArr.FirstOrDefault(x => x.mapType == mapType);

            if(drawer == null)
            {
                Debug.LogError($"ERROR : {mapType}'s drawer has not exist.");
            }

            drawer.CreateMap(_mapValueGroup);
        }
    }
}
