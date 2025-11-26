using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;


/// Управляет корзиной покупок

public class CartManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Transform cartItemsContainer;
    public TextMeshProUGUI totalPriceText;
    public Button checkoutButton;
    public Button clearCartButton;

    [Header("Префабы")]
    public GameObject cartItemPrefab;

    private Dictionary<ProductData, int> cartItems = new Dictionary<ProductData, int>();
    private int totalPrice = 0;


    /// Инициализация менеджера корзины

    private void Start()
    {
        checkoutButton.onClick.AddListener(OnCheckout);
        clearCartButton.onClick.AddListener(ClearCart);
        UpdateCartUI();
    }


    /// Добавляет товар в корзину


    public void AddToCart(ProductData product, int quantity = 1)
    {
        if (cartItems.ContainsKey(product))
        {
            cartItems[product] += quantity;
        }
        else
        {
            cartItems[product] = quantity;
        }

        UpdateCartUI();
    }


    /// Удаляет товар из корзины

    public void RemoveFromCart(ProductData product, int quantity = 1)
    {
        if (cartItems.ContainsKey(product))
        {
            cartItems[product] -= quantity;
            if (cartItems[product] <= 0)
            {
                cartItems.Remove(product);
            }
        }

        UpdateCartUI();
    }


    /// Обновляет интерфейс корзины

    private void UpdateCartUI()
    {
        foreach (Transform child in cartItemsContainer)
        {
            Destroy(child.gameObject);
        }

        totalPrice = 0;

        foreach (var item in cartItems)
        {
            GameObject cartItem = Instantiate(cartItemPrefab, cartItemsContainer);
            CartItem cartItemScript = cartItem.GetComponent<CartItem>();
            cartItemScript.Initialize(item.Key, item.Value, this);

            totalPrice += item.Key.price * item.Value;
        }

        totalPriceText.text = $"Итого: {totalPrice}";
        checkoutButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Оформить ({totalPrice})";

        foreach (Transform child in cartItemsContainer)
        {
            Destroy(child.gameObject);
        }

        float startY = 630f; 
        float itemHeight = 80f;
        float spacing = 250f; 

        foreach (var item in cartItems)
        {
            GameObject cartItem = Instantiate(cartItemPrefab, cartItemsContainer);


            RectTransform rect = cartItem.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(0, startY);
            startY -= (itemHeight + spacing); 

            CartItem cartItemScript = cartItem.GetComponent<CartItem>();
            cartItemScript.Initialize(item.Key, item.Value, this);

            totalPrice += item.Key.price * item.Value;
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(cartItemsContainer as RectTransform);
    }


    /// Обрабатывает оформление заказа

    private void OnCheckout()
    {
        Debug.Log("Заказ оформлен! Состав заказа:");
        foreach (var item in cartItems)
        {
            Debug.Log($"{item.Key.name} x{item.Value} - {item.Key.price * item.Value} руб.");
        }
        Debug.Log($"Общая стоимость: {totalPrice} руб.");

        ClearCart();
    }


    /// Очищает корзину

    private void ClearCart()
    {
        cartItems.Clear();
        UpdateCartUI();
    }
}