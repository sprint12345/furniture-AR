using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CatalogManager : MonoBehaviour
{
    [Header("UI References")]
    public static string lastActivePanel; // Το τελευταίο panel που ήταν ενεργό

    public GameObject checkoutPanel;
    public GameObject SceneManager;
    public GameObject productItemPrefab;
    public Transform content;
    public GameObject detailsPanel;
    public GameObject contactusPanel;
    public GameObject homePanel;
    public TMP_Text detailsItemName;
    public TMP_Text detailsPrice;
    public TMP_Text detailsDescription;
    public Image detailsItemImage;
    public GameObject cartPanel;
    public Transform cartContent; // Container for the cart items
    public GameObject cartItemPrefab; // Prefab for each cart item
    public TMP_Text totalText; // Total price text
    public GameObject favoritePanel; // Panel for displaying favorite items
    public Transform favoriteContent; // Container for favorite items
    public GameObject FavoriteItemPrefab;
    [Header("Product Prefabs")]
    public List<GameObject> productPrefabs;
    public static GameObject selectedPrefab;
    public GameObject profilePanel;
    [Header("Product Data")]
    public List<Product> products = new List<Product>
    {
        new Product("Stylish Chair", 59.99f, "Images/chair2", "A stylish modern chair.", "Chair", "StylishChairPrefab"),
        new Product("Wooden Chair", 49.99f, "Images/chair1", "A comfortable wooden chair.", "Chair", "WoodenChairPrefab"),
        new Product("Gaming Chair", 75.99f, "Images/chair3", "A chair for gamers.", "Chair", "GamingChairPrefab"),
        new Product("Modern Sofa", 299.99f, "Images/sofa1", "A stylish modern sofa.", "Sofa", "ModernSofaPrefab"),
    };

    private List<Product> cartProducts = new List<Product>();
    private List<Product> favoriteProducts = new List<Product>();
    private float totalPrice = 0f;

    [Header("Favorite Icons")]
    public Sprite defaultHeartIcon; // Black heart
    public Sprite activeHeartIcon;  // Red heart

    public void Start()
    {
        FilterProducts("All");
    }

    public void FilterProducts(string category)
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach (var product in products)
        {
            if (category == "All" || product.category == category)
            {
                GameObject item = Instantiate(productItemPrefab, content);

                var itemImage = item.transform.Find("ItemImage").GetComponent<Image>();
                var itemName = item.transform.Find("ItemName").GetComponent<TMP_Text>();
                var itemPrice = item.transform.Find("Price").GetComponent<TMP_Text>();
                var favoriteButton = item.transform.Find("FavoriteButton").GetComponent<Button>();

                itemImage.sprite = Resources.Load<Sprite>(product.imagePath);
                itemName.text = product.name;
                itemPrice.text = $"${product.price:F2}";

                // Ενημέρωση εικονιδίου της καρδιάς
                Image heartIcon = favoriteButton.GetComponent<Image>();
                heartIcon.sprite = favoriteProducts.Contains(product) ? activeHeartIcon : defaultHeartIcon;

                // Προσθήκη Listener για ToggleFavorite
                favoriteButton.onClick.RemoveAllListeners();
                favoriteButton.onClick.AddListener(() =>
                {
                    ToggleFavorite(product, heartIcon);
                });

                Button itemButton = item.GetComponent<Button>();
                if (itemButton != null)
                {
                    itemButton.onClick.AddListener(() => ShowDetails(product));
                }
            }
        }
    }



