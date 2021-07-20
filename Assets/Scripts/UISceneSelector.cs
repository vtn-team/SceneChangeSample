using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class UISceneSelector : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] Text _currentSceneName;

    List<UIButton> _buttonList = new List<UIButton>();
    UIButton _current = null;

    private void Start()
    {
        _currentSceneName.text = GameManager.Instance.CurrentScene;
        _buttonList = GetComponentsInChildren<UIButton>().ToList();
        _current = _buttonList[0];

        _buttonList[0].Setup("戻る", GameManager.Instance.BeforeScene);

        int index = 1;
        foreach (var s in GameManager.Instance.SceneList)
        {
            if (index >= _buttonList.Count) break;
            _buttonList[index].Setup(s, s);
            index++;
        }
    }

    private void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Vector2 vec = new Vector2(h, v);
        vec.Normalize();
        if(vec.sqrMagnitude > 0.01f && (Input.GetButtonDown("Vertical") || Input.GetButtonDown("Horizontal")))
        {
            UIButton c = null;
            float len = 999999.0f;
            float cmax = 999.0f;
            foreach (var b in _buttonList)
            {
                if (_current && _current.gameObject == b.gameObject) continue;

                Vector2 sub = (b.GetPosition2D() - _current.GetPosition2D());

                float dot = sub.x * vec.x + sub.y * vec.y;   //x1 * x2 + y1 * y2 = |a|・|b|Cos(Θ)
                float cross = sub.x * vec.y - vec.x * sub.y; //x1 * y2 - x2 * y1 = |a|・|b|Sin(Θ)
                if (cross < cmax && dot > 0.0f && len > sub.magnitude)
                {
                    cmax = cross;
                    len = sub.magnitude;
                    c = b;
                }
            }
            if(c != null)
            {
                _current = c;
                _image.transform.position = c.transform.position;
            }
        }

        if(Input.GetButtonDown("Fire1"))
        {
            GameManager.Instance.ChangeScene(_current.SceneName);
        }
    }
}
