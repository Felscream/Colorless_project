public class LifeGemItemData : ItemData {
    private int amount;

    public LifeGemItemData(string id, string n, int amnt):base(id, n)
    {
        amount = amnt;
    }
	
    public int GetAmount()
    {
        return amount;
    }
}
