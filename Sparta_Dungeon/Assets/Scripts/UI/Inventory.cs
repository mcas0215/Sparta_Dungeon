using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static PlayerInventory;

public class InventoryUI : MonoBehaviour
{
    public GameObject itemSlotPrefab;
    public Transform contentPanel;
    public Button actionButton1; // 장착 or 먹기
    public Button actionButton2; // 버리기

    private PlayerInventory playerInventory;
    private ItemData currentItem;

    void Start()
    {
        playerInventory = CharacterManager.Instance.Player.GetComponent<PlayerInventory>();
        RefreshUI();
    }

    public void RefreshUI()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (InventoryItem invItem in playerInventory.items)
        {
            GameObject obj = Instantiate(itemSlotPrefab, contentPanel);
            var slotUI = obj.GetComponent<ItemSlotUI>();
            slotUI.Setup(invItem, this); // 전달값 변경
        }

    }

    public void OnItemClicked(ItemData item)
    {
        currentItem = item;

        actionButton1.onClick.RemoveAllListeners();
        actionButton2.onClick.RemoveAllListeners();

        if (item.type == ItemType.Equipable)
        {
            actionButton1.GetComponentInChildren<TMP_Text>().text = "장착";
            actionButton1.onClick.AddListener(() => EquipItem(item));
        }
        else if (item.type == ItemType.Consumable)
        {
            actionButton1.GetComponentInChildren<TMP_Text>().text = "먹기";
            actionButton1.onClick.AddListener(() => ConsumeItem(item));
        }

        actionButton2.GetComponentInChildren<TMP_Text>().text = "버리기";
        actionButton2.onClick.AddListener(() => DropItem(item));
    }

    void EquipItem(ItemData item)
    {
        Debug.Log($"{item.displayName} 장착됨!");
        // 장착 상태 관리 로직은 나중에 추가
    }

    void ConsumeItem(ItemData item)
    {
        PlayerCondition condition = CharacterManager.Instance.Player.GetComponent<PlayerCondition>();
        PlayerController controller = CharacterManager.Instance.Player.GetComponent<PlayerController>();

        foreach (var con in item.consumables)
        {
            if (con.type == ConsumableType.Hunger)
            {
                condition.Eat(con.value);
            }

            if (con.type == ConsumableType.Health)
            {
                condition.Heal(con.value);
            }

            if (con.type == ConsumableType.JumpBoost)
            {
                controller.ApplyJumpBoost(con.value, 5f); // 예: 5초 동안 버프
            }
        }

        playerInventory.RemoveItem(item);
        RefreshUI();
    }



    void DropItem(ItemData item)
    {
        Transform player = CharacterManager.Instance.Player.transform;
        Vector3 dropPosition = player.position + player.forward * 1.5f + Vector3.up * 0.5f;

        GameObject drop = Instantiate(item.dropPrefab, dropPosition, Quaternion.identity);

        // 여기서 inventoryUI를 연결!
        ItemObject itemObject = drop.GetComponent<ItemObject>();
        if (itemObject != null)
        {
            itemObject.Initialize(this); // this = 현재 Inventory.cs (InventoryUI)
        }

        playerInventory.RemoveItem(item);
        RefreshUI();
    }

}
