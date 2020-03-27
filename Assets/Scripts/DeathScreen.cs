using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public Button menu;

    void Start() {
        Button menuBtn = menu.GetComponent<Button>();

        menuBtn.onClick.AddListener(menuClick);
    }

    void menuClick()
    {
        SceneManager.LoadScene("Menu");
    }
}
