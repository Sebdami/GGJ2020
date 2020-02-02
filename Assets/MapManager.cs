using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    public Light ligth1;
    public Light ligth2;

    public float transistionTime = 2.5f;

    [SerializeField]
    public int NumberOfChunks = 15;

    [SerializeField]
    Vector2 chunkSize = new Vector2(200, 200);

    [SerializeField]
    Vector3 chunkDefaultRotation = new Vector3(0, 180, 0);

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
        ApplyChunkSettings(CurrentChunk.lightingSettings);
        source = CurrentChunk.lightingSettings;
    }

    void InstantiateFirst(Chunk prefab)
    {
        CurrentChunk = Instantiate(prefab, transform);
        CurrentChunk.transform.position = ChunkPositions[0];
        CurrentChunk.transform.eulerAngles = chunkDefaultRotation;
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
        ligth1.DOBlendableColor(CurrentChunk.light1, transistionTime);   
        ligth2.DOBlendableColor(CurrentChunk.light2, transistionTime);
        CoroutineManager.StartStaticCoroutine(DoBlend(CurrentChunk.lightingSettings, transistionTime));
        
        CurrentChunk.transform.position = ChunkPositions[CurrentChunkIndex];
        CurrentChunk.transform.eulerAngles = chunkDefaultRotation;

        if (CurrentChunk.test)
            CurrentChunk.test.gameObject.SetActive(false);
        toMove.DOMove(ChunkPositions[CurrentChunkIndex], transistionTime+0.1f).OnComplete(() =>
        {
            if (CurrentChunkIndex > 0)
                Destroy(lastChunk.gameObject);

            source = CurrentChunk.lightingSettings;

            callback?.Invoke();
        });

        AudioManager.Instance.PlaySFX(AudioManager.Instance.goToNextSound);
    }

    Chunk.LightingSettings source;

    public void ApplyChunkSettings(Chunk.LightingSettings light)
    {
        RenderSettings.ambientSkyColor = light.skyColor;
        RenderSettings.ambientEquatorColor = light.equatorColor;
        RenderSettings.ambientGroundColor = light.groundColor;
        RenderSettings.skybox.SetColor("_Tint", light.skyboxTint); 
    }

    private IEnumerator DoBlend(Chunk.LightingSettings targetData, float duration)
    {
        float t = 0;
        while (t < 1)
        {
            t = Mathf.Clamp01(t + Time.deltaTime / transistionTime);
            var _currentSettings = Blend(source, targetData, t);
            ApplyChunkSettings(_currentSettings);
            yield return null;
        }
    }

    public Chunk.LightingSettings Blend(Chunk.LightingSettings fromSettings, Chunk.LightingSettings toSettings, float t)
    {
        return new Chunk.LightingSettings(Color.Lerp(fromSettings.skyColor, toSettings.skyColor, t),
            Color.Lerp(fromSettings.equatorColor, toSettings.equatorColor, t),
            Color.Lerp(fromSettings.groundColor, toSettings.groundColor, t), Color.Lerp(fromSettings.skyboxTint, toSettings.skyboxTint, t));
    }

}
