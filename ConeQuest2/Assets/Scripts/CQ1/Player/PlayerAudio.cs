using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [Header("Land Sounds")]
    [SerializeField] private AudioClip defaultLandClip;
    [SerializeField] private AudioClip plasticLandClip;
    [SerializeField] private AudioClip metalLandClip;
    [SerializeField] private AudioClip glassLandClip;
    [SerializeField] private AudioClip iceLandClip;

    [Header("Step Sounds")]
    [SerializeField] private AudioClip[] defualtStepClips;
    [SerializeField] private AudioClip[] plasticStepClips;
    [SerializeField] private AudioClip[] metalStepClips;
    [SerializeField] private AudioClip[] glassStepClips;
    [SerializeField] private AudioClip[] iceStepClips;

    public AudioClip GetRandomStepClip(TerrainType terrainType)
    {
        switch(terrainType)
        {
            case TerrainType.PLASTIC:
                return plasticStepClips[Random.Range(0, plasticStepClips.Length)];
            case TerrainType.METAL:
                return metalStepClips[Random.Range(0, metalStepClips.Length)];
            case TerrainType.GLASS:
                return glassStepClips[Random.Range(0, glassStepClips.Length)];
            case TerrainType.ICE:
                return iceStepClips[Random.Range(0, iceStepClips.Length)];
            default:
                return defualtStepClips[Random.Range(0, defualtStepClips.Length)];
        }
    }

}
