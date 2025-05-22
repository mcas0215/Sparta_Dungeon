using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static PlayerInventory;

public class InventoryUI : MonoBehaviour
{
    public GameObject itemSlotPrefab;
    public Transform contentPanel;
    public Button actionButton1; // ���� or �Ա�
    public Button actionButton2; // ������

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
            slotUI.Setup(invItem, this); // ���ް� ����
        }

    }

    public void OnItemClicked(ItemData item)
    {
        currentItem = item;

        actionButton1.onClick.RemoveAllListeners();
        actionButton2.onClick.RemoveAllListeners();

        if (item.type == ItemType.Equipable)
        {
            actionButton1.GetComponentInChildren<TMP_Text>().text = "����";
            actionButton1.onClick.AddListener(() => EquipItem(item));
        }
        else if (item.type == ItemType.Consumable)
        {
            actionButton1.GetComponentInChildren<TMP_Text>().text = "�Ա�";
            actionButton1.onClick.AddListener(() => ConsumeItem(item));
        }

        actionButton2.GetComponentInChildren<TMP_Text>().text = "������";
        actionButton2.onClick.AddListener(() => DropItem(item));
    }

    void EquipItem(ItemData item)
    {
        Debug.Log($"{item.displayName} ������!");
        // ���� ���� ���� ������ ���߿� �߰�
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
                controller.ApplyJumpBoost(con.value, 5f); // ��: 5�� ���� ����
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

        // ���⼭ inventoryUI�� ����!
        ItemObject itemObject = drop.GetComponent<ItemObject>();
        if (itemObject != null)
        {
            itemObject.Initialize(this); // this = ���� Inventory.cs (InventoryUI)
        }

        playerInventory.RemoveItem(item);
        RefreshUI();
    }

}
