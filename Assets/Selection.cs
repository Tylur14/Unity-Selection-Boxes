using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selection : MonoBehaviour
{
    [SerializeField] private RectTransform selectionBox;
    [SerializeField] private BoxCollider2D col;
    private Vector2 _reference;
    private float _offset;
    private Vector2 _startPos;

    private void Awake()
    {
        _reference = FindObjectOfType<CanvasScaler>().referenceResolution;
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _offset =
                _reference.x / Screen.width;
            _startPos = Input.mousePosition * _offset;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Selectable>()!=null)
            other.GetComponent<Selectable>().ToggleSelect(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.GetComponent<Selectable>()!=null)
            other.GetComponent<Selectable>().ToggleSelect(false);
    }
}
