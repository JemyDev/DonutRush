using UnityEngine;
using UnityEngine.UI;

public class UIOrderLineController : MonoBehaviour
{
    [SerializeField] private Image _orderLineImage;
    
    public void AddSprite(Sprite sprite)
    {
        _orderLineImage.sprite = sprite;
    }
}