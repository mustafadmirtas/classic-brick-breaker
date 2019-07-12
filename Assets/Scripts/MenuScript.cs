using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public static MenuScript instance;
    public Button button_play, button_opt, button_quit, buttonback_menu,buttonquit_opt, button_leader;
    public GameObject panel_play, panel_opt,panel_lang,panel_leaderboard,panel_user;
    public Button[] buttons2 = new Button[32];
    // Start is called before the first frame update
    Lang language;
    void Start()
    {
        button_play.onClick.AddListener(PlayPanel);
        buttonback_menu.onClick.AddListener(PlayBackmenu);
        button_opt.onClick.AddListener(OptPanel);
        buttonquit_opt.onClick.AddListener(QuitOpt);
        button_quit.onClick.AddListener(QuitGame);
        button_leader.onClick.AddListener(PlayLeaderBoard);
        instance = this;
        language = new Lang();
        foreach (Button button in buttons2)
        {
            button.onClick.AddListener(() => { SelectLevel(button.GetComponentInChildren<Text>().text); });
        }
        
        if(PlayerPrefs.GetInt("Lang", 0) == 0)
        {
            panel_lang.SetActive(true);
        }
        if (PlayerPrefs.GetString("username") != null)
        {
            panel_user.SetActive(false);
        }
        OpenLevels();
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    void PlayPanel()
    {
        panel_play.SetActive(true);
    }
    void PlayLeaderBoard()
    {
        SQLCon.instance.LeaderLoad();
        panel_leaderboard.SetActive(true);
    }
    public void PlayLeaderBoardToBack()
    {

        panel_leaderboard.SetActive(false);
    }
    void PlayBackmenu()
    {
        panel_play.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    void OptPanel()
    {
        panel_opt.SetActive(true);
    }
    void QuitOpt()
    {
        panel_opt.SetActive(false);
    }
    void SelectLevel(string text)
    {
        
        int b = int.Parse(text);
        SceneManager.LoadScene(b);
        gameObject.SetActive(false);
    }
    public void OpenLevels()
    {
        int a = PlayerPrefs.GetInt("Comp_Levels",1);
        if(a < 40)
        {
            for(int i = 0; i< a; i++)
            {
                buttons2[i].interactable = true;
            }
        }
        else
        {
            for (int i = 0; i < 40; i++)
            {
                buttons2[i].interactable = true;
            }
        }
        
    }
 

}
