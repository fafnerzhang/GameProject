using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour {

    // Use this for initialization
    public void Stage1()
    {
        SceneManager.LoadScene("Stage1");
       
    }
    public void Menu()
    {
        SceneManager.LoadScene("MenuScene");
        
    }
    public void DemoMap_01()
    {
        SceneManager.LoadScene("DemoMap_01");
    }
	
	
}
