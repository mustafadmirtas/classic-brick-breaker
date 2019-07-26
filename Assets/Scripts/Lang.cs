using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lang : MonoBehaviour
{
    public static Lang instance;
    public Text[] text;
    public Button button_tr,button_eng;
    public GameObject panel_lang;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        button_tr.onClick.AddListener(() => { SelectLang(1); });
        button_eng.onClick.AddListener(() => { SelectLang(2); });
        LangChecker();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LangChecker()
    {
        if (PlayerPrefs.GetInt("Lang", 1) == 1)
        {
            text[0].text = "Bölümler";
            text[1].text = "Çıkış";
            text[2].text = "Dil";
            text[3].text = "Sesler";
            if(PlayerPrefs.GetInt("Sound", 1) == 1)
            {
                text[4].text = "Açık";
            }
            else
            {
                text[4].text = "Kapalı";
            }
            text[5].text = "Geri Dön";
            text[6].text = "Kalan Taş Sayısı";
            text[7].text = "Tebrikler!";
            text[8].text = "Bölüm Tamamlandı";
            text[9].text = "Sonraki Bölüm";
            text[10].text = "Çıkış";
            text[11].text = "Bölüm Sona Erdi";
            text[12].text = "Tekrar Deneyin";
            text[13].text = "Tekrar Deneyin";
            text[14].text = "Reklam İle Devam Et";
            text[15].text = "Çıkış";
            text[16].text = "Oyna";
            text[17].text = "Ayarlar";
            text[18].text = "Çıkış";
            text[19].text = "Lider Tablosu";
            text[20].text = "Lider Tablosu";
            text[21].text = "Kullanıcı Adı";
            text[22].text = "Bölüm";

        }
        if (PlayerPrefs.GetInt("Lang", 1) == 2)
        {
            text[0].text = "Levels";
            text[1].text = "Exit";
            text[2].text = "Language";
            text[3].text = "Sounds";
            if (PlayerPrefs.GetInt("Sound", 1) == 1)
            {
                text[4].text = "On";
            }
            else
            {
                text[4].text = "Off";
            }
            text[5].text = "Go Back";
            text[6].text = "Bricks Remaining";
            text[7].text = "Congratulations!";
            text[8].text = "Level Completed";
            text[9].text = "Next Level";
            text[10].text = "Exit";
            text[11].text = "Game Over!";
            text[12].text = "Try Again";
            text[13].text = "Try Again";
            text[14].text = "Continue with Ads";
            text[15].text = "Quit";
            text[16].text = "Play";
            text[17].text = "Optıons";
            text[18].text = "Quıt";
            text[19].text = "Leaderboard";
            text[20].text = "Leaderboard";
            text[21].text = "Username";
            text[22].text = "Level";
        }
    }
    public void SelectLang(int a)
    {
        if (a == 1)
        {
            PlayerPrefs.SetInt("Lang", 1);
            LangChecker();
        }
        else
        {
            PlayerPrefs.SetInt("Lang", 2);
            LangChecker();
        }
        panel_lang.SetActive(false);
        LangChecker();
    }
    

}
