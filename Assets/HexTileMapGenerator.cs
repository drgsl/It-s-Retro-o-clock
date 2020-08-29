using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTileMapGenerator : MonoBehaviour
{

    [SerializeField] int mapWidth = 25;
    [SerializeField] int mapHeight = 12;

    float tileXOffset = 1.8f;
    float tileZOffset = 1.565f;
    public GameObject hexTilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        CreateHexTileMap();
        transform.localScale = Vector3.one * 5;
        transform.position = new Vector3(-90f, transform.position.y, -80f);
    }

    void CreateHexTileMap()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int z = 0; z < mapHeight; z++)
            {
                GameObject tempGO = Instantiate(hexTilePrefab);

                if (z % 2 == 0)
                {
                    tempGO.transform.position = new Vector3(x * tileXOffset, transform.position.y , z * tileZOffset);
                }
                else
                {
                    tempGO.transform.position = new Vector3(x * tileXOffset + tileXOffset/2, transform.position.y, z * tileZOffset);
                }
                setTileInfo(tempGO, x, z);
            }
        }
    }

    void setTileInfo(GameObject GO, int x, int z)
    {
        GO.transform.parent = transform;
        GO.name = x.ToString("D2") + ", " + z.ToString("D2");
    }
}
