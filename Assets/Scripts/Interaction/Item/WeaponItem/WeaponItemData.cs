public class WeaponItemData : ItemData{
    private string prefabPath;
    private int inventoryAmmo;
    private int clipAmmo, maxClipAmmo;
    private int maxTotalAmmo;

    public WeaponItemData(string id, string n, string path, int cAmmo,int mCAmmo,  int iAmmo, int maxTAmmo) : base(id,n)
    {
        prefabPath = path;
        clipAmmo = cAmmo;
        maxClipAmmo = mCAmmo;
        inventoryAmmo = iAmmo;
        maxTotalAmmo = maxTAmmo;
    }

    public void ChangeInventoryAmmo(int ammo)
    {
        if (maxTotalAmmo >= inventoryAmmo + ammo)
            inventoryAmmo += ammo;
        else
            inventoryAmmo = maxTotalAmmo;
        if(inventoryAmmo < 0)
        {
            inventoryAmmo = 0;
        }
    }

    public int GetInventoryAmmo()
    {
        return inventoryAmmo;
    }

    public void DecrementClipAmmo()
    {
        clipAmmo--;
    }
    public int GetClipAmmo()
    {
        return clipAmmo;
    }

    public void SetClipAmmo(int ammo)
    {
        clipAmmo = ammo;
    }

    public string GetPrefabPath()
    {
        return prefabPath;
    }

    public void ChangePrefabPath(string newPath)
    {
        prefabPath = newPath;
    }
	
    public int GetMaxClipAmmo()
    {
        return maxClipAmmo;
    }
}
