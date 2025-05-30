using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static GameObject prefabToLoad;

    public void LoadScene(string sceneName)
    {
        prefabToLoad = CatalogManager.selectedPrefab;
        SceneManager.LoadScene(sceneName);
    }
}
