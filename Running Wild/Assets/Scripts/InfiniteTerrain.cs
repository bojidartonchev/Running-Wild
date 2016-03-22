using System;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteTerrain : MonoBehaviour
{
    public GameObject PlayerObject;

    public int NumberOfForests;

    private List<GameObject> Forests;

    private Terrain[,] _terrainGrid = new Terrain[3, 3];

    void Start()
    {

        Terrain linkedTerrain = gameObject.GetComponent<Terrain>();

        _terrainGrid[0, 0] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
        _terrainGrid[0, 1] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
        _terrainGrid[0, 2] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
        _terrainGrid[1, 0] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
        _terrainGrid[1, 1] = linkedTerrain;
        _terrainGrid[1, 2] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
        _terrainGrid[2, 0] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
        _terrainGrid[2, 1] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();
        _terrainGrid[2, 2] = Terrain.CreateTerrainGameObject(linkedTerrain.terrainData).GetComponent<Terrain>();

        Forests = new List<GameObject>();
        Forests.Add(Instantiate(Resources.Load("Prefabs/Forest1", typeof(GameObject))) as GameObject);
        Forests.Add(Instantiate(Resources.Load("Prefabs/Forest2", typeof(GameObject))) as GameObject);
        Forests.Add(Instantiate(Resources.Load("Prefabs/Forest1", typeof(GameObject))) as GameObject);
        Forests.Add(Instantiate(Resources.Load("Prefabs/Forest2", typeof(GameObject))) as GameObject);

        UpdateTerrainPositionsAndNeighbors();
    }

    private void UpdateTerrainPositionsAndNeighbors()
    {
        _terrainGrid[0, 0].transform.position = new Vector3(
            _terrainGrid[1, 1].transform.position.x - _terrainGrid[1, 1].terrainData.size.x,
            _terrainGrid[1, 1].transform.position.y,
            _terrainGrid[1, 1].transform.position.z + _terrainGrid[1, 1].terrainData.size.z);
        _terrainGrid[0, 1].transform.position = new Vector3(
            _terrainGrid[1, 1].transform.position.x - _terrainGrid[1, 1].terrainData.size.x,
            _terrainGrid[1, 1].transform.position.y,
            _terrainGrid[1, 1].transform.position.z);
        _terrainGrid[0, 2].transform.position = new Vector3(
            _terrainGrid[1, 1].transform.position.x - _terrainGrid[1, 1].terrainData.size.x,
            _terrainGrid[1, 1].transform.position.y,
            _terrainGrid[1, 1].transform.position.z - _terrainGrid[1, 1].terrainData.size.z);

        _terrainGrid[1, 0].transform.position = new Vector3(
            _terrainGrid[1, 1].transform.position.x,
            _terrainGrid[1, 1].transform.position.y,
            _terrainGrid[1, 1].transform.position.z + _terrainGrid[1, 1].terrainData.size.z);
        _terrainGrid[1, 2].transform.position = new Vector3(
            _terrainGrid[1, 1].transform.position.x,
            _terrainGrid[1, 1].transform.position.y,
            _terrainGrid[1, 1].transform.position.z - _terrainGrid[1, 1].terrainData.size.z);

        _terrainGrid[2, 0].transform.position = new Vector3(
            _terrainGrid[1, 1].transform.position.x + _terrainGrid[1, 1].terrainData.size.x,
            _terrainGrid[1, 1].transform.position.y,
            _terrainGrid[1, 1].transform.position.z + _terrainGrid[1, 1].terrainData.size.z);
        _terrainGrid[2, 1].transform.position = new Vector3(
            _terrainGrid[1, 1].transform.position.x + _terrainGrid[1, 1].terrainData.size.x,
            _terrainGrid[1, 1].transform.position.y,
            _terrainGrid[1, 1].transform.position.z);
        _terrainGrid[2, 2].transform.position = new Vector3(
            _terrainGrid[1, 1].transform.position.x + _terrainGrid[1, 1].terrainData.size.x,
            _terrainGrid[1, 1].transform.position.y,
            _terrainGrid[1, 1].transform.position.z - _terrainGrid[1, 1].terrainData.size.z);

        _terrainGrid[0, 0].SetNeighbors(null, null, _terrainGrid[1, 0], _terrainGrid[0, 1]);
        _terrainGrid[0, 1].SetNeighbors(null, _terrainGrid[0, 0], _terrainGrid[1, 1], _terrainGrid[0, 2]);
        _terrainGrid[0, 2].SetNeighbors(null, _terrainGrid[0, 1], _terrainGrid[1, 2], null);
        _terrainGrid[1, 0].SetNeighbors(_terrainGrid[0, 0], null, _terrainGrid[2, 0], _terrainGrid[1, 1]);
        _terrainGrid[1, 1].SetNeighbors(_terrainGrid[0, 1], _terrainGrid[1, 0], _terrainGrid[2, 1], _terrainGrid[1, 2]);
        _terrainGrid[1, 2].SetNeighbors(_terrainGrid[0, 2], _terrainGrid[1, 1], _terrainGrid[2, 2], null);
        _terrainGrid[2, 0].SetNeighbors(_terrainGrid[1, 0], null, null, _terrainGrid[2, 1]);
        _terrainGrid[2, 1].SetNeighbors(_terrainGrid[1, 1], _terrainGrid[2, 0], null, _terrainGrid[2, 2]);
        _terrainGrid[2, 2].SetNeighbors(_terrainGrid[1, 2], _terrainGrid[2, 1], null, null);

        GenerateForests(NumberOfForests);


    }

    private void GenerateForests(int numberOfForests)
    {
        int[] parameters = GetRandomParams();
        for (int i = 0; i < numberOfForests; i++)
        {
            Instantiate(Forests[parameters[2]], new Vector3(parameters[0], 0, parameters[1]), Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0));
            parameters = GetRandomParams();
        }
    }

    private int[] GetRandomParams()
    {
        int[] toReturn = new int[3];
        toReturn[0] = UnityEngine.Random.Range((int)PlayerObject.transform.position.x - 400, (int)PlayerObject.transform.position.x + 400); //X limit
        toReturn[1] = UnityEngine.Random.Range((int)PlayerObject.transform.position.z + 200, (int)PlayerObject.transform.position.z + 1000); //Z Limit
        toReturn[2] = UnityEngine.Random.Range(0, 2); // Number of forest prefabs
        return toReturn;


    }

    void OnGUI()
    {
        Vector3 playerPosition = new Vector3(PlayerObject.transform.position.x, PlayerObject.transform.position.y, PlayerObject.transform.position.z);
        Terrain playerTerrain = null;
        int xOffset = 0;
        int yOffset = 0;
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if ((playerPosition.x >= _terrainGrid[x, y].transform.position.x) &&
                    (playerPosition.x <= (_terrainGrid[x, y].transform.position.x + _terrainGrid[x, y].terrainData.size.x)) &&
                    (playerPosition.z >= _terrainGrid[x, y].transform.position.z) &&
                    (playerPosition.z <= (_terrainGrid[x, y].transform.position.z + _terrainGrid[x, y].terrainData.size.z)))
                {
                    playerTerrain = _terrainGrid[x, y];
                    xOffset = 1 - x;
                    yOffset = 1 - y;
                    break;
                }
            }
            if (playerTerrain != null)
                break;
        }

        if (playerTerrain != _terrainGrid[1, 1])
        {
            Terrain[,] newTerrainGrid = new Terrain[3, 3];
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                {
                    int newX = x + xOffset;
                    if (newX < 0)
                        newX = 2;
                    else if (newX > 2)
                        newX = 0;
                    int newY = y + yOffset;
                    if (newY < 0)
                        newY = 2;
                    else if (newY > 2)
                        newY = 0;
                    newTerrainGrid[newX, newY] = _terrainGrid[x, y];
                }
            _terrainGrid = newTerrainGrid;
            UpdateTerrainPositionsAndNeighbors();
        }
    }
}