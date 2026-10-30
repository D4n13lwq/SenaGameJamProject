using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneAfterTime : MonoBehaviour
{
    [SerializeField] float delay;
    [SerializeField] GameObject fade;

    private void Start()
    {
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        fade.SetActive(true);       
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(0);
    }
}
