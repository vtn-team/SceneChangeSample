using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    [SerializeField] RectTransform _rt;
    [SerializeField] Text _text;
    [SerializeField] Button _button;
    string _sceneName;

    public string SceneName => _sceneName;
    
    public Vector2 GetPosition2D()
    {
        return new Vector2(_rt.position.x, _rt.position.y);
    }

    public void Setup(string displayName, string sceneName)
    {
        _sceneName = sceneName;
        _text.text = displayName;
    }
}
