using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Core;

public class AIManager : MonoBehaviour {
    [SerializeField]
    private int numberEnemiesClose;
    [SerializeField]
    private float UpdateCloseRangeInterval;
    private float lastCloseRangeUpdate = 0.0f;
    private AIRig aiRig;
    public List<AIRig> closeList;
    public List<GameObject> enemiesList;
    // Use this for initialization
    void Start () {
        aiRig = GetComponentInChildren<AIRig>();
        aiRig.AI.WorkingMemory.SetItem<List<AIRig>>("closeEnemies", closeList);
        closeList = aiRig.AI.WorkingMemory.GetItem<List<AIRig>>("closeEnemies");
    }
	
	// Update is called once per frame
	void Update () {
        CanUpdateCloseRange();
    }

    private void CanUpdateCloseRange()
    {
        if(Time.time - lastCloseRangeUpdate < UpdateCloseRangeInterval)
        {
            SelectRandomCloseRange();
            lastCloseRangeUpdate = Time.time;
        }
    }
    public void AddEnemy(GameObject e)
    {
        enemiesList.Add(e);  
    }

    public void RemoveEnemy(GameObject e)
    {
        if (enemiesList.Contains(e))
        {
            enemiesList.Remove(e);
            AIRig aiR = e.GetComponent<AIRig>();
            if (closeList.Contains(aiR))
            {
                closeList.Remove(aiR);
            }
        }
    }

    public void SelectRandomCloseRange()
    {
        if(closeList.Count < numberEnemiesClose)
        {
            int emptyCells = numberEnemiesClose - closeList.Count;
            List<GameObject> temp = enemiesList;
            int closeListCount = closeList.Count;
            int ceil = Mathf.Min(numberEnemiesClose, temp.Count);
            for (int i = closeListCount; i < ceil; i++)
            {
                int rand = Random.Range(0, enemiesList.Count);
                Debug.Log("Rand : " + rand);
                AIRig aiToAdd = temp[rand].GetComponentInChildren<AIRig>();
                if (!closeList.Contains(aiToAdd))
                {
                    closeList.Add(enemiesList[rand].GetComponentInChildren<AIRig>());
                    closeList[i].AI.WorkingMemory.SetItem<bool>("CloseRange", true);
                }
                temp.Remove(temp[rand]);
            }
        }
    }
}
