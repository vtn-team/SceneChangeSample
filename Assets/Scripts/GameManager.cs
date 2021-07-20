using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager
{
    static public GameManager Instance => _instance;
    static GameManager _instance = new GameManager();
    private GameManager() {  }

    public List<string> SceneList => _sceneList;
    public string BeforeScene => _beforeScene;
    public string CurrentScene => _currentScene;

    string _currentScene = "SampleScene"; //HACK
    string _beforeScene;
    List<string> _sceneList = new List<string>()
    {
        "Test01",
        "Test02",
        "Test03",
        "Test04",
    };

    public void ChangeScene(string nextScene)
    {
        _beforeScene = _currentScene;
        _currentScene = nextScene;
        Fade.In(() => {
            UnityEngine.SceneManagement.SceneManager.LoadScene(_currentScene);
            Fade.Out(() => { });
        });
    }
}

