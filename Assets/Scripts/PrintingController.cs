using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrintingController : MonoBehaviour
{
    public static PrintingController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this);
    }
    public void PrintScreenshot()
    {
        StartCoroutine(Print());
    }

    IEnumerator Print()
    {
        Screen.SetResolution(2970, 2100, FullScreenMode.Windowed);
        SceneManager.LoadScene("Print");
        yield return new WaitForSeconds(0.1f);
        string path = Application.streamingAssetsPath + "\\screenshot.png";
        ScreenCapture.CaptureScreenshot(path);
        yield return new WaitForSeconds(0.2f);
        System.Diagnostics.Process.Start("mspaint", $"\"{path}\"");
        SceneManager.LoadScene("Menu");
        Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
    }
}
