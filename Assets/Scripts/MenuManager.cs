using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject _CoOpButton;
    // Start is called before the first frame update
    private void Start()
    {
        if (Application.platform==RuntimePlatform.Android || Application.isMobilePlatform)
        {
            _CoOpButton.SetActive(false);
        }
    }
    public void LoadGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
