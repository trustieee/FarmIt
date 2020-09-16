using System.Linq;
using TMPro;
using UnityEngine;

public class UI_Vitals : MonoBehaviour
{
    private Vitals _vitals;
    private TMP_Text _healthTMP;
    private TMP_Text _hungerTMP;

    private void Start()
    {
        _vitals = GetComponentInParent<Vitals>();
        _healthTMP = GetComponentsInChildren<TMP_Text>().Single(c => c.name == "Vitals_Health_Text");
        _hungerTMP = GetComponentsInChildren<TMP_Text>().Single(c => c.name == "Vitals_Hunger_Text");
    }

    private void Update()
    {
        _healthTMP.SetText($"{Mathf.RoundToInt((_vitals.Health / _vitals.MaxHealth) * 100)}%");
        _hungerTMP.SetText($"{Mathf.RoundToInt((_vitals.Hunger / _vitals.MaxHunger) * 100)}%");
    }
}
