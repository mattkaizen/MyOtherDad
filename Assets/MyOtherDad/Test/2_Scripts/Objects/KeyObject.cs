using Objects;

public class KeyObject : InteractiveItem, IPickable
{
    public ItemData Pickup()
    {
        gameObject.SetActive(false);
        return Data;
    }
}
