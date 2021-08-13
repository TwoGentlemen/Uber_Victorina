using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameLogic : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] private QustionStruct[] qustions;

    [Space(5)]
    [SerializeField] private GameObject panelButtonsNext;
    [SerializeField] private GameObject panelGameCompleted;
    [SerializeField] private Button[] bottonsAnswer;

    [Space(5)]
    [SerializeField] private TextMeshProUGUI textQuestion;

    [Space(5)]
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textScore2;

    private TextMeshProUGUI[] buttonsAnswerText;
    private byte countClick = 0; //Для подсчета кол-во кликов до показа рекламы 

    //Текущий ответ игрока
    private string answerPlayer = "";
    //Номер кнопки хранящий верный ответ
    private int indexTrueAnswer = 0;
    private int indexPlayerChoice = -1;

    private void Start()
    {
        InitializeButtonsAnswerText();
        LoadScore();
    }
    
    private void InitializeButtonsAnswerText()
    {
        buttonsAnswerText = new TextMeshProUGUI[bottonsAnswer.Length];

        for (int i = 0; i < bottonsAnswer.Length; i++)
        {
            buttonsAnswerText[i] = bottonsAnswer[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public void LoadScore()
    {
        textScore.text = "Правильных ответов " + PlayerStats.instanse.Score + " из "+qustions.Length+".";
        textScore2.text = textScore.text;
    }

    //Метод заполнения карточки с вопросом, ответами, подсказкой
    public void SetQuestion()
    {

        if(!CheckLevel()){ return;}

        panelButtonsNext.SetActive(false);
        indexPlayerChoice = -1;

        if (qustions == null) { Debug.LogError("No question!"); return;}

        if(PlayerStats.instanse.Level >= qustions.Length)
        {
            PlayerStats.instanse.SaveLevel(0);
        }

        textQuestion.text = qustions[PlayerStats.instanse.Level].question;
        SetAnswer();
    }

    //Метод заполнения ответов
    private void SetAnswer()
    {
        string[] _answers = 
        { 
            qustions[PlayerStats.instanse.Level].trueAnswer,
            qustions[PlayerStats.instanse.Level].answer1,
            qustions[PlayerStats.instanse.Level].answer2,
            qustions[PlayerStats.instanse.Level].answer3
        };

        _answers = MixWell(_answers);

        if(bottonsAnswer.Length != _answers.Length) { Debug.LogError("Error length!"); return;}

        for (int i = 0; i < bottonsAnswer.Length; i++)
        {
            buttonsAnswerText[i].text = _answers[i];

            if(_answers[i] == qustions[PlayerStats.instanse.Level].trueAnswer)
            {
                indexTrueAnswer = i;
            }
        }
    }

    //Метод перемешивания ответов
    private string[] MixWell(string[] str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            int rnd = Random.Range(0,str.Length);
            
            var bufStr = str[i];
            str[i] = str[rnd];
            str[rnd] = bufStr;
        }

        return str;
    }

    //Метод нажатия кнопок с ответами
    public void ButtonAnswerClick(int index)
    {
        if(bottonsAnswer.Length <= index || bottonsAnswer == null) { return;}

        bottonsAnswer[index].image.color = Color.yellow;
        answerPlayer = buttonsAnswerText[index].text;
        indexPlayerChoice = index;

        for (int i = 0; i < bottonsAnswer.Length; i++)
        {
            if(i != index)
            {
                bottonsAnswer[i].image.color = Color.white;
            }
        }
    }

    //Метод нажатия на кнопку ответить
    public void ButtonClickGetAnswer()
    {
        if(indexPlayerChoice == -1) { return;}

        if(answerPlayer == qustions[PlayerStats.instanse.Level].trueAnswer)
        {
            Debug.Log("True");
            bottonsAnswer[indexTrueAnswer].image.color = Color.green;
            PlayerStats.instanse.AddScore();
            
            
        }
        else
        {
            Debug.Log("False");
            bottonsAnswer[indexTrueAnswer].image.color = Color.green;
            bottonsAnswer[indexPlayerChoice].image.color = Color.red;

        }

        PlayerStats.instanse.SaveLevel(PlayerStats.instanse.Level + 1);

        foreach (var item in bottonsAnswer)
        {
            item.enabled = false;
        }

        panelButtonsNext.SetActive(true);

        LoadScore();

        countClick++;
        if(countClick >= 10)
        {
            countClick = 0;

            //ToDO
        }
    }

    //Метод проверки пройденных уровней
    public bool CheckLevel()
    {
        if (qustions.Length <= PlayerStats.instanse.Level)
        {
            LoadScore();
            panelGameCompleted.SetActive(true);
            PlayerStats.instanse.SaveLevel(0);
            PlayerStats.instanse.ClearScore();
            return false;

        }

        for (int i = 0; i < bottonsAnswer.Length; i++)
        {
            bottonsAnswer[i].image.color = Color.white;
            bottonsAnswer[i].enabled = true;
        }

        return true;
    }

}
