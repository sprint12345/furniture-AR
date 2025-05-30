using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CheckoutManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text cartTotalText; // Total price in CartPanel
    public TMP_Text checkoutTotalText; // Total price in CheckoutPanel
    public GameObject cartPanel; // Reference to the Cart Panel
    public GameObject checkoutPanel; // Reference to the Checkout Panel

    // Εμφάνιση του CheckoutPanel και ενημέρωση της τιμής
    public void ShowCheckoutPanel()
    {
        if (cartPanel != null)
        {
            cartPanel.SetActive(false); // Κρύψε το CartPanel
        }

        if (checkoutPanel != null)
        {
            checkoutPanel.SetActive(true); // Εμφάνισε το CheckoutPanel
        }

        // Αντιγραφή της συνολικής τιμής από το CartPanel
        if (cartTotalText != null && checkoutTotalText != null)
        {
            checkoutTotalText.text = cartTotalText.text;
        }
    }

    // Επιστροφή στο CartPanel
    public void BackToCartPanel()
    {
        if (checkoutPanel != null)
        {
            checkoutPanel.SetActive(false); // Κρύψε το CheckoutPanel
        }

        if (cartPanel != null)
        {
            cartPanel.SetActive(true); // Εμφάνισε το CartPanel
        }
    }
}
