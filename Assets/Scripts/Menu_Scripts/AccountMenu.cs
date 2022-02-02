using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.Networking;
using DG.Tweening;
using UnityEngine.UI;

public class AccountMenu : MonoBehaviour
{
    [Header("Create Account")]
    public TMP_InputField accountCA;
    public TMP_InputField emailCA;
    public TMP_InputField passwordCA;
    public TMP_InputField confirmPasswordCA;

    [Header("")]
    [Header("Login")]
    public TMP_InputField emailLogin;
    public TMP_InputField passwordLogin;
    public GameObject btnLogin;
    [Header("Forgot")]
    public TMP_InputField emailForgot;

    [Header("")]
    public ErrorFB[] errorFbs;
    public loginData userData;

    [System.Serializable]
    public struct loginData
    {
        public string access_token,id,username,email;
    }
    [System.Serializable]
    public struct ErrorFB
    {
        public string id;
        public Animator anim;
        public TextMeshProUGUI text;
    }
    #region Register
    public void CreateAccountBtn()
    {
        if (InputNull())
            return;
        if(passwordCA.text != confirmPasswordCA.text)
        {
            FeedBackError("confirmPassword");
            return;
        }
        string[] email = emailCA.text.Split("@");
        if (email.Length < 2 || email[0].Length < 5)
        {
            FeedBackError("email");
            return;
        }
        StartCoroutine(Register());
    }

    private IEnumerator Register()
    {
        string url = TransportData.webServer + "/register";
        WWWForm form = new WWWForm();
        form.AddField("username", accountCA.text);
        form.AddField("email", emailCA.text.ToLower());
        form.AddField("password", passwordCA.text);
        using (UnityWebRequest web = UnityWebRequest.Post(url, form))
        {
            yield return web.SendWebRequest();
            if ((web.result == UnityWebRequest.Result.ConnectionError && web.result == UnityWebRequest.Result.ProtocolError))
            { 
                FeedBackError("conexion");
            }
            else
            {
                if (!ErrorCredencials(web.downloadHandler.text))
                {
                    loginData myData = JsonUtility.FromJson<loginData>(web.downloadHandler.text);
                    TransportData.access_token = myData.access_token;
                    TransportData.namePlayer = myData.username;
                    SaveLoadJsons.Instance.ExecuteAll(true);
                    TransitionScenes.Instance.ChangeScene("GameMenu");
                    Debug.Log("CreateAccount: " + myData.username);
                }
                userData = JsonUtility.FromJson<loginData>(web.downloadHandler.text);
            }
        }
    }
    #endregion
    #region Login
    public void LoginBtn()
    {
        if (   InputNull())
            return;
        string[] email = emailLogin.text.Split("@");
        if(email.Length < 2 || email[0].Length < 5)
        {
            FeedBackError("email");
            return;
        }
        StartCoroutine(Login());
    }

    private IEnumerator Login()
    {
        string url = TransportData.webServer + "/login";
        WWWForm form = new WWWForm();

        form.AddField("email", emailLogin.text.ToLower());
        form.AddField("password", passwordLogin.text);

        using (UnityWebRequest web = UnityWebRequest.Post(url, form))
        {
            yield return web.SendWebRequest();
            if ((web.result == UnityWebRequest.Result.ConnectionError && web.result == UnityWebRequest.Result.ProtocolError))
            {
                FeedBackError("conexion");
            }
            else
            {
                print("Resources: " + web.downloadHandler.text);
                if (!ErrorCredencials(web.downloadHandler.text))
                {
                    loginData myData = JsonUtility.FromJson<loginData>(web.downloadHandler.text);
                    TransportData.access_token = myData.access_token;
                    TransportData.namePlayer = myData.username;
                    btnLogin.SetActive(false);
                    SaveLoadJsons.Instance.ExecuteAll(false);
                    TransitionScenes.Instance.ChangeScene("GameMenu");
                }
                userData = JsonUtility.FromJson<loginData>(web.downloadHandler.text);
            }
        }
    }
    #endregion
    #region ForgotThePassword
    public void ForgotBtn()
    {
        if (InputNull())
        {
            return;
        }
        string[] email = emailLogin.text.Split("@");
        if (email.Length < 2 || email[0].Length < 5)
        {
            FeedBackError("email");
            return;
        }
        StartCoroutine(Forgot());
    }

    private IEnumerator Forgot()
    {
        string url = TransportData.webServer + "/forgot_password";
        WWWForm form = new WWWForm();

        form.AddField("email", emailForgot.text.ToLower());

        using (UnityWebRequest web = UnityWebRequest.Post(url, form))
        {
            yield return web.SendWebRequest();
            if ((web.result == UnityWebRequest.Result.ConnectionError && web.result == UnityWebRequest.Result.ProtocolError))
            {
                FeedBackError("conexion");
            }
            else
            {
                if(!ErrorCredencials(web.downloadHandler.text))
                {
                    FeedBackError("forgot");
                }
                Debug.Log("Resources: " + web.downloadHandler.text);
            }
        }
    }
    #endregion
    #region logout
    public void LogoutBtn()
    {
        if (string.IsNullOrEmpty(TransportData.access_token))
        {
            
            return;
        }
        else
        {
            emailLogin.text = "";
            passwordLogin.text = "";
            TransportData.access_token = "";
            btnLogin.SetActive(true);
            StartCoroutine(Logout());
        }
    }
    private IEnumerator Logout()
    {
        string url = TransportData.webServer + "/logout";
        WWWForm form = new WWWForm();

        form.AddField("access_token", TransportData.access_token);

        using (UnityWebRequest web = UnityWebRequest.Post(url, form))
        {
            yield return web.SendWebRequest();
            if ((web.result == UnityWebRequest.Result.ConnectionError && web.result == UnityWebRequest.Result.ProtocolError))
            {
                FeedBackError("conexion");
            }
            else
            {
                print("Resources: " + web.downloadHandler.text);
                userData.access_token = "";
                userData.username = "Player " + UnityEngine.Random.Range(0, 99999);
                TransportData.access_token = "";
                btnLogin.SetActive(true);
                string path = Application.persistentDataPath + "/initialLoggin.json";
                File.Delete(path);
                var paths = SaveLoadJsons.Instance.GetPaths();
                foreach (var p in paths)
                {
                    File.Delete(p.Value);
                }
            }
        }
    }
    #endregion
    #region error
    private bool InputNull()
    {
        bool b = false;
        foreach (var e in errorFbs)
        {
            if (e.text.text.Length < 5)
            {
                e.anim.SetTrigger("error");
                b = true;
            }
        }
        return b;
    }
    private void FeedBackError(string id)
    {
        foreach (var e in errorFbs)
        {
            if (e.id == id)
            {
                e.anim.SetTrigger("error");
                return;
            }
        }
    }
    private void FeedBackError()
    {
        foreach (var e in errorFbs)
        {
            e.anim.SetTrigger("error");
        }
    }
    private bool ErrorCredencials(string code)
    {
        var array = code.Split(",");
        var first = array[0].Split(":");
        if(first[1].Contains("ERROR"))
        {
            FeedBackError("credentials");
            return true;
        }
        return false;
    }
    public void BackButtonPressed()
    {
        TransitionScenes.Instance.ChangeScene("scene_menu");
    }
    #endregion

    private void Start()
    {
        DOTween.Init();
    }
}
