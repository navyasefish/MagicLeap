using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public GameObject videoPrefab; // A quad with VideoPlayer component
    public string[] videoPaths;    // 0 = StreamingAssets, 1 = URL
    private int currentIndex = 0;

    private GameObject currentVideoObject;
    private VideoPlayer videoPlayer;
    private bool isPlaying = false;

    void Start()
    {
        // Optional: preload if needed
    }

    public void PlayPause()
    {
        if (!isPlaying)
        {
            CreateAndPlay(videoPaths[currentIndex]);
        }
        else
        {
            StopAndDestroy();
        }
    }

    public void Next()
    {
        currentIndex = (currentIndex + 1) % videoPaths.Length;
        CreateAndPlay(videoPaths[currentIndex]);
    }

    public void Previous()
    {
        currentIndex = (currentIndex - 1 + videoPaths.Length) % videoPaths.Length;
        CreateAndPlay(videoPaths[currentIndex]);
    }

    private void CreateAndPlay(string path)
    {
        StopAndDestroy();

        currentVideoObject = Instantiate(videoPrefab, Camera.main.transform.position + Camera.main.transform.forward * 2f, Quaternion.identity);
        videoPlayer = currentVideoObject.GetComponent<VideoPlayer>();

        if (path.StartsWith("http"))
            videoPlayer.url = path;
        else
            videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, path);

        videoPlayer.Play();
        isPlaying = true;
    }

    private void StopAndDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
        }

        if (currentVideoObject != null)
        {
            Destroy(currentVideoObject);
        }

        isPlaying = false;
    }
}
