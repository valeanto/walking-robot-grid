using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
    public int w = 5;
    public int h = 5;
    public Tile tilePrefab;
    Tile[,] tiles;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new Tile[w, h];
        for (var j = 0; j < h; ++j)
        {
            for (var i = 0; i < w; ++i)
            {
                var tile = Instantiate(tilePrefab);
                var transform = tile.transform;
                transform.parent = transform;
                transform.localPosition = new Vector2(i, j);

                tile.x = i;
                tile.y = j;
                tiles[i, j] = tile;


            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

   
}
