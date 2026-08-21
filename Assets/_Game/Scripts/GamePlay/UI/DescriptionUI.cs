using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.GamePlay.UI
{
public class DescriptionUI: MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;

    private void Awake()
    {
        Hide();
    }

    public void SetDescription(Sprite image, string itemName, string description)
    {
        _image.sprite = image;
        _name.text = itemName;
        _description.text = description;

        Show();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
}