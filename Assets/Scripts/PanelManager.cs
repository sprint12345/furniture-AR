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
        // ������������ �� panels �� ��� dictionary ��� ������ ����������
        panels = new Dictionary<string, GameObject>
        {
            { "Home", homePanel },
            { "Cart", cartPanel },
            { "Favorites", favoritesPanel },
            { "Profile", profilePanel }
        };

        // �������� �� �� HomePanel ������
        ShowPanel("Home");
    }

    /// <summary>
    /// ��������� �� ���������� panel ��� ������ �� ��������.
    /// </summary>
    /// <param name="panelName">�� ����� ��� panel ��� ������� �� �����������.</param>
    public void ShowPanel(string panelName)
    {
        foreach (var panel in panels)
        {
            panel.Value.SetActive(panel.Key == panelName);
        }
    }
}