public void ShowDetails(Product product)
    {
        Debug.Log($"Showing details for: {product.name}");

        // Έλεγχος για null αναφορές πριν τη χρήση
        if (detailsItemName == null)
        {
            Debug.LogError("detailsItemName is NULL. Check if it is assigned in the Inspector.");
            return;
        }
        if (detailsPrice == null)
        {
            Debug.LogError("detailsPrice is NULL. Check if it is assigned in the Inspector.");
            return;
        }
        if (detailsDescription == null)
        {
            Debug.LogError("detailsDescription is NULL. Check if it is assigned in the Inspector.");
            return;
        }
        if (detailsItemImage == null)
        {
            Debug.LogError("detailsItemImage is NULL. Check if it is assigned in the Inspector.");
            return;
        }
        if (detailsPanel == null)
        {
            Debug.LogError("detailsPanel is NULL. Check if it is assigned in the Inspector.");
            return;
        }

        // Ενημέρωση UI με τα στοιχεία του προϊόντος
        detailsItemName.text = product.name;
        detailsPrice.text = $"${product.price:F2}";
        detailsDescription.text = product.description;
        detailsItemImage.sprite = Resources.Load<Sprite>(product.imagePath);

        // Εύρεση και ορισμός του prefab που αντιστοιχεί στο προϊόν
        selectedPrefab = null;
        foreach (GameObject prefab in productPrefabs)
        {
            if (prefab.name == product.prefabName)
            {
                selectedPrefab = prefab;
                break;
            }
        }

        if (selectedPrefab == null)
        {
            Debug.LogError($"Prefab with name {product.prefabName} not found in the productPrefabs list!");
        }

        // Εύρεση του κουμπιού AddToCart και προσθήκη listener
        Transform addToCartButtonTransform = detailsPanel.transform.Find("Button_AddToCart");
        if (addToCartButtonTransform == null)
        {
            Debug.LogError("Button_AddToCart not found! Check the hierarchy of the DetailsPanel.");
        }
        else
        {
            Button addToCartButton = addToCartButtonTransform.GetComponent<Button>();
            if (addToCartButton != null)
            {
                addToCartButton.onClick.RemoveAllListeners();
                addToCartButton.onClick.AddListener(() => AddToCart(product));
            }
            else
            {
                Debug.LogError("Button_AddToCart is missing the Button component!");
            }
        }

        // Εμφάνιση του DetailsPanel
        homePanel.SetActive(false);
        detailsPanel.SetActive(true);
    }


    public void AddToCart(Product product)
    {
        cartProducts.Add(product);
        totalPrice += product.price;

        GameObject cartItem = Instantiate(cartItemPrefab, cartContent);
        cartItem.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(product.imagePath);
        cartItem.transform.Find("Title").GetComponent<TMP_Text>().text = product.name;
        cartItem.transform.Find("Price").GetComponent<TMP_Text>().text = $"${product.price:F2}";

        Button removeButton = cartItem.transform.Find("Remove").GetComponent<Button>();
        removeButton.onClick.AddListener(() => RemoveFromCart(product, cartItem));

        UpdateTotalPrice();
    }

    public void RemoveFromCart(Product product, GameObject cartItem)
    {
        cartProducts.Remove(product);
        totalPrice -= product.price;
        Destroy(cartItem);
        UpdateTotalPrice();
    }

    private void UpdateTotalPrice()
    {
        totalText.text = $"Total: ${totalPrice:F2}";
    }

    public void LoadARScene()
    {
        if (selectedPrefab == null)
        {
            Debug.LogError("No prefab selected for AR! Make sure to call ShowDetails before loading the ARScene.");
            return;
        }

        Debug.Log($"Loading ARScene with prefab: {selectedPrefab.name}");
        FindObjectOfType<SceneLoader>().LoadScene("ARScene");
    }



    public void HideDetailsPanel()
    {
        detailsPanel.SetActive(false);
        homePanel.SetActive(true);
    }

    public void ShowCartPanel()
    {
        homePanel.SetActive(false);
        detailsPanel.SetActive(false);
        cartPanel.SetActive(true);
    }

    public void HideCartPanel()
    {
        cartPanel.SetActive(false);
        homePanel.SetActive(true);
    }

    public void ToggleFavorite(Product product, Image heartIcon)
    {
        if (favoriteProducts.Contains(product))
        {
            favoriteProducts.Remove(product);
            heartIcon.sprite = defaultHeartIcon; // Set to black heart
            Debug.Log($"{product.name} removed from favorites.");
        }
        else
        {
            favoriteProducts.Add(product);
            heartIcon.sprite = activeHeartIcon; // Set to red heart
            Debug.Log($"{product.name} added to favorites.");
        }

        UpdateFavoritePanel();
    }

    private void UpdateFavoritePanel()
    {
        foreach (Transform child in favoriteContent)
        {
            Destroy(child.gameObject);
        }

        foreach (var product in favoriteProducts)
        {
            GameObject favoriteItem = Instantiate(FavoriteItemPrefab, favoriteContent);

            favoriteItem.transform.Find("ItemImage").GetComponent<Image>().sprite = Resources.Load<Sprite>(product.imagePath);
            favoriteItem.transform.Find("ItemName").GetComponent<TMP_Text>().text = product.name;
            favoriteItem.transform.Find("Price").GetComponent<TMP_Text>().text = $"${product.price:F2}";

            // Προσθήκη λειτουργίας για RemoveButton
            Button removeButton = favoriteItem.transform.Find("RemoveButton").GetComponent<Button>();
            if (removeButton != null)
            {
                removeButton.onClick.AddListener(() =>
                {
                    RemoveFromFavorites(product, favoriteItem);
                });
            }

            // Προσθήκη για εμφάνιση των λεπτομερειών αν χρειάζεται
            Button itemButton = favoriteItem.GetComponent<Button>();
            if (itemButton != null)
            {
                itemButton.onClick.AddListener(() => ShowDetails(product));
            }
        }
    }
    public void RemoveFromFavorites(Product product, GameObject favoriteItem)
    {
        if (favoriteProducts.Contains(product))
        {
            favoriteProducts.Remove(product);
            Destroy(favoriteItem);
            Debug.Log($"{product.name} removed from favorites.");
        }
        else
        {
            Debug.LogWarning($"Product {product.name} not found in favorites.");
        }
    }




    public void ShowFavoritesPanel()
    {
        homePanel.SetActive(false);
        detailsPanel.SetActive(false);
        cartPanel.SetActive(false);
        favoritePanel.SetActive(true);
    }

    public void ShowCheckoutPanel()
    {
        homePanel.SetActive(false);
        detailsPanel.SetActive(false);
        cartPanel.SetActive(false);
        favoritePanel.SetActive(false);
        checkoutPanel.SetActive(true);
    }
    public void ShowContactUsPanel()
    {
        homePanel.SetActive(false);
        detailsPanel.SetActive(false);
        cartPanel.SetActive(false);
        favoritePanel.SetActive(false);
        profilePanel.SetActive(false);
        contactusPanel.SetActive(true);
    }
    public void ShowProfilePanel()
    {

        contactusPanel.SetActive(false);
        profilePanel.SetActive(true);
        
    }
    public void HideCheckoutPanel()
    {
        checkoutPanel.SetActive(false);
        homePanel.SetActive(true);
    }

    public void HideFavoritesPanel()
    {
        favoritePanel.SetActive(false);
        homePanel.SetActive(true);
    }
     
    
    public void HideProfilePanel()
    {
       profilePanel.SetActive(false);
        homePanel.SetActive(true);
    }
}
