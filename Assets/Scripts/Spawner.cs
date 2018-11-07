using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    [SerializeField]
    private GameObject[] enemiesPrefab;//Vector with the prefabs of all the enemies

    private GameObject enemy;//Which enemy will be spawned

    [SerializeField]
    private float spawnTime; //Time between spawns

    private float xPosition;
    private float yPosition;

    //Animation curve used to modify the enemy's spawn probability at the given axis.
    // The X-axis of each curve is a normalized value 0-1. The Y-axis of each curve defines the spawn limits on that axis.
    public AnimationCurve enemyYAxis;
    public AnimationCurve enemyXAxis;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    //Function that decides which enemy to choose from
    GameObject Choose_Enemy()
    {
        //guarda o retorno para o objeto inimigo escolhido
        GameObject chosen;
        //gerando um numero aleatorio para escolher o inimigo
        int nbr = Random.Range(0, enemiesPrefab.Length);
      
        return chosen = enemiesPrefab[nbr];
    }


    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            //Need to choose which enemy to spawn
            enemy = Choose_Enemy();

            //Need to choose a position in which to spawn the enemy
            xPosition = enemyXAxis.Evaluate(Random.value);
            yPosition = enemyYAxis.Evaluate(Random.value);
      
            //With the enemy and its position the last thing to be done is spawn
            Instantiate(enemy, new Vector3(transform.position.x + xPosition, transform.position.y + yPosition, 0), transform.rotation);

            yield return new WaitForSeconds(spawnTime);

        }
    }
}
