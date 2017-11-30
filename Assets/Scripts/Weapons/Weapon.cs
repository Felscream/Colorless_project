using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : MonoBehaviour{
    [SerializeField]
    protected float fireRate, reloadTime, verticalRecoilIncrease, horizontalRecoilIncrease;
    [SerializeField]
    protected string gunPrefabFolder = "Prefabs/Weapons/Guns";
    [SerializeField]
    protected string prefab, weaponName;
    protected Transform bulletSpawn;
    protected float verticalRecoil = 0.0f, horizontalRecoil = 0.0f;
    protected Camera cam;
    protected Text ammoInfo;
    //weapon related timers
    protected float firingStart, reloadStart;
    //bullet related variables

    protected bool isReloading;
    protected WeaponItemData weaponData;
    protected Inventory playerInventory;
    protected Animator animator;

    public void Awake()
    {
        
        prefab = GetComponent<Transform>().name;
        
        Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform t in transforms)
        {
            if (t.tag == "BulletSpawn" && t.gameObject.activeInHierarchy)
            {
                bulletSpawn = t;
            }
        }
        if (bulletSpawn == null)
        {
            Debug.Log("Bullet spawn transform not found");
        }
        ammoInfo = GameObject.FindGameObjectWithTag("AmmoDisplay").GetComponent<Text>();
        playerInventory = Inventory.GetInstance();
    }
    public void Start()
    {
        cam = Camera.main;
        animator = GetComponent<Animator>();
        if (!animator)
        {
            animator = GetComponentInChildren<Animator>();
        }
        weaponData = playerInventory.FindWeaponDataByName(weaponName);
        if (weaponData == null)
        {
            Debug.Log("Weapon data found in inventory");
        }
        firingStart = -(1 / fireRate);
    }

    public void LateUpdate()
    {
        UpdateAmmoInfo();
    }
    
    //abstract Fire() Method
    public abstract void Fire();
    protected bool CanFire()
    {
        //Debug.Log(clipAmmo);
        if ((Time.time >= (1 / fireRate + firingStart)) && weaponData.GetClipAmmo() > 0 && !isReloading)
        {
            return true;
        }
        return false;
    }

    
    protected void UpdateAmmoInfo()
    {
        if (ammoInfo != null)
            ammoInfo.text = weaponData.GetClipAmmo().ToString() + " | " + weaponData.GetInventoryAmmo().ToString();
        else
            Debug.Log("No ammunition display element linked");
    }

    public IEnumerator Reload()
    {
        int currentTotalAmmo = weaponData.GetInventoryAmmo();
        if (weaponData.GetClipAmmo() < weaponData.GetMaxClipAmmo() && !isReloading && currentTotalAmmo > 0)
        {
            int ammoSpent = weaponData.GetMaxClipAmmo() - weaponData.GetClipAmmo();
            isReloading = true;
            animator.SetBool("reload", true);
            Debug.Log("Reloading");
            //Debug.Log(m_CurrentClipInfo.Length);
            yield return new WaitForSeconds(reloadTime);
            int clipAmmo = (weaponData.GetMaxClipAmmo() <= currentTotalAmmo + weaponData.GetClipAmmo()) ? weaponData.GetMaxClipAmmo() : (weaponData.GetClipAmmo() + currentTotalAmmo) % weaponData.GetMaxClipAmmo();
            
            if (this is HitScan)
            {
                HitScan temp = (HitScan)this;
                temp.ResetBulletSpread();
            }
            weaponData.SetClipAmmo(clipAmmo);
            weaponData.ChangeInventoryAmmo(-ammoSpent);
            Debug.Log("Done reloading");
            animator.SetBool("reload", false);
            UpdateAmmoInfo();

        }
        isReloading = false;
        
    }
    public bool GetReloadingStatus()
    {
        return isReloading;
    }

    public string GetPrefab()
    {
        return gunPrefabFolder + prefab;
    }



}
