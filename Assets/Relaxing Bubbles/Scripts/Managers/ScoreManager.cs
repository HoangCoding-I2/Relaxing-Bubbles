
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour {
    
    public static ScoreManager Instance;
    
    [SerializeField] private Text _currentScoreText;
    [SerializeField] private Text _bestScoreText;
    private int _score = 0;

    private void Awake()
    {
        Instance = this;
        _currentScoreText.text = "0";
        _bestScoreText.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
    }
    
    public void AddScore()
    {
        _score++;
        
        _currentScoreText.text = _score.ToString();
        if (_score > PlayerPrefs.GetInt("BestScore", 0))
        {
            PlayerPrefs.SetInt("BestScore", _score);
            
            _bestScoreText.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
        }
    }
    
    public int GetScore()
    {
        return _score;
    }
}
