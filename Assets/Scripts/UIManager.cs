using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _ammoText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartGameText;
    private GameManager _gameManager;
    [SerializeField]
    private Text _thrusterText;
    [SerializeField]
    private Image _thrusterImage;
    private Player _player;
    private bool _isMaxAmmo;

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
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player==null)
        {
            Debug.LogError("Player is null");
        }
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
    public void UpdateAmmoCount(int currentAmmo)
    {
        if (currentAmmo<30)
        {
            _ammoText.text = "Current Ammo: " + currentAmmo.ToString();
        }
              
        else if (currentAmmo==30)
        {
            _ammoText.text = "Max " + currentAmmo.ToString();
          
        }
    }
    public void UpdateThrusterImage (float currentThruster)
    {
        _thrusterImage.fillAmount = Mathf.Clamp(_player.currentThrusterFuel / _player.maxThrusterFill, 0, 1f);
        _thrusterText.text = "Fuel: " + currentThruster.ToString();
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
