using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject enemyContainer;
    [SerializeField]
    private GameObject[] powerUpPrefab;
    private bool isPlayerAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void startCoroutine()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerups());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(4f);
        while (isPlayerAlive)
        {
            Vector3 position = new Vector3(Random.Range(-9, 9), 6.37f, 0);
            GameObject newEnemy = Instantiate(enemyPrefab,position,Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform; 
            yield return new WaitForSeconds(5f);
        }
    }
    IEnumerator SpawnPowerups()
    {
        while (isPlayerAlive)
        {
            yield return new WaitForSeconds(4f);
            Vector3 position = new Vector3(Random.Range(-9.7f, 9.4f), 6.25f, 0);
            int randomPower = Random.Range(0, 3);
            Instantiate(powerUpPrefab[randomPower], position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(7f,13f));
        }
    }
    public void OnPlayerDeath()
    {
        isPlayerAlive = false;

    }
}
