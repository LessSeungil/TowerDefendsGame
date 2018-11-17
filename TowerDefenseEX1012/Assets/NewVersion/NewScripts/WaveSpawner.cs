using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

	public static int EnemiesAlive = 0;

	public Wave[] waves;

	public Transform spawnPoint;

	public float timeBetweenWaves;
	private float countdown = 5f;


	public GameManager gameManager;

	private int waveIndex = 0;

    void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }

        if (waveIndex == waves.Length)      // wave가 0이 되면 클리어
        {
            gameManager.WinLevel();
            this.enabled = false;
        }

        

    }

    public void StartWave()
    {
        if (EnemiesAlive == 0)
        {
            StartCoroutine(SpawnWave());
        }
        else
        {
            Debug.Log("waveplaying");
        }
        
    }


	IEnumerator SpawnWave ()
	{
		PlayerStats.Rounds++;

		Wave wave = waves[waveIndex];

		EnemiesAlive = wave.count;

		for (int i = 0; i < wave.count; i++)
		{
			SpawnEnemy(wave.enemy);
			yield return new WaitForSeconds(1f);
		}

		waveIndex++;
	}

	void SpawnEnemy (GameObject enemy)
	{
		Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
	}

}
