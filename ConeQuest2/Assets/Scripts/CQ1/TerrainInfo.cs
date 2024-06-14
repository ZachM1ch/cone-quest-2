using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainInfo : MonoBehaviour
{
    [SerializeField] private TerrainType terrainType = TerrainType.DEFAULT;

    public TerrainType GetTerrainType()
    {
        return terrainType;
    }
}
