using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class Popups : MonoBehaviour
{
    public string path = "/get_terms";
    public GameObject root;
    public Animator anim;
    public bool useURL = true;
    public TextMeshProUGUI textContent;
    void Start()
    {
        root.SetActive(false);
        if(useURL)
            StartCoroutine(GetInformation());
    }
    private IEnumerator GetInformation()
    {
        string url = TransportData.webServer + path;

        using (UnityWebRequest web = UnityWebRequest.Get(url))
        {
            yield return web.SendWebRequest();
            if ((web.result == UnityWebRequest.Result.ConnectionError && web.result == UnityWebRequest.Result.ProtocolError))
            {
                Debug.LogError("No se pudo conectar.");
                textContent.text = "Error! The requested content could not be downloaded.";
            }
            else
            {
                Debug.Log("Resources: " + web.downloadHandler.text);
                textContent.text = "" + web.downloadHandler.text;
            }
        }
    }
    public void ShowScreen()
    {
        root.SetActive(true);
        anim.SetTrigger("enter");
    }
    public void HideScreen()
    {
        anim.SetTrigger("exit");
        Invoke(nameof(Dismiss), 1.1f);
    }
    private void Dismiss()
    {
        root.SetActive(false);
    }
}
