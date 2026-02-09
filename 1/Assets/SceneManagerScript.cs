using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour //скрипт, управляющий сценой
{
    [SerializeField] int nextSceneIndex = 0;    //индекс сцены к которой следует перейти далее

    public void frezeScene()    //заморозить сцену (остановить всё движение)
        => Time.timeScale = 0.0f;

    public void unfrezeScene()  //разморозить сцену
        => Time.timeScale = 1.0f;

    public void loadNextScene() //загрузить следующую сцену
    {
        unfrezeScene();
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void reloadScene()   //перезагрузить текущую сцену
    {
        unfrezeScene();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}