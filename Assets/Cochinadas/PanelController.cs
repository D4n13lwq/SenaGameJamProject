using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    [SerializeField] float delay;

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator FadeDelay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(1);
    }

    public void StartGame()
    {
        StartCoroutine(FadeDelay());
    }
}
