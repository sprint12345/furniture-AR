using UnityEngine;
using UnityEngine.UI;

public class CategoryButtonManager : MonoBehaviour
{
    [System.Serializable]
    public class CategoryButton
    {
        public Button button;           // Το κουμπί κατηγορίας
        public Image icon;              // Η εικόνα του κουμπιού
        public Sprite activeSprite;     // Εικόνα για ενεργή κατάσταση
        public Sprite inactiveSprite;   // Εικόνα για ανενεργή κατάσταση
    }

    public CategoryButton[] categoryButtons; // Λίστα με τα κουμπιά κατηγοριών
    private int activeButtonIndex = -1;      // Το index του ενεργού κουμπιού

    private void Start()
    {
        // Ρύθμιση των listeners για κάθε κουμπί
        for (int i = 0; i < categoryButtons.Length; i++)
        {
            int index = i; // Αποθήκευση του index
            categoryButtons[i].button.onClick.AddListener(() => OnCategoryButtonClicked(index));
        }
    }

    public void OnCategoryButtonClicked(int index)
    {
        // Αν το κουμπί που πατήθηκε είναι ήδη ενεργό, δεν κάνουμε τίποτα
        if (activeButtonIndex == index) return;

        // Ενημέρωση της κατάστασης των κουμπιών
        for (int i = 0; i < categoryButtons.Length; i++)
        {
            bool isActive = i == index;

            // Αλλάζει το sprite για το Image Component
            categoryButtons[i].icon.sprite = isActive ? categoryButtons[i].activeSprite : categoryButtons[i].inactiveSprite;
        }

        // Ενημέρωση του ενεργού κουμπιού
        activeButtonIndex = index;

        // Εδώ μπορείς να καλέσεις το υπάρχον script για φιλτράρισμα
        // π.χ., CatalogManager.FilterProducts(categoryName)
    }
}
