using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] IrisWipeController irisWipeController;
    [SerializeField] Transform player;

    void Start()
    {
        irisWipeController.StartIrisOpen(player);
    }

    public void RestartLevel()
    {
        irisWipeController.StartIrisWipe(player);
        StartCoroutine(RestartRoutine());
    }

    IEnumerator RestartRoutine()
    {
        yield return new WaitForSeconds(irisWipeController.Duration);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
