using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public PlayerCondition status;
    [SerializeField] private PlayerController _controller;

    private void Awake()
    {
        status.OnAwake();
        CharacterManager.Instance.Player = this;
    }

    private void Start()
    {
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        if (!Application.isFocused) return;
        _controller.OnFixedUpdate();
    }

    private void LateUpdate()
    {
        if (!Application.isFocused) return;
        _controller.OnLateUpdate();
    }
}
