using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class Fade : MonoBehaviour
{
    static public Fade Instance => _instance;
    static Fade _instance;

    public delegate void Callback();
    Callback _callback = null;

    [SerializeField] CanvasGroup _cg;
    float _timer = 1.0f;
    float _fadeTime = 1.0f;
    bool _isIn = false;
    bool _isOut = true;

    private void Awake()
    {
        _instance = this;
        _cg.alpha = 1.0f;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if(_isIn)
        {
            _cg.alpha = 1.0f - _timer / _fadeTime;
        }
        if (_isOut)
        {
            _cg.alpha = _timer / _fadeTime;
        }

        if (_isIn || _isOut)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _isIn = _isOut = false;
                _callback?.Invoke();
            }
        }
    }

    static public void In(Callback cb, float time = 0.5f)
    {
        _instance._fadeTime = time;
        _instance._timer = time;
        _instance._cg.alpha = 0.0f;
        _instance._isIn = true;
        _instance._callback = cb;
    }

    static public void Out(Callback cb, float time = 0.5f)
    {
        _instance._fadeTime = time;
        _instance._timer = time;
        _instance._cg.alpha = 1.0f;
        _instance._isOut = true;
        _instance._callback = cb;
    }
}
