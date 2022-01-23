using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    [SerializeField] private GameObject highlight;

    public void ToggleSelect(bool state)
    {
        highlight.SetActive(state);
    }
}
