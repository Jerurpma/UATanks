using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;


public class MainMenu2 : MonoBehaviour
{




public void PlayGame()
    {


		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }




	public void QuitGame()
	{
#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}
}

