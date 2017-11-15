using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {
    private static PlayerActions instance;
    [SerializeField]
    private float weaponSwapDelay;
    private Weapon weapon;
    private WaitForSeconds weaponSwapDelaySeconds;
    private Transform weaponLocation, camTransform;
    private InteractionDetector intDetector;
    private Inventory inventory;
    private GameObject equippedWeapon;
    private Coroutine reloadCoroutine;
    private bool ChangeWeaponAxisInUse = false;
    private bool firing;

    public static PlayerActions GetInstance()
    {
        if (instance == null)
        {
            Debug.Log("No instance of " + typeof(PlayerActions));
            return null;
        }
        return instance;

    }
    private void Awake()
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
        weaponLocation = transform.Find("Camera").Find("WeaponLocation");
        if (weaponLocation == null)
        {
            Debug.Log("WeaponLocation not found");
        }
        camTransform = Camera.main.transform;
        weapon = GetComponentInChildren<Weapon>();
    }

    private void Start()
    {
        inventory = Inventory.GetInstance();
        intDetector = InteractionDetector.GetInstance();
        weaponSwapDelaySeconds = new WaitForSeconds(weaponSwapDelay);
        firing = false;
    }

    private void Update()
    {
        if(weapon != null && weapon is HitScan)
        {
            HitScan temp = (HitScan)weapon;
            if (!firing)
            {

                temp.DecrementBulletSpread();
            }
            else
            {
                
                temp.IncrementBulletSpread();
            }
           // Debug.Log(firing);
        }
    }

    private bool HasInventory()
    {
        if (inventory != null)
        {
            return true;
        }
        Debug.Log("No inventory found");
        return false;
    }



    private bool CanChangeWeapon()
    {
        if (inventory.ArsenalNotEmpty() && !ChangeWeaponAxisInUse)
            return true;
        return false;
    }

    public bool HasWeapon()
    {
        if (weapon != null)
            return true;
        return false;
    }

    public IEnumerator ChangeWeapon(int input)
    {
        WeaponItemData weaponItemData = null;
        //check if changing weapon is possible (more than 1 weapon in inventory)
        if (HasInventory() && input != 0 && inventory.GetArsenalSize() > 1 && CanChangeWeapon())
        {
            ChangeWeaponAxisInUse = true;
            Debug.Log(input);
            if (input < 0)
            {
                weaponItemData = inventory.SelectNextWeapon();
            }
            else if (input > 0)
            {
                weaponItemData = inventory.SelectPreviousWeapon();
            }
            if (weaponItemData != null)
            {
                InstantiateWeapon(weaponItemData.GetPrefabPath());
                yield return weaponSwapDelaySeconds;
            }
        }
        ChangeWeaponAxisInUse = false;

    }

	public void DoCapacity()
	{
		inventory.DoCapacity();
	}


    public IEnumerator EquipLatestWeapon()
    {
        WeaponItemData weaponItemData = null;
        //check if changing weapon is possible (more than 1 weapon in inventory)
        if (HasInventory() && inventory.GetArsenalSize() > 1 && CanChangeWeapon())
        {
            weaponItemData = inventory.SelectLatestWeapon();
            ChangeWeaponAxisInUse = true;
            if (weaponItemData != null && weaponItemData.GetPrefabPath() != weapon.GetPrefab())
            {
                InstantiateWeapon(weaponItemData.GetPrefabPath());
                yield return weaponSwapDelaySeconds;
            }
        }
        ChangeWeaponAxisInUse = false;
    }

    public IEnumerator EquipWeaponSlot(int slot)
    {
        WeaponItemData weaponItemData = null;
        if (HasInventory() && CanChangeWeapon())
        {
            weaponItemData = inventory.SelectWeaponSlot(slot);
            ChangeWeaponAxisInUse = true;
            if (weaponItemData != null)
            {
                InstantiateWeapon(weaponItemData.GetPrefabPath());
                yield return weaponSwapDelaySeconds;
            }
        }
        ChangeWeaponAxisInUse = false;
    }
    private void InstantiateWeapon(string prefabPath)
    {
        
        if(reloadCoroutine != null)
        {
            StopCoroutine(reloadCoroutine);
        }
        Destroy(equippedWeapon);
        equippedWeapon = Instantiate((GameObject)Resources.Load(prefabPath));
        equippedWeapon.transform.position = weaponLocation.transform.position;
        //turn weapon forward
        equippedWeapon.transform.rotation = Quaternion.LookRotation(camTransform.forward) * equippedWeapon.transform.rotation;
        equippedWeapon.transform.SetParent(weaponLocation);
        weapon = equippedWeapon.GetComponent<Weapon>();
        Debug.Log("ChangedWeapon");
    }

    public void PlayerFire()
    {
        weapon.Fire();
    }

    public void PlayerReload()
    {
        reloadCoroutine = StartCoroutine(weapon.Reload());
    }
    
    public void PlayerInteraction()
    {
        if (intDetector != null)
        {
            Interaction interaction = intDetector.GetClosestInteraction();
            if (interaction is WeaponItem && inventory != null)
            {
                WeaponItemData weaponItemData = ((WeaponItem)interaction).GetWeaponItemData();
                inventory.AddWeapon(weaponItemData);
                Destroy(interaction.gameObject);
                //If this is the only weapon in inventory, and no weapon is equiped, equip it
                if (inventory.GetArsenalSize() == 1 && equippedWeapon == null)
                {
                    InstantiateWeapon(weaponItemData.GetPrefabPath());
                }
                Debug.Log(interaction.gameObject.name);
            }

        }
        else
        {
            Debug.Log("Interaction detector not found on " + gameObject.name);
        }
    }

    public bool IsSwappingWeapon()
    {
        return ChangeWeaponAxisInUse;
    }
    public void SetFiring(bool state)
    {
        firing = state;
    }
}
