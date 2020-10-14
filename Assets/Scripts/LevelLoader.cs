using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelLoader : MonoBehaviour {

    [SerializeField] private Animator _Animator;
    [SerializeField] private float _WaitTime;
    [SerializeField] private TextMeshProUGUI _Text_Day;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            LoadNextScene();
        }
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex+1));
    }

    IEnumerator LoadScene(int levelindex)
    {
        _Text_Day.text = "Day " + (levelindex).ToString();
        _Animator.SetTrigger("Start");
        yield return new WaitForSeconds(_WaitTime);
        SceneManager.LoadScene(levelindex);
    }
}
