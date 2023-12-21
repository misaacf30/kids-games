// Diseñado y desarrollado por Miguel I. Fernandez
// Email: misaacf30@gmail.com
// Por favor, no dudes en contactarme si necesitas ayuda.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class AddingNums : MonoBehaviour
{
    public Text number1;
    public Text number2;
    public Button[] answersButtons;
    public int maxNumberToSum;
    public AudioSource[] answerSounds;  // 0 = right, 1 = wrong

    private int correctAnswer;

    // Start is called before the first frame update
    void Start()
    {
        int lastSceneIndex = PlayerPrefs.GetInt("PrevSceneIndex");
        int minimumNumberToSum = 0;
        if (lastSceneIndex == 2)
        {
            maxNumberToSum = 9;
            minimumNumberToSum = 0;
        }
        else if (lastSceneIndex == 3)
        {
            maxNumberToSum = 20;
            minimumNumberToSum = 1;
        }

        int num1 = Random.Range(minimumNumberToSum, maxNumberToSum + 1);
        int num2 = Random.Range(minimumNumberToSum, maxNumberToSum + 1);

        /*while (num1 == num2)
        {
            num2 = Random.Range(minimumNumberToSum, (maxNumberToSum + 1));
        }*/

        number1.text = num1.ToString();
        number2.text = num2.ToString();


        correctAnswer = num1 + num2;

        int min = correctAnswer - 3;
        if (min < minimumNumberToSum)
            min = minimumNumberToSum;
        int max = correctAnswer + 3;
        if (max > maxNumberToSum*2)
            max = maxNumberToSum*2;
        int incorrectAnswer = Random.Range(min, max+1);
 
        while(incorrectAnswer == correctAnswer)
        {
            incorrectAnswer = Random.Range(min, max+1);
        }

        int correctAnsIndex = Random.Range(0, 2);

        for(int i = 0; i < 2; i++)
        {
            if (i == correctAnsIndex)
            {
                answersButtons[i].GetComponentInChildren<Text>().text = (num1 + num2).ToString();
            }
            else
            {
                answersButtons[i].GetComponentInChildren<Text>().text = incorrectAnswer.ToString();
            }
        }        
    }

    public void OnClick()
    {
        string clickedButtonName = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        var button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        var colors = button.colors;
        var disabledColor = colors.disabledColor;
        if(clickedButtonName == correctAnswer.ToString())
        {
            disabledColor = Color.green;
            answerSounds[0].Play();
        }
        else
        {
            disabledColor = Color.red;
            answerSounds[1].Play();
        }      
        colors.disabledColor = disabledColor;
        button.colors = colors;

        Button[] btns = EventSystem.current.currentSelectedGameObject.transform.parent.GetComponentsInChildren<Button>();
        foreach (Button btn in btns)
        {
            btn.interactable = false;
        }
    }
     
    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("PrevSceneIndex"));
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
