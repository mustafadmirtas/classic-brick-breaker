using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Button button_play, button_opt, button_quit, buttonback_menu;
    public GameObject panel_play, panel_opt,panel_lang;
    public Button[] buttons2 = new Button[32];
    // Start is called before the first frame update
    Lang language;
    void Start()
    {
        button_play.onClick.AddListener(play_panel);
        buttonback_menu.onClick.AddListener(play_backmenu);
        button_opt.onClick.AddListener(opt_panel);
        button_quit.onClick.AddListener(quit_game);
        
        language = new Lang();
        foreach (Button button in buttons2)
        {
            button.onClick.AddListener(() => { SelectLevel(button.GetComponentInChildren<Text>().text); });
        }
        
        if ((PlayerPrefs.GetInt("Lang", 0)) == 0)
        {
            panel_lang.SetActive(true);
        }  
        OpenLevels();
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    void play_panel()
    {
        panel_play.SetActive(true);
    }
    void play_backmenu()
    {
        panel_play.SetActive(false);
    }
    void quit_game()
    {
        Application.Quit();
    }
    void opt_panel()
    {
        panel_play.SetActive(true);
    }
    void SelectLevel(string text)
    {
        
        int b = int.Parse(text);
        SceneManager.LoadScene(b);
    }
    void OpenLevels()
    {
        int a = PlayerPrefs.GetInt("Comp_Levels",1);
        for(int i = 0; i< a; i++)
        {
            buttons2[i].interactable = true;
        }
    }
 

}
