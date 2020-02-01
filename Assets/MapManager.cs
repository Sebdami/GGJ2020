using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    [SerializeField]
    List<Chunk> chunkPrefabs = new List<Chunk>();

    [SerializeField]
    public int NumberOfChunks = 15;

    [SerializeField]
    Vector2 chunkSize = new Vector2(200, 200);

    public List<Chunk> Chunks = new List<Chunk>();
    public int CurrentChunk = 0;

    public void GenerateChunks()
    {
        for (int i = 0; i < transform.childCount; ++i)
            Destroy(transform.GetChild(i).gameObject);
        Chunks = new List<Chunk>();
        Vector3 lastPosition = Vector3.zero;
        for(int i = 0; i < NumberOfChunks; ++i)
        {
            Chunks.Add(Instantiate(chunkPrefabs[Random.Range(0, chunkPrefabs.Count - 1)], transform));
            Chunks[i].transform.localPosition = lastPosition + (Random.Range(0, 2) == 1 ? Vector3.right * chunkSize.x : Vector3.forward * chunkSize.y);
            lastPosition = Chunks[i].transform.localPosition;
        }
    }

    public void Init()
    {
        GenerateChunks();
        CurrentChunk = 0;
    }

    public Vector3 GetNextPlayerTargetPosition()
    {
        if (CurrentChunk >= NumberOfChunks - 1)
            return Vector3.zero;

        return Chunks[CurrentChunk + 1].playerTargetTransform.position;
    }

    public Vector3 GetPlayerTargetPosition()
    {
        if (CurrentChunk >= NumberOfChunks - 1)
            return Vector3.zero;

        return Chunks[CurrentChunk].playerTargetTransform.position;
    }

    public Vector3 GetPrefabTargetPosition()
    {
        if (CurrentChunk >= NumberOfChunks - 1)
            return Vector3.zero;

        return Chunks[CurrentChunk].prefabSpawnTransform.position;
    }

    public Transform GetCurrentChuckTransform()
    {
        return Chunks[CurrentChunk].transform;
    }

    public Vector3 GetNextPrefabTargetPosition()
    {
        if (CurrentChunk >= NumberOfChunks - 1)
            return Vector3.zero;

        return Chunks[CurrentChunk + 1].prefabSpawnTransform.position;
    }

    public void GoToNextChunk(Transform toMove, TweenCallback callback)
    {
        CurrentChunk++;
        toMove.DOMove(Chunks[CurrentChunk].playerTargetTransform.position, 3f).OnComplete(callback);
    }
}
