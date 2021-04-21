using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{

    private int numberA = 0;
    private int numberB = 0;
    private int numberC = 0;

    public int token = 10;
    public int jackpot = 0;

    public int winOnThree = 3;
    public int winOnTwo = 2;
    public int loseToken = 1;

    public const int MIN_NUMBER = 1;
    public const int MAX_NUMBER = 10;

    public Image _numberA;
    public Image _numberB;
    public Image _numberC;

    public Sprite[] spriteArray;

    public Text _token;
    public Text _jackpot;
    public Text _jackpotLoseText;

    public GameObject lose;
    public GameObject playGround;

    public Button playBtn;

    // Start is called before the first frame update
    void Start()
    {
        jackpot = PlayerPrefs.GetInt("LastJackpot");
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        if (token <= 0)
        {
            playGround.SetActive(false);
            playBtn.gameObject.SetActive(false);
            _jackpotLoseText.text = jackpot.ToString();
            lose.gameObject.SetActive(true);
            PlayerPrefs.SetInt("LastJackpot", jackpot);
        }
    }

    public void PlayBtn()
    {
        numberA = (int)UnityEngine.Random.Range(MIN_NUMBER, MAX_NUMBER);
        numberB = (int)UnityEngine.Random.Range(MIN_NUMBER, MAX_NUMBER);
        numberC = (int)UnityEngine.Random.Range(MIN_NUMBER, MAX_NUMBER);
        ChangeSprite();
        Result();
    }

    void ChangeSprite()
    {
        _numberA.sprite = spriteArray[numberA - 1];
        _numberB.sprite = spriteArray[numberB - 1];
        _numberC.sprite = spriteArray[numberC - 1];
    }

    public void Result()
    {
        if (numberA == numberB && numberA == numberC)
        {
            if (numberA == 7 && numberB == 7 && numberC == 7)
            {
                token = token + jackpot;
                jackpot = 0;
            }
            token = token + winOnThree;
        }
        else if (numberA == numberB || numberB == numberC || numberA == numberC)
        {
            token = token + winOnTwo;
        }
        else
        {
            token = token - loseToken;
            jackpot++;
        }
        UpdateText();
    }

    public void UpdateText()
    {
        _token.text = token.ToString();
        _jackpot.text = jackpot.ToString();

    }

    public void playAgain()
    {
        jackpot = PlayerPrefs.GetInt("LastJackpot");
        SceneManager.LoadScene(0);
    }

    public void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}
