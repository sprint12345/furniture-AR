using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject homePanel;
    public GameObject cartPanel;
    public GameObject favoritesPanel;
    public GameObject profilePanel;

    private Dictionary<string, GameObject> panels;

    private void Start()
    {
        // Αποθηκεύουμε τα panels σε ένα dictionary για εύκολη διαχείριση
        panels = new Dictionary<string, GameObject>
        {
            { "Home", homePanel },
            { "Cart", cartPanel },
            { "Favorites", favoritesPanel },
            { "Profile", profilePanel }
        };

        // Ξεκινάμε με το HomePanel ενεργό
        ShowPanel("Home");
    }

    /// <summary>
    /// Εμφανίζει το επιλεγμένο panel και κρύβει τα υπόλοιπα.
    /// </summary>
    /// <param name="panelName">Το όνομα του panel που θέλουμε να εμφανίσουμε.</param>
    public void ShowPanel(string panelName)
    {
        foreach (var panel in panels)
        {
            panel.Value.SetActive(panel.Key == panelName);
        }
    }
}
