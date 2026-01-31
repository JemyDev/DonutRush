using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIOrderLineController : MonoBehaviour
{
    [SerializeField] private Image _orderLineImage;
    [SerializeField] private TMP_Text _orderLineQuantityText;
    
    public void SetSprite(Sprite sprite)
    {
        _orderLineImage.sprite = sprite;
    }

    public void UpdateQuantity(int quantity)
    {
        _orderLineQuantityText.text = quantity.ToString();
    }
}