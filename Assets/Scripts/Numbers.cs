// Diseñado y desarrollado por Miguel I. Fernandez
// Email: misaacf30@gmail.com
// Por favor, no dudes en contactarme si necesitas ayuda.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Numbers : MonoBehaviour
{
    public Text number1;
    public Text number2;
    public int numbersLimit;
    public AudioSource[] sounds;
    public AudioSource[] answerSounds;  // 0 = right, 1 = wrong

    string[] numbersNames = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", 
        "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30"
    };

    private int correctAnswer;

    // Start is called before the first frame update
    void Start()
    {
        int lastSceneIndex = PlayerPrefs.GetInt("PrevSceneIndex");
        if(lastSceneIndex == 1)
        {
            numbersLimit = 10;
        }
        else if(lastSceneIndex == 2)
        {
            numbersLimit = 30;
        }

        int num1 = Random.Range(0, numbersLimit+1);

        int min = 0;
        int max = numbersLimit;
        
        if(numbersLimit == 30)
        {
            min = num1 - 3;
            if (min < 0)
                min = 0;
            max = num1 + 3;
            if (max > numbersLimit)
                max = numbersLimit;
        }
        
        int num2 = Random.Range(min, max+1);

        
        while(num2 == num1)
        {
            num2 = Random.Range(min, max+1);
        }

        number1.text = num1.ToString();
        number2.text = num2.ToString();

        int[] choseNumbers = { num1, num2 };
        int correctIndex = Random.Range(0, 2);

        for(int i = 0; i <= sounds.Length; i++)
        {
            if (sounds[i].clip.name == numbersNames[choseNumbers[correctIndex]])
            {
                sounds[i].PlayDelayed(0.5f);
                break;
            }
        }
        correctAnswer = choseNumbers[correctIndex];
    }

    public void onClick()
    {
        string clickedButtonName = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        var button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        var colors = button.colors;
        var disabledColor = colors.disabledColor;
        if (clickedButtonName == correctAnswer.ToString())
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

    // Update is called once per frame
    void Update()
    {
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
