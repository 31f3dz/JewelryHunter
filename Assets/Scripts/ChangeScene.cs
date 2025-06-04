using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;

    public void Load()
    {
        // SceneManagerクラスが持っているメソッドを使ってシーン切り替え
        SceneManager.LoadScene(sceneName);
    }
}
