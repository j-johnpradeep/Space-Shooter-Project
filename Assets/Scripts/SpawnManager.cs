using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnEnemy;
    [SerializeField]
    private GameObject[] powerUps;
    [SerializeField]
    private GameObject Enemy_Container;
    private bool _stopToSpawn = false;
    private void Start()
    {
        
    }
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2.75f);
        while (_stopToSpawn == false)
        {
            Debug.Log("Spawnenemy");
            Vector3 spawnPos = new Vector3(Random.Range(-8.5f,8.5f),7.6f,0);
            GameObject newEnemy = Instantiate(spawnEnemy,spawnPos,Quaternion.identity);
            newEnemy.transform.parent = Enemy_Container.transform;
            yield return new WaitForSeconds(4);
        }
        
    }
    IEnumerator SpawnPowerRoutine()
    {
        yield return new WaitForSeconds(2.75f);
        while (_stopToSpawn == false)
        {
            Vector3 spawnPos = new(Random.Range(-9.5f, 9.5f), 7.6f, 0);
            Instantiate(powerUps[Random.Range(0,powerUps.Length)], spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3,7));
        }
    }

    public void OnPlayerDeath()
    {
        _stopToSpawn = true;
        Destroy(Enemy_Container);
    }
}
