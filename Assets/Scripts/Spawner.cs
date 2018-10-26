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

    public float xstartPosition;
    public float xendPosition;

    public float ystartPosition;
    public float yendPosition;

    private float xposition;
    private float yposition;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    //Function that decide which enemy to choose from
    GameObject Choose_Enemy()
    {
        //guarda o retorno para o objeto inimigo escolhido
        GameObject chosen;
        //gerando um numero aleatorio para escolher o inimigo
        int nbr = Random.Range(0, 2);
      
        return chosen = enemiesPrefab[nbr];
    }

    //Function that generate a coordinate
    float Choose_Coordinate(float startPosition, float endPosition)
    {
        float coordinate;
        //generate a coordinate given a function
        coordinate = Random.Range(startPosition, endPosition);

        return coordinate;
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            //Need to choose which enemy to spawn
            enemy = Choose_Enemy();
    
            //Need to choose a position in which to spawn the enemy
            xposition = Choose_Coordinate(xstartPosition, xendPosition);
            yposition = Choose_Coordinate(ystartPosition, yendPosition);
      
            //With the enemy and its position the last thing to be done is spawn
            Instantiate(enemy, new Vector3(xposition, yposition, 0), Quaternion.identity);

            yield return new WaitForSeconds(spawnTime);

        }
    }
}
