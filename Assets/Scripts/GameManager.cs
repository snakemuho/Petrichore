using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public bool GamePaused { get; private set; } = false;
    private int _collected = 0;
    [SerializeField] GameObject _pauseMenu;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get { if (_instance is null) Debug.Log("game manager null"); return _instance; }
    }
    private void Awake()
    {
        _instance = this;
    }
    private void Update()
    {
        Pause();
    }
    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (GamePaused)
                ResumeGame();
            else PauseGame();
    }
    public void PauseGame()
    {
        GamePaused = true;
        Time.timeScale = 0;
        _pauseMenu?.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        GamePaused = false;
        Time.timeScale = 1;
        _pauseMenu?.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ReloadLevel()
    {
        StartCoroutine(StartReload());
    }

    IEnumerator StartReload()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddCollected()
    {
        _collected++;
        if (_collected >= 4)
            SceneManager.LoadScene("GameEnd");
    }


}
