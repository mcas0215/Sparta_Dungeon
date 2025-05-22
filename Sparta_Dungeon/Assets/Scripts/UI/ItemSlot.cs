using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static PlayerInventory;

public class ItemSlotUI : MonoBehaviour
{
    public Button button;
    public TMP_Text itemNameText;

    private InventoryItem invItem;
    private InventoryUI inventoryUI;

    public void Setup(InventoryItem data, InventoryUI ui)
    {
        invItem = data;
        inventoryUI = ui;

        button.onClick.AddListener(OnClick);

        if (data.data.canStack)
        {
            itemNameText.text = $"{data.data.displayName} x{data.quantity}";
        }
        else
        {
            itemNameText.text = data.data.displayName;
        }
    }

    void OnClick()
    {
        inventoryUI.OnItemClicked(invItem.data);
    }
}
