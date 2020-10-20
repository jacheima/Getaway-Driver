using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [Tooltip("This is the number of tiles that are between this tile and where the new tile should spawn")]
    [SerializeField] private int offset;
    [SerializeField] private Tiles tiles;

    [HideInInspector]
    public GameObject tileParent;

    public bool isSpawned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            if(!isSpawned)
            {
                GameObject newTile = Instantiate(tiles.tilePrefabs[Random.Range(0, tiles.tilePrefabs.Count)], transform.position, transform.rotation);

                Vector3 newTilePosition = newTile.transform.position;
                newTilePosition.z += (offset * 46.25f);
                newTile.transform.position = newTilePosition;

                GameManager.instance.spawnedTiles.Add(newTile);

                newTile.GetComponent<TileSpawner>().tileParent = this.gameObject;

                if(tileParent != null && tileParent.tag != "Starting Tile")
                {
                    Destroy(tileParent);
                }
                

                isSpawned = true;
            }
             
        }
    }
}
