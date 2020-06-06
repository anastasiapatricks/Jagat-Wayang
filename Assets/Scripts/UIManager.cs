using RotaryHeart.Lib.SerializableDictionary;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class UIComponentMap : SerializableDictionaryBase<string, UIComponent> { }

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance { get { return _instance; } }

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        }
        else {
            _instance = this;
        }
    }

    public UIComponentMap componentMap;
    private Stack<UIComponent> focusStack = new Stack<UIComponent>();
    public bool IsFocused()
    {
        return focusStack.Count > 0;
    }

    private void UpdateFocus()
    {
        foreach (UIComponent component in componentMap.Values)
        {
            component.Blur();
        }
        if (focusStack.Count > 0)
        {
            focusStack.Peek().Focus();
        }
    }

    public void AddFocus(string componentName)
    {
        if (!componentMap.ContainsKey(componentName))
        {
            return;
        }
        focusStack.Push(componentMap[componentName]);
        UpdateFocus();
    }

    public void RemoveFocus()
    {
        if (focusStack.Count > 0)
        {
            focusStack.Pop();
        }
        UpdateFocus();
    }
}
