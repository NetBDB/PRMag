using UnityEngine;
using UnityEngine.UI;
using TMPro;




public class ProductInfoManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI productNameText;
    public TextMeshProUGUI productDescriptionText;
    public Image productImage;
    public TextMeshProUGUI quantityText;
    public Button minusButton;
    public Button plusButton;
    public Button addToCartButton;
    public Button backButton;

    private ProductData currentProduct;
    private int quantity = 1;
    private CartManager cartManager;
    private NavigationManager navigationManager;


    /// Инициализация менеджера информации о товаре

    private void Start()
    {
        cartManager = FindObjectOfType<CartManager>();
        navigationManager = FindObjectOfType<NavigationManager>();

        minusButton.onClick.AddListener(DecreaseQuantity);
        plusButton.onClick.AddListener(IncreaseQuantity);
        addToCartButton.onClick.AddListener(AddToCart);
        backButton.onClick.AddListener(GoBack);

        UpdateQuantityUI();
    }

    /// Устанавливает текущий товар для отображения

    public void SetProduct(ProductData product)
    {
        currentProduct = product;
        quantity = 1;

        productNameText.text = product.name;
        productDescriptionText.text = product.description;

        if (product.image != null)
        {
            productImage.sprite = product.image;
        }

        UpdateQuantityUI();
        UpdateAddToCartButton();
    }


    /// Увеличивает количество товара

    private void IncreaseQuantity()
    {
        quantity++;
        UpdateQuantityUI();
        UpdateAddToCartButton();
    }


    /// Уменьшает количество товара

    private void DecreaseQuantity()
    {
        if (quantity > 1)
        {
            quantity--;
            UpdateQuantityUI();
            UpdateAddToCartButton();
        }
    }


    /// Обновляет отображение количества

    private void UpdateQuantityUI()
    {
        quantityText.text = quantity.ToString();
    }


    /// Обновляет текст кнопки добавления в корзину

    private void UpdateAddToCartButton()
    {
        if (currentProduct != null)
        {
            int totalPrice = currentProduct.price * quantity;
            addToCartButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Добавить в корзину ({totalPrice})";
        }
    }


    /// Добавляет товар в корзину

    private void AddToCart()
    {
        if (currentProduct != null)
        {
            cartManager.AddToCart(currentProduct, quantity);
            navigationManager.ShowProductsScreen();
        }
    }


    /// Возвращает к экрану товаров

    private void GoBack()
    {
        navigationManager.ShowProductsScreen();
    }
}