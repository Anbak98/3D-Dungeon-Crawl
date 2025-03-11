using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerInfo : MonoBehaviour
{
    [SerializeField] private GameObject _panelHealth;
    [SerializeField] private GameObject _uIHealthPrefabs;
    private List<GameObject> _uIHealthObjects;

    // Start is called before the first frame update
    void Start()
    {
        _uIHealthObjects = new();
        for(int i = 0; i < CharacterManager.Instance.Player.status.Health; ++i)
        {
            _uIHealthObjects.Add(Instantiate(_uIHealthPrefabs, _panelHealth.transform));
        }
        CharacterManager.Instance.Player.status.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged()
    {
        if (_uIHealthObjects.Count > 0)
        {
            Destroy(_uIHealthObjects[0]);
            _uIHealthObjects?.RemoveAt(0);
        }
    }
}
