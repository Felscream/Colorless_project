using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : Item {
    [SerializeField]
    protected string id,weaponPrefabName;
    [SerializeField]
    protected int clipAmmo, maxClipAmmo, inventoryAmmo, maxTotalAmmo;

    protected const string weaponPrefabFolderPath = "Prefabs/Weapons/Guns/";

    WeaponItemData weaponData;
    private void Start()
    {
        weaponData = new WeaponItemData(id, itemName, GetWeaponPrefabPath(), clipAmmo, maxClipAmmo, inventoryAmmo, maxTotalAmmo);
    }
    public string GetWeaponPrefabPath()
    {
        return weaponPrefabFolderPath + weaponPrefabName;
    }

    public WeaponItemData GetWeaponItemData()
    {
        return weaponData;
    }
}
