using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;
    public LayoutElement layoutElement;
    public int characterWrapLimit;
    public Transform objectTransform;
    public Vector2 offset = new Vector2(0,100);
    Canvas canvas;
    //RectTransform rectTransform;
    void Awake()
    {
        //rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        objectTransform = transform;
    }
    void Start()
    {
        
    }
    public void SetText(string content, string header = "")
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Rect rect = rectTransform.rect;
        
        Vector2 pos = (Vector2)objectTransform.InverseTransformVector(objectTransform.position) + offset;
        Vector2 screenBounds = new Vector2(Screen.width, Screen.height);
        //Debug.Log("boundsx" + (rect.width - screenBounds.x / 2));
        //Debug.Log("boundsy" + (rect.height - screenBounds.y / 2));
        
        pos.x = Mathf.Clamp(pos.x, -300, screenBounds.x - rect.width);
        pos.y = Mathf.Clamp(pos.y, rect.height - screenBounds.y / 2, screenBounds.y - rect.height);
        //Debug.Log("Location To Draw" + pos);
        rectTransform.anchoredPosition = pos;


        headerField.text = header;
        headerField.gameObject.SetActive(!string.IsNullOrEmpty(header));
        contentField.text = content;
        ToggleElement();
    } 
    void Update()
    {
        if (Application.isEditor)
        {
            ToggleElement();
        }
    }
    void ToggleElement()
    {
        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;
        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit);
    }
}
