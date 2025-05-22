using UnityEngine;

public interface IInteractable
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;
    public InventoryUI inventoryUI; // 여기 추가

    public string GetInteractPrompt()
    {
        return $"{data.displayName}\n{data.description}";
    }

    public void OnInteract()
    {
        var player = CharacterManager.Instance.Player;

        player.itemData = data;
        player.GetComponent<PlayerInventory>().AddItem(data);

        if (inventoryUI != null)
        {
            inventoryUI.RefreshUI();
        }

        Destroy(gameObject);
    }
    public void Initialize(InventoryUI ui)
    {
        inventoryUI = ui;
    }
}
