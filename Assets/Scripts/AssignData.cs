using UnityEngine;
using UnityEngine.EventSystems;

public class AssignData : MonoBehaviour, IPointerClickHandler
{
    private Product product; // ���������� �� ������

    // ������� ��� �� �������� �� ������ ���� ������������� �� prefab
    public void SetProduct(Product productData)
    {
        product = productData;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (product != null)
        {
            Debug.Log($"Product clicked: {product.name}");
            FindObjectOfType<CatalogManager>().ShowDetails(product);
        }
        else
        {
            Debug.LogError("Product is not assigned!");
        }
    }
}
