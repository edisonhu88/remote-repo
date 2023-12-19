using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class LoginMgr : MonoBehaviour
{
    public InputField Name;
    private string username;
    public InputField Pass;
    private string password;
    private static object tmp;
    private void Start()
    {
        //PlayerPrefs.DeleteAll();
    }



    public void LoadGame()
    {
        PlayerPrefs.SetInt("Level", 0);

        username = Name.GetComponent<InputField>().text;
        password = Pass.GetComponent<InputField>().text;
        if (username !="" && password!="")
        {
            if (SQLController.InquireMysql<string>(username, "bird")!=null)
            {
                if (SQLController.InquireMysql<string>(username, "bird") != password)
                {
                    print("密码错误");
                    return;
                }
                PlayerPrefs.SetString("username", username);
                print("登录成功");
                SceneManager.LoadSceneAsync("Level" + (PlayerPrefs.GetInt("Level") + 1));
                return;

            }
            print("新用户登录");
            SQLController.InsertSQL(username, password);
            PlayerPrefs.SetString("username", username);
            SceneManager.LoadSceneAsync("Level" + (PlayerPrefs.GetInt("Level") + 1));
            
        }
        return;
    }
  


    
}
