using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;        
    [SerializeField] private GameObject _gameOverEffectPanel;  
    [SerializeField] private GameObject _touchToMoveTextObj;   
    [SerializeField] private GameObject _StartFadeInObj;       
    
    void Awake()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 1.0f;
        StartCoroutine(FadeIn());
    }

    void Update()
    {
        if (_touchToMoveTextObj.activeSelf == false) return;
        
        if (Input.GetMouseButton(0))
        {
            _touchToMoveTextObj.SetActive(false);
        }
    }

    IEnumerator FadeIn()
    {
        _StartFadeInObj.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _StartFadeInObj.SetActive(false);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverCoroutine());
    }

    private IEnumerator GameOverCoroutine()
    {
        _gameOverEffectPanel.SetActive(true);
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(0.5f);
        _gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
