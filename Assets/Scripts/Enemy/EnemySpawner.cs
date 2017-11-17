using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField]
    private GameObject[] enemy;
	[SerializeField]
	private GameObject room;
    [SerializeField]
	private float spawnDelay;
	[SerializeField]
	private int enemyQuantity = 2;
	private float lastSpawnTime;
	

    private Collider col;
	
    private GameObject GetEnemy()
    {
        lastSpawnTime = -spawnDelay;
        int index = Mathf.FloorToInt(Random.Range(0, enemy.Length));
        return enemy[index];
    }
    private void SpawnEnemy()
    {
        if(Time.time >= lastSpawnTime + spawnDelay)
        {
            Enemy[] currentChildren = GetComponentsInChildren<Enemy>();
            if(currentChildren.Length < 1)
            {
                //Debug.Log(-col.bounds.extents.x + " , " + col.bounds.extents.x);
                float spawnX = transform.position.x + Random.Range(-col.bounds.extents.x, col.bounds.extents.x);
                float spawnY = transform.position.y;
                float spawnZ = transform.position.z + Random.Range(-col.bounds.extents.z, col.bounds.extents.z);
                //Debug.Log(spawnX);
                GameObject enemy = (GameObject)Instantiate(GetEnemy());
                enemy.transform.SetParent(transform);
                enemy.transform.position = new Vector3(spawnX, spawnY, spawnZ);
                lastSpawnTime = Time.time;
                //Debug.Log("spawned");
            }
            
        }
        
    }
    private void Start () {
        col = GetComponent<Collider>();
        if(col == null)
        {
            Debug.Log("No collider attached");
        }
		if(enemy == null || enemy.Length == 0)
        {
            Debug.Log("No enemies attached");
        }
        else
        {
            SpawnEnemy();
        }
	}
	
	// Update is called once per frame
	private void Update () {
        SpawnEnemy();
        
    }

	public ColoriseRoom GetRoom()
	{
		return room.GetComponent<ColoriseRoom>();
	}
 

	public float GetColorRatio()
	{
		return 1f/enemyQuantity;
	}
}
