using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

	public GameObject[]UI;
	
    public void ToVolumeChange()
    {
        UI[0].SetActive(false);
        UI[1].SetActive(true);     
    }
    public void ToMenu()
    {
        UI[0].SetActive(true);
        UI[1].SetActive(false);
    }

        
}
