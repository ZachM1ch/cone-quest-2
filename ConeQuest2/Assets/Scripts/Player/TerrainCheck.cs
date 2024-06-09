using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TerrainType
{
    DEFAULT = 0,
    PLASTIC,
    METAL,
    GLASS,
    ICE,
    ORGANIC,
    SQUISHY,
    COUNT
}

public class TerrainCheck : MonoBehaviour
{
    [SerializeField] private Transform groundTransform;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float terrainRayLength = 1.0f;
    [SerializeField] private Vector3 terrainRayPosOffset = Vector3.zero;
    
    public TerrainType currTerrainType = TerrainType.DEFAULT;

    [Header("Debug")]
    [SerializeField] private bool castTerrainRay = true;

    /// <summary>
    /// Cast a physics ray downwards. If the ray hits, check the object's TerrainType
    /// </summary>
    public void CheckTerrainRay()
    {
        Vector3 terrainRayPos = groundTransform.position + terrainRayPosOffset;
        RaycastHit terrainRayHit;

        if(Physics.Raycast(terrainRayPos, Vector3.down, out terrainRayHit, terrainRayLength, layerMask))
        {
             CheckTerrain(terrainRayHit.collider.gameObject);
        }
        else
        {
            // Raycast did not hit an object
            currTerrainType = TerrainType.DEFAULT;
        }

        if(castTerrainRay)
        {
            Debug.DrawLine(groundTransform.position, terrainRayHit.point, Color.yellow);
        }
    }

    private void CheckTerrain(GameObject _gameObject)
    {
        string objectTag = _gameObject.tag;
        TerrainType terrainType = TerrainType.DEFAULT;
        bool foundMatch = false;

        // Start loop at 1 because we want to skip "DEFAULT" type
        for(int i = 1; i < (int)TerrainType.COUNT; i++)
        {
            if(objectTag.ToUpper() == Enum.GetName(typeof(TerrainType), i))
            {
                foundMatch = true;
                terrainType = (TerrainType)i;
            }
        }

        if(!foundMatch)
        {
            terrainType = TerrainType.DEFAULT;
        }

        currTerrainType = terrainType;
    }
}
