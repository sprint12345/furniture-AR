using UnityEngine;
using UnityEngine.SceneManagement;

public class ARSceneManager : MonoBehaviour
{
    public void LoadToHomeScene()
    {
        SceneManager.LoadScene("Home"); // Αντικατέστησε "HomeScene" με το όνομα της σκηνής σου
    }
}
