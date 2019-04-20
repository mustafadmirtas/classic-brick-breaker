using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lang : MonoBehaviour
{
    public Button[] buttons = new Button[3];
    public Sprite[] tr = new Sprite[4], en = new Sprite[4];
    Text[] text;
    string[] en_text, tr_text;
    MenuScript scriptMenu;
    public Button button_tr,button_eng;
    public GameObject panel_lang;
    public Image image_lvl;
    // Start is called before the first frame update
    void Start()
    {
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
        if (PlayerPrefs.GetInt("Lang", 0) == 1)
        {
            buttons[0].image.sprite = tr[0];
            buttons[1].image.sprite = tr[1];
            buttons[2].image.sprite = tr[2];
            image_lvl.sprite = tr[3];
            image_lvl.rectTransform.localScale = new Vector3(1,2);
        }
        if (PlayerPrefs.GetInt("Lang", 0) == 2)
        {
            buttons[0].image.sprite = en[0];
            buttons[1].image.sprite = en[1];
            buttons[2].image.sprite = en[2];
            image_lvl.sprite = en[3];
            image_lvl.rectTransform.localScale = new Vector3(1, 1);
        }
    }
    void SelectLang(int a)
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
    }
    void Texts()
    {
        tr_text[0] = "Skor";
        tr_text[1] = "Congrats";
        tr_text[2] = "Bölümü Tamamladınız";
        tr_text[3] = "Sonraki Bölüme Geç";
        tr_text[4] = "Oyun Bitti";
        tr_text[5] = "Tekrar Deneyin";
        tr_text[6] = "Skorunuz";
        tr_text[7] = "Tekrar Dene";
        tr_text[8] = "Reklam İle Devam Et";
        tr_text[9] = "Çıkış";

        en_text[0] = "Skor";
        en_text[1] = "Congrats";
        en_text[2] = "Bölümü Tamamladınız";
        en_text[3] = "Sonraki Bölüme Geç";
        en_text[4] = "Oyun Bitti";
        en_text[5] = "Tekrar Deneyin";
        en_text[6] = "Skorunuz";
        en_text[7] = "Tekrar Dene";
        en_text[8] = "Reklam İle Devam Et";
        en_text[9] = "Çıkış";

    }

}
