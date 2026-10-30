using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public void TimeS()
    {
        Time.timeScale = 0;
    }
    public void TimeB()
    {
        Time.timeScale = 1;
    }

    public void Finish()
    {
        SceneManager.LoadScene(2);
    }

    public void Return()
    {
        SceneManager.LoadScene(0);
    }
}
