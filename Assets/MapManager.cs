using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using RenderSettings = UnityEngine.RenderSettings;

public class MapManager : Singleton<MapManager>
{
    public DirectionalLight ligth1;
    public DirectionalLight ligth2;

    [SerializeField]
    public int NumberOfChunks = 15;

    [SerializeField]
    Vector2 chunkSize = new Vector2(200, 200);

    public List<Vector3> ChunkPositions = new List<Vector3>();
    public int CurrentChunkIndex = 0;

    public Chunk CurrentChunk;
    Chunk lastChunk;

    public void GenerateChunkPositions()
    {
        for (int i = 0; i < transform.childCount; ++i)
            Destroy(transform.GetChild(i).gameObject);
        ChunkPositions = new List<Vector3>();
        Vector3 lastPosition = Vector3.zero;
        ChunkPositions.Add(lastPosition);
        for (int i = 1; i < NumberOfChunks; ++i)
        {
            ChunkPositions.Add(lastPosition + (Random.Range(0, 2) == 1 ? Vector3.right * chunkSize.x : Vector3.forward * chunkSize.y));
            lastPosition = ChunkPositions[i];
        }
    }

    public void Init(GameplayEvent gevent)
    {
        GenerateChunkPositions();
        CurrentChunkIndex = 0;
        InstantiateFirst(gevent.mapPrefab);
        ApplyChunkSettings(CurrentChunk);
    }

    void InstantiateFirst(Chunk prefab)
    {
        CurrentChunk = Instantiate(prefab, transform);
        CurrentChunk.transform.position = ChunkPositions[0];
    }

    public Vector3 GetNextPlayerTargetPosition()
    {
        if (CurrentChunkIndex >= NumberOfChunks - 1)
            return Vector3.zero;

        return ChunkPositions[CurrentChunkIndex + 1];
    }

    public Vector3 GetPlayerTargetPosition()
    {
        if (CurrentChunkIndex >= NumberOfChunks - 1)
            return Vector3.zero;

        return ChunkPositions[CurrentChunkIndex];
    }

    public Vector3 GetPrefabTargetPosition()
    {
        if (CurrentChunkIndex >= NumberOfChunks - 1)
            return Vector3.zero;

        return ChunkPositions[CurrentChunkIndex];
    }

    public Vector3 GetNextPrefabTargetPosition()
    {
        if (CurrentChunkIndex >= NumberOfChunks - 1)
            return Vector3.zero;

        return ChunkPositions[CurrentChunkIndex + 1];
    }

    public void GoToNextChunk(Transform toMove, Chunk chunkPrefab, TweenCallback callback)
    {
        CurrentChunkIndex++;

        // @seb j'ai rajouté par ce que sinon quand on arrive a NumberOfChunks sa plante
        if (CurrentChunkIndex > NumberOfChunks -1)
        {
            NumberOfChunks += 3;
            var lastPos = ChunkPositions[CurrentChunkIndex - 1];
            for (int i = CurrentChunkIndex; i < NumberOfChunks; ++i)
            {
                ChunkPositions.Add(lastPos + (Random.Range(0, 2) == 1 ? Vector3.right * chunkSize.x : Vector3.forward * chunkSize.y));
                lastPos = ChunkPositions[i];
            }
        }
        lastChunk = CurrentChunk;
        CurrentChunk = Instantiate(chunkPrefab, transform);
        CurrentChunk.transform.position = ChunkPositions[CurrentChunkIndex];
        toMove.DOMove(ChunkPositions[CurrentChunkIndex], 3f).OnComplete(() =>
        {
            if (CurrentChunkIndex > 0)
                Destroy(lastChunk.gameObject);
            ApplyChunkSettings(CurrentChunk);
            
            callback?.Invoke();
        });
    }

    public void ApplyChunkSettings(Chunk chunk)
    {
        RenderSettings.ambientSkyColor = chunk.lightingSettings.skyColor;
        RenderSettings.ambientEquatorColor = chunk.lightingSettings.equatorColor;
        RenderSettings.ambientGroundColor = chunk.lightingSettings.groundColor;
        RenderSettings.skybox.SetColor("_Tint", chunk.lightingSettings.skyboxTint);
    }
}
