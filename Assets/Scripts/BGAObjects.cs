using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGAObjects : MonoBehaviour
{
    [SerializeField]
    private SpriteMask cityMask;

    public int maskFrequency;

    public float amplitudeX;
    public float amplitudeY;

    public void OnLoop()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        
        spawnMask(cityMask, maskFrequency);
    }

    private void spawnObject(GameObject obj, int frequency)
    {
        int num = Random.Range(0, frequency + 1);
        for (int i=0; i<num; i++)
        {
            float spawnPointX = Random.Range(-1f, 1f) * amplitudeX;
            float spawnPointY = Random.Range(-1f, 1f) * amplitudeY;
            Vector3 spawnVector = new Vector3(transform.position.x + spawnPointX, transform.position.y + spawnPointY, 0);
            Instantiate(obj, spawnVector, Quaternion.identity, transform); 
        }
    }

    private void spawnMask(SpriteMask mask, int frequency)
    {
        int num = Random.Range(1, frequency + 1);
        for (int i = 0; i < num; i++)
        {
            float spawnPointX = Random.Range(-1f, 1f) * amplitudeX;
            float spawnPointY = Random.Range(-1f, 1f) * amplitudeY;
            Vector3 spawnVector = new Vector3(transform.position.x + spawnPointX, transform.position.y + spawnPointY, 0);
            Instantiate(mask, spawnVector, Quaternion.identity, transform);
        }
    }
}
