// Diseñado y desarrollado por Miguel I. Fernandez
// Email: misaacf30@gmail.com
// Por favor, no dudes en contactarme si necesitas ayuda.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void Initial1()
    {
        storeSceneIndex();
        SceneManager.LoadScene("1.Initial1");
    }

    public void Initial2()
    {
        storeSceneIndex();
        SceneManager.LoadScene("2.Initial2");
    }

    public void Jardin()
    {
        storeSceneIndex();
        SceneManager.LoadScene("3.Jardin");
    }
    

    public void LoadShapesScene()
    {
        storeSceneIndex();
        SceneManager.LoadScene("1.1.Shapes");
    }


    public void LoadLongShortScene()
    {
        storeSceneIndex();
        SceneManager.LoadScene("1.2.LongShort");
    }

    public void LoadNumbersScene()
    {
        storeSceneIndex();
        SceneManager.LoadScene("1.3.Counting");
    }

    public void LoadAdditionScene()
    {
        storeSceneIndex();
        SceneManager.LoadScene("2.1.Addition");
    }

    public void LoadFrontBackScene()
    {
        storeSceneIndex();
        SceneManager.LoadScene("2.4.FrontBack");
    }


    public void LoadGreaterLessScene()
    {
        storeSceneIndex();
        SceneManager.LoadScene("3.2.GreaterLess");
    }

    public void LoadDragDropScene()
    {
        storeSceneIndex();
        SceneManager.LoadScene("3.3.DragDrop");
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadPreviousScene()
    { 
        if(PlayerPrefs.GetInt("PrevSceneIndex") >= SceneManager.GetActiveScene().buildIndex)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("PrevSceneIndex"));
        }    
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    private void storeSceneIndex()
    {
        PlayerPrefs.SetInt("PrevSceneIndex", SceneManager.GetActiveScene().buildIndex);
    }
}
