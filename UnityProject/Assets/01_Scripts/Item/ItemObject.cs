using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public ItemData _data;

    private void OnTriggerEnter(Collider other)
    {
        CharacterManager.Instance.Player.rootedItem = _data;
        CharacterManager.Instance.Player.addItem?.Invoke();
    }
}
