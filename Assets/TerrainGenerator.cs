using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    public int depth = 20;
    public int height = 256;
    public int width = 256;
    public float scale = 20.0f;
    private float timer = 0.0f;
    private bool ascending = true;
    // Start is called before the first frame update
    void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }


private void Update() {
    if(ascending) {
        timer += Time.deltaTime;
    } else {
        timer -= Time.deltaTime;
    }
    changeScaleOverTime();
    Terrain terrain = GetComponent<Terrain>();
    terrain.terrainData = GenerateTerrain(terrain.terrainData);
}
    private TerrainData GenerateTerrain(TerrainData terrainData) {

        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);

        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    private float[,] GenerateHeights() {
        float[,] heights = new float[width, height];

        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                heights[x, y] = CalculateHeight(x, y);
                // heights[x, y] *= ;
            }
        }
        return heights;
    }

    private float CalculateHeight(int x, int y) {
        float xCoord = (float)x / width * scale;
        float yCoord = (float)y / height * scale;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }

    private void changeScaleOverTime() {
        scale = Mathf.Lerp(1.0f, 20.0f, timer / 20.0f);
        if(timer > 20.0f) {
            ascending = false;
            Debug.Log(timer);
        } else if(timer < 1.0f) {
            ascending = true;
        }
    }
}
