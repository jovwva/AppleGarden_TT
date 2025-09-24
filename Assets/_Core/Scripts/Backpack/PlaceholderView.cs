using UnityEngine;
using UnityEngine.UI;
public class PlaceholderView : MonoBehaviour
{
    [SerializeField] private Image  iconImage;
    [SerializeField] private Sprite emptyIcon;

    private void Awake()
    {
        Reset();
    }

    public void Reset() => ChangeIcon(emptyIcon);
    public void SetIcon(Sprite sprite) => ChangeIcon(sprite);

    private void ChangeIcon(Sprite sprite)
    {
        iconImage.sprite = sprite;
    }
}
