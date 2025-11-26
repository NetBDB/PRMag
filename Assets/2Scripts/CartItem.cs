using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CartItem : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemPriceText;
    public TextMeshProUGUI quantityText;
    public Button minusButton;
    public Button plusButton;
    public Image itemImage;

    private ProductData productData;
    private int quantity;
    private CartManager cartManager;


    public void Initialize(ProductData data, int initialQuantity, CartManager manager)
    {
        productData = data;
        quantity = initialQuantity;
        cartManager = manager;
        itemImage.sprite = data.image;

        itemNameText.text = data.name;
        itemPriceText.text = $"Цена: {data.price}";
        quantityText.text = quantity.ToString();

        minusButton.onClick.AddListener(DecreaseQuantity);
        plusButton.onClick.AddListener(IncreaseQuantity);
    }


    private void IncreaseQuantity()
    {
        quantity++;
        quantityText.text = quantity.ToString();
        cartManager.AddToCart(productData, 1);
    }


    private void DecreaseQuantity()
    {
        if (quantity > 1)
        {
            quantity--;
            quantityText.text = quantity.ToString();
            cartManager.RemoveFromCart(productData, 1);
        }
        else
        {
            cartManager.RemoveFromCart(productData, 1);
            Destroy(gameObject);
        }
    }
}