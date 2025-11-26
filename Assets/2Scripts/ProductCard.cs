using UnityEngine;
using UnityEngine.UI;
using TMPro;


/// Управляет отображением карточки товара

public class ProductCard : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI productNameText;
    public TextMeshProUGUI productPriceText;
    public Button cardButton;
    public Image productImage;

    private ProductData productData;
    private ProductManager productManager;


    public void Initialize(ProductData data, ProductManager manager)
    {
        productData = data;
        productManager = manager;

        productNameText.text = data.name;
        productPriceText.text = $"Цена: {data.price}";
        productImage.sprite = data.image;

        cardButton.onClick.RemoveAllListeners();
        cardButton.onClick.AddListener(OnCardClick);
    }


    /// Обрабатывает клик по карточке товара

    private void OnCardClick()
    {
        productManager.ShowProductInfo(productData);
    }
}