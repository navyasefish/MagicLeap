using UnityEngine;
using UnityEngine.UI;

public class TogglePlayPause : MonoBehaviour
{
    public Sprite playSprite;
    public Sprite pauseSprite;
    public Image buttonImage;

    private bool isPlaying = false;

    public void ToggleButton()
    {
        isPlaying = !isPlaying;
        buttonImage.sprite = isPlaying ? pauseSprite : playSprite;

        // Add video control logic here
    }
}
