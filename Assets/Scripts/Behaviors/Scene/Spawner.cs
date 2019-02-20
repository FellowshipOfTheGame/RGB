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


    private float spawnPosition;

    //Animation curve used to modify the enemy's spawn probability at the given axis.
    // The X-axis of each curve is a normalized value 0-1. The Y-axis of each curve is a value between -1 and 1 (inclusive).
    public AnimationCurve[] spawningCurves;

    //There will be 'n' curves n = spawningCurves.lenght. The designer will have to choose which curve he wants.
    public int wantedCurve;

    //After getting a random value from the spawnCurve, we will have to multiply that value between -1 and 1 by the amplitude,
    //that way the enemy will spawn inside an desirable region
    public float amplitude;

    private void OnEnable()
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
        while (enabled)
        {
            //Need to choose which enemy to spawn
            enemy = Choose_Enemy();

            //getting a value from the curve
            float evaluatedPosition = spawningCurves[wantedCurve].Evaluate(Random.value);

            //Need to choose a position in which to spawn the enemy
            spawnPosition =  evaluatedPosition * amplitude;
      
            //With the enemy and its position the last thing to be done is spawn
            Instantiate(enemy, new Vector3(transform.position.x + spawnPosition, transform.position.y, 0), transform.rotation, transform);

            yield return new WaitForSeconds(spawnTime);

        }
    }
}
