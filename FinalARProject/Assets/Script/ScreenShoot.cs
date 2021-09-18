using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShoot : MonoBehaviour
{
    [SerializeField]
    GameObject canvas;

    public bool takingScreenshot = false;

    public void CaptureScreenshot()
    {
        if (takingScreenshot)
            return;
        StartCoroutine(TakeScreenshotAndSave());
    }

    private IEnumerator TakeScreenshotAndSave()
    {
        takingScreenshot = true;
        canvas.SetActive(false);

        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        // Save the screenshot to Gallery/Photos
        string name = string.Format("{0}_Capture{1}_{2}.png", Application.productName, "{0}", System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
        Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(ss, Application.productName + " Captures", name));
        takingScreenshot = false;

        Destroy(ss);
        canvas.SetActive(true);

    }
}