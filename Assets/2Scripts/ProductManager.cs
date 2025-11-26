using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI; 


/// Управляет отображением и взаимодействием с товарами

public class ProductManager : MonoBehaviour
{
    [Header("Настройки")]
    public List<ProductData> products;
    public GameObject productCardPrefab;
    public Transform productsGrid;

    [Header("Экран информации о товаре")]
    public GameObject productInfoScreen;
    public TextMeshProUGUI productNameText;
    public TextMeshProUGUI productDescriptionText;
    public TextMeshProUGUI productPriceText;
    public Image productImage; 
    public ProductInfoManager productInfoManager;

    private NavigationManager navigationManager;


    /// Инициализация менеджера товаров

    private void Start()
    {
        navigationManager = FindObjectOfType<NavigationManager>();
        GenerateProductCards();
    }

    /// Создает карточки товаров на основе данных

    private void GenerateProductCards()
    {
        foreach (ProductData product in products)
        {
            GameObject card = Instantiate(productCardPrefab, productsGrid);
            ProductCard cardScript = card.GetComponent<ProductCard>();
            cardScript.Initialize(product, this);
        }
    }


    /// Показывает информацию о выбранном товаре

    public void ShowProductInfo(ProductData product)
    {
        productNameText.text = product.name;
        productDescriptionText.text = product.description;
        productPriceText.text = $"Цена: {product.price}";

        if (product.image != null)
        {
            productImage.sprite = product.image;
        }

        productInfoManager.SetProduct(product);
        navigationManager.ShowProductInfoScreen();
    }
}