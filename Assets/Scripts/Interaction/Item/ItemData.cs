public class ItemData {
    private string itemID;
    private string itemName;

    public ItemData(string id, string n)
    {
        itemID = id;
        itemName = n;
    }
    public string GetItemID()
    {
        return itemID;
    }

    public string GetItemName()
    {
        return itemName;
    }

}
