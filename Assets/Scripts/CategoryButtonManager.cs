using UnityEngine;
using UnityEngine.UI;

public class CategoryButtonManager : MonoBehaviour
{
    [System.Serializable]
    public class CategoryButton
    {
        public Button button;           // �� ������ ����������
        public Image icon;              // � ������ ��� ��������
        public Sprite activeSprite;     // ������ ��� ������ ���������
        public Sprite inactiveSprite;   // ������ ��� �������� ���������
    }

    public CategoryButton[] categoryButtons; // ����� �� �� ������� ����������
    private int activeButtonIndex = -1;      // �� index ��� ������� ��������

    private void Start()
    {
        // ������� ��� listeners ��� ���� ������
        for (int i = 0; i < categoryButtons.Length; i++)
        {
            int index = i; // ���������� ��� index
            categoryButtons[i].button.onClick.AddListener(() => OnCategoryButtonClicked(index));
        }
    }

    public void OnCategoryButtonClicked(int index)
    {
        // �� �� ������ ��� �������� ����� ��� ������, ��� ������� ������
        if (activeButtonIndex == index) return;

        // ��������� ��� ���������� ��� ��������
        for (int i = 0; i < categoryButtons.Length; i++)
        {
            bool isActive = i == index;

            // ������� �� sprite ��� �� Image Component
            categoryButtons[i].icon.sprite = isActive ? categoryButtons[i].activeSprite : categoryButtons[i].inactiveSprite;
        }

        // ��������� ��� ������� ��������
        activeButtonIndex = index;

        // ��� ������� �� �������� �� ������� script ��� �����������
        // �.�., CatalogManager.FilterProducts(categoryName)
    }
}
