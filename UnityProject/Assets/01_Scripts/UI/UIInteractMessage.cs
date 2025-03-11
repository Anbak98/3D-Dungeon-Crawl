using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteractMessage : MonoBehaviour
{
    [SerializeField] private GameObject message;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = CharacterManager.Instance.Player;
        _player.controller.interactableObjectDetector.OnInteractableDetected += () =>
        {
            message.SetActive(true);
            message.transform.position = _player.controller._look.position + _player.controller._look.forward;
            message.transform.rotation = _player.controller._look.rotation;
        };
        _player.controller.interactableObjectDetector.OnInteractableRemoved += () => { message.SetActive(false); };
    }
}
