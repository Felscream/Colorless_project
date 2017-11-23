using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeGemSpawn : MonoBehaviour {
    [SerializeField]
    private string LifeGemPrefabPath;
    [SerializeField]
    private int minLifeGems, maxLifeGems, minLifeGemValue, maxLifeGemValue, angleToLaunch;
    [SerializeField]
    private float minLaunchX, maxLaunchX, minLaunchY, maxLaunchY, minLaunchZ, maxLaunchZ;
    // Use this for initialization
    public void SpawnLifeGem()
    {
        int lifeGemNumber = Random.Range(minLifeGems, maxLifeGems+1);
        int lifeGemAmount;
        GameObject lifeGem = (GameObject)Resources.Load(LifeGemPrefabPath);
        for(int i = 0; i < lifeGemNumber; i++)
        {
            lifeGemAmount = Random.Range(minLifeGemValue, maxLifeGemValue);
            GameObject temp = Instantiate(lifeGem);
            temp.GetComponent<LifeGemItem>().Amount = lifeGemAmount;
            temp.transform.position = transform.position;
            temp.transform.parent = null;
            Vector3 Launch = new Vector3(Random.Range(minLaunchX, maxLaunchX), Random.Range(minLaunchY, maxLaunchY), Random.Range(minLaunchZ, maxLaunchZ));
            temp.GetComponent<Rigidbody>().AddForce(Launch, ForceMode.Impulse);
        }
    }
}
