using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapReseter : MonoBehaviour
{
    [Serializable]
    public class TilemapState
    {
        public Tilemap tilemap;
        public Dictionary<Vector3Int, TileBase> initialTilemapState = new Dictionary<Vector3Int, TileBase>();
    }

    public List<TilemapState> tilemaps = new List<TilemapState>();

    public static TilemapReseter instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        // Tilemapsを初期化
        InitializeTilemaps();
    }

    private void InitializeTilemaps()
    {
        foreach (var tilemapState in tilemaps)
        {
            SaveInitialTilemapState(tilemapState);
        }
    }

    private void Start()
    {
        // 初期状態の保存はAwakeで行うため、Startでは特に行う処理はない
    }

    void SaveInitialTilemapState(TilemapState tilemapState)
    {
        tilemapState.initialTilemapState.Clear();
        foreach (var pos in tilemapState.tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            if (tilemapState.tilemap.HasTile(localPlace))
            {
                tilemapState.initialTilemapState[localPlace] = tilemapState.tilemap.GetTile(localPlace);
            }
        }
    }

    public void ResetTiles()
    {
        foreach (var tilemapState in tilemaps)
        {
            foreach (var item in tilemapState.initialTilemapState)
            {
                tilemapState.tilemap.SetTile(item.Key, item.Value);
            }
        }

    }
}