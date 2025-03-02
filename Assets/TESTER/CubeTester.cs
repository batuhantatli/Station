using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeTester : Singleton<CubeTester>
{
    public SpawnedCube cube;
    public List<SpawnedCube> spawnedCubes = new List<SpawnedCube>();
    public float radius;
    public int x;
    public SpawnedCube mostCube;
    public SpawnedCube CubeSpawned(SpawnedCube currentCube,int tt)
    {
        x++;
        Vector3 currentTransform = currentCube.transform.position;
        var g = Instantiate(cube, currentTransform, quaternion.identity);
        var vector = Random.insideUnitCircle.normalized * radius;
        g.MoveNextPoint(currentCube.transform.position + new Vector3(vector.x,tt,vector.y));
        return g;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log(Random.insideUnitCircle*radius);
            // CubeSpawned();
        }
    }

    public Vector3 CalculateNextCubePoint(Transform currentCube)
    {
        Vector2 randomPosition2 = Random.insideUnitCircle * radius;
        Vector2 randomPosition = randomPosition2.normalized * radius;
        return new Vector3(randomPosition.x, x, randomPosition.y);
    }
    
}
