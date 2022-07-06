using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropdownChoice : MonoBehaviour
{

    public ToggleChoice toggleChoice;
    public TMP_Dropdown dropdown;

    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        dropdown.onValueChanged.AddListener(delegate { ChangeValue(dropdown); });
        List<string> debrisNames = new List<string>();
        foreach (Debris d in toggleChoice.types) {
            debrisNames.Add(d.name);
        }
        dropdown.ClearOptions();
        dropdown.AddOptions(debrisNames);
    }

    public void ChangeValue(TMP_Dropdown newValue) {
        toggleChoice.choice = dropdown.options[newValue.value].text;
    }

}
