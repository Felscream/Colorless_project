using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    private static Inventory instance;
    private const int inventoryCapacity = 4;
    private List<WeaponItemData> arsenal = new List<WeaponItemData>();
    private WeaponItemData currentWeapon, latestWeapon;
    private List<ICapacityItem> capacities = new List<ICapacityItem>();
	private ICapacityItem currentCapacity;
    private int arsenalSize;
    private int currentArsenalIndex;
    private int lifeGem = 0;
    private Text lifeGemText;
    public static Inventory GetInstance()
    {
        if (instance == null)
        {
            Debug.Log("No instance of " + typeof(Inventory));
            return null;
        }
        return instance;
    }
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        arsenalSize = arsenal.Count;
        currentArsenalIndex = 0;
        if (arsenalSize > 0)
        {
            currentWeapon = arsenal[currentArsenalIndex];
            latestWeapon = currentWeapon;
        }
    }

    public void Start()
    {

		lifeGemText = GameObject.FindGameObjectWithTag("LifeGem").GetComponent<Text>();
        lifeGemText.text = string.Format("{0:0,0.0}", lifeGem);
        lifeGemText.enabled = true;
    }

    public void Update()
    {
		//Temporaire
		if (capacities.Count != 0)
		{
			currentCapacity = capacities[0];
			//currentCapacity.DoEffect();
		}
		//
		UpdateLifeGemText();
    }

    private void UpdateLifeGemText()
    {
		
		lifeGemText.text = string.Format("{0:0,0}", lifeGem); 
    }
    private bool LatestWeaponDifferentFromWeapon(WeaponItemData weapon)
    {
        if (latestWeapon != weapon)
            return true;
        return false;
    }
    public bool AlreadyInInventory(WeaponItemData weaponData)
    {
        foreach(WeaponItemData w in arsenal)
        {
            if(w.GetItemName() == weaponData.GetItemName())
            {
                Debug.Log("Item Already In inventory");
                return true;
            }
        }
        return false;
    }
    public WeaponItemData AddWeapon(WeaponItemData weaponToAdd)
    {
        if(arsenalSize == inventoryCapacity)
        {
            Debug.Log("Inventory is full");
        }
        else if(!AlreadyInInventory(weaponToAdd))
        {
            arsenal.Add(weaponToAdd);
            arsenalSize = arsenal.Count;
            if (arsenalSize == 1)
            {
                currentWeapon = arsenal[0];
                latestWeapon = currentWeapon;
                return currentWeapon;
            } 
        }
        return null;
        
    }

    public void RemoveWeapon(WeaponItemData weaponToRemove)
    {
        //ajouter traitement au cass où on enlèverait l'arme courante ou récemment utilisée
        //

        if (arsenalSize == 0)
        {
            Debug.Log("Nothing to remove");
        }
        else
        {
            arsenal.Remove(weaponToRemove);
        }
        arsenalSize = arsenal.Count;
        
        
    }


    public WeaponItemData SelectNextWeapon()
    {

        currentArsenalIndex = (currentArsenalIndex + 1) % arsenal.Count;
        latestWeapon = currentWeapon;
        currentWeapon = arsenal[currentArsenalIndex];
        
        return currentWeapon;
    }

    public WeaponItemData SelectPreviousWeapon()
    {

        currentArsenalIndex = currentArsenalIndex -1 < 0 ? arsenal.Count -1 : currentArsenalIndex -1;
        latestWeapon = currentWeapon;
        currentWeapon = arsenal[currentArsenalIndex];
        return currentWeapon;
    }

    public WeaponItemData FindWeaponDataByName(string name)
    {

        for (int i = 0; i < arsenalSize; i++)
        {
            if (arsenal[i].GetItemName() == name)
            {
                return arsenal[i];
            }
        }
        Debug.Log("Weapon not found");
        
        return arsenal[currentArsenalIndex];
    }

    public WeaponItemData SelectWeaponSlot(int slot)
    {
        Debug.Log(slot);
        if(slot < arsenalSize)
        {
            if (arsenal[slot] != null && arsenal[slot] != currentWeapon)
            {
                if (LatestWeaponDifferentFromWeapon(arsenal[slot]))
                {
                    latestWeapon = currentWeapon;
                }
                currentWeapon = arsenal[slot];
                currentArsenalIndex = slot;
                return currentWeapon;
            }
            else
            {
                Debug.Log("Weapon already equipped");
            }
        }
        Debug.Log("Weapon not found");
        return currentWeapon;
    }

    public WeaponItemData SelectLatestWeapon()
    {
        WeaponItemData temp = currentWeapon;
        //verify if we had a previous weapon
        if (LatestWeaponDifferentFromWeapon(currentWeapon))
        {

            currentWeapon = latestWeapon;
            latestWeapon = temp;
            for (int i = 0; i < arsenalSize; i++)
            {
                if (arsenal[i] == currentWeapon)
                {
                    currentArsenalIndex = i;
                    break;
                }
            }
        }
        else //select next weapon
        {
            currentWeapon = SelectNextWeapon();
            latestWeapon = temp;
        }
        return currentWeapon;
    }
    public int GetArsenalSize()
    {
        arsenalSize = arsenal.Count;
        return arsenalSize;
    }

    public bool ArsenalNotEmpty()
    {
        if(arsenal.Count > 0)
        {
            return true;
        }
        return false;
    }

    public WeaponItemData GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public void CollectLifeGem(int amount)
    {
        lifeGem += amount;
    }
	
	public void AddCapacity(ICapacityItem capacityItem)
	{
		capacities.Add(capacityItem);
	}

	public int DeleteCapacity(string id)
	{
		return capacities.RemoveAll(x => (x.GetId() == id));
	}

	public ICapacityItem GetCapacity(string id)
	{
		return capacities.Find(x => (x.GetId() == id));
	}

	
    public int GetLifeGem()
    {
        return lifeGem;
    }

	public void DoCapacity()
	{
		currentCapacity.DoEffect();
	}
}
