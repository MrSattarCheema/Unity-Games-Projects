using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private Text txtScore;
    [SerializeField]
    private Sprite[] liveSprites;
    [SerializeField]
    private Image liveImage;
    [SerializeField]
    private Text txtGameOver;
    [SerializeField]
    private Text restartTxt;
    [SerializeField]
    private GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        txtScore.text = "Score: " ;
        gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        txtGameOver.gameObject.SetActive(false);
        restartTxt.gameObject.SetActive(false) ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void updateScore(int score)
    {
        txtScore.text = $"Score: {score}";
    }
    public void updateLivesImg(int lives)
    {
        liveImage.sprite = liveSprites[lives];
    }
    public void gameOvertxt()
    {
        txtGameOver.gameObject.SetActive (true);
        restartTxt.gameObject.SetActive(true) ;
        gameManager.gameState();
        StartCoroutine(gameFlicker());
    }
    private IEnumerator gameFlicker()
    {
        while (true)
        {
            txtGameOver.text = "Game Over!";
            yield return new WaitForSeconds(.5f);
            txtGameOver.text = "";
            yield return new WaitForSeconds(.5f);
        }
    }
}
