using UnityEngine;

public class Hotbar : MonoBehaviour
{
    private void Awake()
    {
        foreach (var button in GetComponentsInChildren<HotbarButton>())
        {
            button.OnButtonClicked += OnButtonClicked;
        }
    }

    private void OnButtonClicked(int buttonNumber)
    {
        Debug.Log($"Button {buttonNumber} clicked!");
    }
}
