using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartGameText;
    private GameManager _gameManager;
   

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager==null)
        {
            Debug.LogError("game manager is null");
        }
        _gameOverText.gameObject.SetActive(false);
        _restartGameText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void ScoreUpdate(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }
    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];
    }
    public void GameOver()
    {
        _gameManager.GameOver();
        _restartGameText.gameObject.SetActive(true);           
        StartCoroutine("GameDeathFlicker");
    }
    IEnumerator GameDeathFlicker()
    {
        while(true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }    
    }  
}
