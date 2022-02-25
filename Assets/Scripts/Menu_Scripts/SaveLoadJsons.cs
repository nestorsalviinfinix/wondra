using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System;

public class SaveLoadJsons : MonoBehaviour
{
    private static SaveLoadJsons instance;
    private Dictionary<string, string> paths = new Dictionary<string, string>();

    public static SaveLoadJsons Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SaveLoadJsons>();
                if (instance == null)
                {
                    GameObject aux = Instantiate(Resources.Load<GameObject>("Prefabs/general/LoadSaveJson"));
                    instance = aux.GetComponent<SaveLoadJsons>();
                }
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        paths.Add("cards", Application.persistentDataPath + "/cards.json");
    }
    public Dictionary<string,string> GetPaths()
    {
        return paths;
    }

    [System.Serializable]
    public struct OneJsonData
    {
        public string type;
        public string user_id;
        public string game_id;
        public string json;
        public string id;
    }
    public void Execute(bool save, string id)
    {
        if (string.IsNullOrEmpty(TransportData.access_token))
        {
            Debug.Log("No hay usuario loggeado.");
            return;
        }
        if(!paths.ContainsKey(id))
        {
            Debug.Log("La id colocada no esta en el diccionario de direcciones");
            return;
        }
        if (save)
            StartCoroutine(SaveData(id));
        else
            StartCoroutine(LoadData(id));
    }
    public void ExecuteAll(bool save)
    {
        if (string.IsNullOrEmpty(TransportData.access_token))
        {
            Debug.Log("No hay usuario loggeado.");
            return;
        }
        foreach (var dic in paths)
        {
            if (save)
                StartCoroutine(SaveData(dic.Key));
            else
                StartCoroutine(LoadData(dic.Key));
        }
    }

    private IEnumerator SaveData(string id)
    {
        string url = TransportData.webServer + "/add_json";
        WWWForm form = new WWWForm();

        string _currentPath = paths[id];
        if (!File.Exists(_currentPath))
        {
            Debug.LogError("****** El path solicitado no existe");
        }else
        {
            form.AddField("access_token", TransportData.access_token);
            print(TransportData.access_token);
            //form.AddField("type", type);
            string jsonInside = File.ReadAllText(_currentPath);

            form.AddField("json", jsonInside);
            print("jsonInside: " + jsonInside);

            form.AddField("type", "wondra");
            form.AddField("game_id", id);


            using (UnityWebRequest web = UnityWebRequest.Post(url, form))
            {
                yield return web.SendWebRequest();
                if ((web.result == UnityWebRequest.Result.ConnectionError && web.result == UnityWebRequest.Result.ProtocolError))
                {
                    Debug.LogError("Error en el request");
                }
                else
                {
                    Debug.Log("****** Save Response: " + web.downloadHandler.text);
                    //data = JsonUtility.FromJson<OneJsonData>(web.downloadHandler.text);
                }
            }
        }
    }

    private IEnumerator LoadData(string id)
    {
        string url = TransportData.webServer + "/get_json";
        WWWForm form = new WWWForm();

        string _currentPath = paths[id];

        form.AddField("access_token", TransportData.access_token);
        form.AddField("game_id", id);

        using (UnityWebRequest web = UnityWebRequest.Post(url, form))
        {
            yield return web.SendWebRequest();
            if ((web.result == UnityWebRequest.Result.ConnectionError && web.result == UnityWebRequest.Result.ProtocolError))
            {
                Debug.LogError("Error en el request");
            }
            else
            {
                Debug.Log("***------*** Load Response id " + id + " -> " + web.downloadHandler.text);
                if (web.downloadHandler.text == "null")
                    yield break;

                OneJsonData dataWeb = JsonUtility.FromJson<OneJsonData>(web.downloadHandler.text);
                File.WriteAllText(_currentPath, dataWeb.json);
            }
        }
    }
}
