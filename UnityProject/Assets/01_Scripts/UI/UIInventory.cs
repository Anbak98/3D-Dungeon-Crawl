using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public UIItemSlot[] slots;

    public GameObject inventoryWindow;
    public Transform slotPanel;
    public Transform dropPosition;

    [Header("Selected Item")]
    private UIItemSlot selectedItemSlot;
    private int selectedItemSlotIndex;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemStatName;
    public TextMeshProUGUI selectedItemStatValue;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unEquipButton;
    public GameObject dropButton;

    private int curEquipIndex;

    private PlayerController controller;
    private PlayerCondition condition;

    public void Start()
    {
        CharacterManager.Instance.Player.controller.inventory += Toggle;
        CharacterManager.Instance.Player.addItem += AddItem;

        inventoryWindow.SetActive(false);
        slots = new UIItemSlot[slotPanel.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<UIItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
            slots[i].Clear();
        }

        ClearSelectedItemWindow();
    }

    public void AddItem()
    {
        ItemData data = CharacterManager.Instance.Player.rootedItem;

        if(data.canStack)
        {
            UIItemSlot slot = GetItemStack(data);
            if(slot != null)
            {
                slot.quantity++;
                UpdateUI();
                CharacterManager.Instance.Player.rootedItem = null;
                return;
            }
        }

        UIItemSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.item = data;
            emptySlot.quantity = 1;
            UpdateUI();
            CharacterManager.Instance.Player.rootedItem = null;
            return;
        }

        CharacterManager.Instance.Player.rootedItem = null;
    }

    private void ClearSelectedItemWindow()
    {
        selectedItemSlot = null;

        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        useButton.SetActive(false);
        equipButton.SetActive(false);
        unEquipButton.SetActive(false);
        dropButton.SetActive(false);
    }

    public void UpdateSelectedItem(int index)
    {
        if (slots[index].item == null) return;

        selectedItemSlot = slots[index];
        selectedItemSlotIndex = index;

        selectedItemName.text = selectedItemSlot.item.displayName;
        selectedItemDescription.text = selectedItemSlot.item.description;

        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        foreach (var consumable in selectedItemSlot.item.consumables)
        {
            selectedItemStatName.text += consumable.type.ToString() + "\n";
            selectedItemStatValue.text += consumable.value.ToString() + "\n";
        }

        useButton.SetActive(selectedItemSlot.item.type == ItemType.Consumable);
        equipButton.SetActive(selectedItemSlot.item.type == ItemType.Equipable && !slots[index].equipped);
        unEquipButton.SetActive(selectedItemSlot.item.type == ItemType.Equipable && slots[index].equipped);
        dropButton.SetActive(true);
    }

    public void OnUseButton()
    {
        if (selectedItemSlot.item.type == ItemType.Consumable)
        {
            foreach(var consume in selectedItemSlot.item.consumables)
            {
                switch(consume.type)
                {
                    case ConsumableType.Speed:
                        StartCoroutine(SpeedUp(consume.value, 2f));
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private IEnumerator SpeedUp(float value, float duration)
    {
        float originalSpeed = CharacterManager.Instance.Player.status.MoveSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float boost = Mathf.Lerp(value, 0, elapsedTime / duration);
            CharacterManager.Instance.Player.status.MoveSpeed = originalSpeed + boost;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        CharacterManager.Instance.Player.status.MoveSpeed = originalSpeed;
    }


    private void UpdateUI()
    {
        foreach (var slot in slots)
            if (slot.item != null)
                slot.Set();
            else
                slot.Clear();
    }

    private UIItemSlot GetItemStack(ItemData itemData)
    {
        foreach (var slot in slots)
            if (slot.item == itemData)
                return slot;

        return null;
    }

    private UIItemSlot GetEmptySlot()
    {
        foreach (var slot in slots)
            if (slot.item == null)
                return slot;

        return null;
    }

    private void Toggle() => inventoryWindow.SetActive(!inventoryWindow.activeSelf);
}
