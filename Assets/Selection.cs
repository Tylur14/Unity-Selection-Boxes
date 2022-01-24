using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selection : MonoBehaviour
{
    [SerializeField] private RectTransform selectionBox;
    [SerializeField] private BoxCollider2D col;
    [SerializeField] private List<Selectable> selectedItems;
    private Vector2 _reference;
    private float _offset;
    private Vector2 _startPos;

    private void Awake()
    {
        _reference = FindObjectOfType<CanvasScaler>().referenceResolution;
        Release();
        Reset();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Reset();
        }
        
        if(Input.GetMouseButton(0))
            Drag(Input.mousePosition * _offset);
        if(Input.GetMouseButtonUp(0))
            Release();
    }

    private void Drag(Vector2 cursorPos)
    {
        if(!selectionBox.gameObject.activeSelf)
            selectionBox.gameObject.SetActive(true);

        float width = cursorPos.x - _startPos.x;
        float height = cursorPos.y - _startPos.y;

        var size =
            new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        var pos =
            _startPos + new Vector2(width / 2, height / 2);

        col.size = size;
        col.offset = new Vector2(
            pos.x - (_reference.x / 2),
            pos.y - (_reference.y / 2));
        
        selectionBox.sizeDelta = size;
        selectionBox.anchoredPosition = pos;
    }

    private void Release()
    {
        selectionBox.gameObject.SetActive(false);
    }

    private void Reset()
    {
        _offset =
            _reference.x / Screen.width;
        _startPos = Input.mousePosition * _offset;
        
        foreach(var s in selectedItems)
            s.ToggleSelect(false);
        selectedItems.Clear();
        
        col.size = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Selectable>() == null) return;
        
        var s = other.GetComponent<Selectable>();
        if (selectedItems.Contains(s)) return;
        s.ToggleSelect(true);
        selectedItems.Add(s);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Selectable>() == null) return;
        
        var s = other.GetComponent<Selectable>();
        if (!selectedItems.Contains(s)) return;
        s.ToggleSelect(false);
        selectedItems.Remove(s);
    }
}
