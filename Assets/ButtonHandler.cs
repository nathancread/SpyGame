using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public void TryAgain()
    {
        SceneManager.LoadScene("Level1");
    }
        public void Exit()
    {
        Application.Quit();
    }
}
