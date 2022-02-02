using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScenes : MonoBehaviour
{
    private static TransitionScenes instance;
    [SerializeField] private Animator _animator;
    private const float delayEnter = 1;
    private const float delayExit = 1.6f;
    private string loadSceneName = "";

    public static TransitionScenes Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TransitionScenes>();
                if(instance == null)
                {
                    instance = Instantiate(Resources.Load<TransitionScenes>("Prefabs/general/TransitionCanvas"));
                }
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void ChangeScene(string sceneName)
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        if(string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("****** Se quiere cambiar a una escena con el nombre vacio");
            return;
        }
        if (SceneManager.GetSceneByName(sceneName) == null)
        {
            Debug.LogError("******* El nombre de la scena no esta puesto en el build o esta mal escrito y no corresponde a ninguna scena");
            return;
        }
        loadSceneName = sceneName;
        this.gameObject.SetActive(true);
        //_animator.Update(0);
        _animator.Rebind();
        StartCoroutine(DelayEnter());
    }
    IEnumerator DelayEnter()
    {
        yield return new WaitForSeconds(delayEnter);
        _animator.SetTrigger("Enter");
        AsyncOperation operation = SceneManager.LoadSceneAsync(loadSceneName);
        while (!operation.isDone)
        {
            yield return null;
        }
        GetComponent<Canvas>().worldCamera = Camera.main;
        _animator.SetTrigger("Exit");
        yield return new WaitForSeconds(delayExit);
        Destroy(gameObject);
    }
}
