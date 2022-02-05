using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    public static string name = "";
    public GameObject obj;
    
    // Start is called before the first frame update
    void Start()
    {
    }
 
    // Update is called once per frame
    void Update()
    {
        
    }

    public void exitGame(){
        Application.Quit();
    }

    public void loadGame(){
        name = obj.GetComponent<TMP_InputField>().text;
        print(name);
        SceneManager.LoadScene(1);
    }

}
