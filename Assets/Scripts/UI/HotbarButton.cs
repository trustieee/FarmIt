using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HotbarButton : MonoBehaviour
{
    public event Action<int> OnButtonClicked;

    private KeyCode _keyCode;
    private TMP_Text _text;
    private int _keyNumber;

    private void OnValidate()
    {
        _keyNumber = transform.GetSiblingIndex() + 1;
        _keyCode = KeyCode.Alpha0 + _keyNumber;

        if (_text == null)
        {
            _text = GetComponentInChildren<TMP_Text>();
        }

        _text.SetText(_keyNumber.ToString());
        gameObject.name = $"Hotbar Button {_keyNumber}";
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(HandleClick);
    }

    private void Update()
    {
        if (Input.GetKeyDown(_keyCode))
        {
            HandleClick();
        }
    }

    private void HandleClick()
    {
        OnButtonClicked?.Invoke(_keyNumber);
    }
}
