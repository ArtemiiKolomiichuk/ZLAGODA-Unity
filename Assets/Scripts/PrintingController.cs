using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrintingController : MonoBehaviour
{
    string path = Application.streamingAssetsPath + "\\screenshot.png";
    [SerializeField] private GameObject[] elementsToHide;

    public void OpenScreenshotPaint()
    {
        StartCoroutine(Paint());
    }

    public void PrintScreenshot()
    {
        StartCoroutine(Print());
    }

    public void OpenScreenshotExplorer()
    {
        StartCoroutine(Explorer());
    }

    private void HideElements()
    {
        foreach (GameObject element in elementsToHide)
        {
            element.SetActive(false);
        }
    }

    private void ShowElements()
    {
        foreach (GameObject element in elementsToHide)
        {
            element.SetActive(true);
        }
    }

    IEnumerator TakeScreenshot()
    {
        HideElements();
        Screen.SetResolution(1485, 1050, FullScreenMode.Windowed);
        yield return new WaitForSecondsRealtime(0.1f);
        ScreenCapture.CaptureScreenshot(path);
        yield return new WaitForSecondsRealtime(0.1f);
        Screen.SetResolution(1366, 768, FullScreenMode.Windowed);
        ShowElements();
    }

    IEnumerator Explorer()
    {
        yield return TakeScreenshot();
        System.Diagnostics.Process.Start($"\"{path}\"");
    }

    IEnumerator Paint()
    {
        yield return TakeScreenshot();
        System.Diagnostics.Process.Start("mspaint", $"\"{path}\"");
    }

    IEnumerator Print()
    {
        yield return TakeScreenshot();
        System.Diagnostics.Process.Start("mspaint", $"/p \"{path}\"");
    }






    public void ShowNextPage()
    {
        Debug.Log("ShowNextPage");
    }
}
