using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuBtn : MonoBehaviour
{
    public Button start;
    public Button quit;
    void Start()
    {
        Button startBtn = start.GetComponent<Button>();
        Button quitBtn = quit.GetComponent<Button>();

        startBtn.onClick.AddListener(startClick);
        quitBtn.onClick.AddListener(quitClick);
    }

    void startClick()
    {
        SceneManager.LoadScene("Game");
    }

    void quitClick()
    {
        Application.Quit();
    }
}
