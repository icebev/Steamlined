using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    public GameObject[] terrainTile;
    public GameObject[] tileDecoration;
    public const int TILE_LENGTH = 40;
    public int xPos = 35;
    public bool creatingSection = false;
    public int secNum;
    public Rigidbody playerBody;
    public System.Random rnd = new System.Random();

    // Update is called once per frame
    void Update()
    {
        if (!this.creatingSection && (this.xPos - this.playerBody.transform.position.x < 500))
        {
            this.creatingSection = true;

            StartCoroutine(GenerateTerrainTile(this.rnd.Next(0, this.terrainTile.Length)));
        }
    }

    IEnumerator GenerateTerrainTile(int terrainTileID)
    {
        this.secNum = terrainTileID;
        Instantiate(this.terrainTile[this.secNum], new Vector3(this.xPos, 0, 0), Quaternion.identity);
        Instantiate(this.tileDecoration[0], new Vector3(this.xPos + 0.2f, -0.55f, 0), Quaternion.identity);
        this.xPos += TILE_LENGTH;
        yield return null;
        this.creatingSection = false;
    }
}
