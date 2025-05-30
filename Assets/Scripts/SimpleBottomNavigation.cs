using UnityEngine;

public class SimpleBottomNavigation : MonoBehaviour
{
    [System.Serializable]
    public class NavigationItem
    {
        public GameObject button;       // Το κουμπί (GameObject)
        public GameObject panel;        // Το αντίστοιχο Panel
        public GameObject activeIcon;   // Εικόνα ενεργής κατάστασης
        public GameObject inactiveIcon; // Εικόνα ανενεργής κατάστασης
    }

    public NavigationItem[] navigationItems;

    private void Start()
    {
        // Αρχικοποίηση: Ενεργοποίησε το πρώτο Panel και τις αντίστοιχες εικόνες
        ActivatePanel(0);
    }

    public void ActivatePanel(int index)
    {
        for (int i = 0; i < navigationItems.Length; i++)
        {
            bool isActive = i == index;

            // Ενεργοποίησε/Απενεργοποίησε τα Panels
            navigationItems[i].panel.SetActive(isActive);

            // Εμφάνιση του ενεργού/ανενεργού εικονιδίου
            navigationItems[i].activeIcon.SetActive(isActive);
            navigationItems[i].inactiveIcon.SetActive(!isActive);
        }
    }
}
