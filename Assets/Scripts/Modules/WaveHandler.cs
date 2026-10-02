using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHandler : MonoBehaviour
{
    public GameObject zombiePrefab;
    public List<ZombieData> waveComposition;
    public List<float> waveChances;
    public int totalCount;

    private int _lastRow = -1;
    private int _spawnedCount;

    float GetSpawningInterval()
    {
        if (_spawnedCount < 4)
            return 4;

        if (_spawnedCount is > 4 and < 10)
            return 3;

        return 1.5f;
    }
    
    void Start()
    {
        SpawnZombie();   
        StartCoroutine(RunZombieSpawner());
    }

    IEnumerator RunZombieSpawner()
    {
        while (true)
        {
            if (GameState.Instance.Finished)
                break;
            
            yield return new WaitForSeconds(GetSpawningInterval());
                
            SpawnZombie();
            _spawnedCount++;

            if (_spawnedCount >= totalCount)
            {
                GameState.Instance.Win();
                break;
            }
        }
    }
    void SpawnZombie()
    {
        for (int i = 0; i < waveComposition.Count; i++)
        {
            if (Random.Range(0f, 1f) < waveChances[i])
            {
                var row = GameGrid.Instance.GetRandomRow();

                while (row == _lastRow)
                {
                    row = GameGrid.Instance.GetRandomRow();
                }
                _lastRow = row;
                
                var position = GameGrid.Instance.GetGlobalPosition(new Vector2Int(GameGrid.Instance.Size.x, row));

                position.y -= 0.3f;
                
                var instance = Instantiate(zombiePrefab, position, Quaternion.identity);
                
                instance.GetComponent<Zombie>().Init(waveComposition[i]);
                return;
            }
        }
    }
}
