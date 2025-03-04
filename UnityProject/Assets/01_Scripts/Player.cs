using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public PlayerCondition status;

    [SerializeField] private PlayerController _controller;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
    }
}
