using UnityEngine;
using UnityEngine.UI;


/// Управляет навигацией между экранами приложения

public class NavigationManager : MonoBehaviour
{
    [Header("Экраны")]
    public GameObject mainScreen;
    public GameObject productsScreen;
    public GameObject productInfoScreen;
    public GameObject cartScreen;

    [Header("Кнопки навигации")]
    public Button homeButton;
    public Button productsButton;
    public Button cartButton;


    /// Инициализация менеджера навигации

    private void Start()
    {
        homeButton.onClick.AddListener(() => ShowScreen(mainScreen));
        productsButton.onClick.AddListener(() => ShowScreen(productsScreen));
        cartButton.onClick.AddListener(() => ShowScreen(cartScreen));

        ShowScreen(mainScreen);
    }


    /// Показывает указанный экран и скрывает остальные

    public void ShowScreen(GameObject screenToShow)
    {
        mainScreen.SetActive(false);
        productsScreen.SetActive(false);
        productInfoScreen.SetActive(false);
        cartScreen.SetActive(false);

        screenToShow.SetActive(true);
    }


    /// Показывает главный экран

    public void ShowMainScreen()
    {
        ShowScreen(mainScreen);
    }


    /// Показывает экран товаров

    public void ShowProductsScreen()
    {
        ShowScreen(productsScreen);
    }

    /// Показывает экран корзины

    public void ShowCartScreen()
    {
        ShowScreen(cartScreen);
    }


    /// Показывает экран информации о товаре

    public void ShowProductInfoScreen()
    {
        ShowScreen(productInfoScreen);
    }
}