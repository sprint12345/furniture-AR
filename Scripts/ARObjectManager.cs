using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ARObjectPlacer : MonoBehaviour
{
    public GameObject objectToPlace;
    private GameObject spawnedObject;
    private ARRaycastManager raycastManager;

    public UnityEngine.UI.Slider rotationSlider;

    private Vector2 previousTouch1, previousTouch2; // Για το pinch-to-zoom
    private bool isPinching = false;

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        objectToPlace = CatalogManager.selectedPrefab;

        if (objectToPlace == null)
        {
            Debug.LogError("No prefab has been selected for placement!");
        }

        // Αρχική τιμή για το rotation slider
        if (rotationSlider != null)
        {
            rotationSlider.value = 0f;
            rotationSlider.onValueChanged.AddListener(OnRotationSliderChanged);
        }
    }

    void Update()
    {
        if (objectToPlace == null) return;

        // Ανίχνευση touch
        if (Input.touchCount == 1 && !isPinching)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;

                    if (spawnedObject == null)
                    {
                        // Δημιουργία αντικειμένου
                        spawnedObject = Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
                    }
                    else
                    {
                        // Επανατοποθέτηση αντικειμένου
                        spawnedObject.transform.position = hitPose.position;
                    }
                }
            }
        }
        else if (Input.touchCount == 2) // Pinch-to-zoom
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (!isPinching)
            {
                previousTouch1 = touch1.position;
                previousTouch2 = touch2.position;
                isPinching = true;
            }
            else
            {
                float previousDistance = Vector2.Distance(previousTouch1, previousTouch2);
                float currentDistance = Vector2.Distance(touch1.position, touch2.position);

                float scaleChange = (currentDistance - previousDistance) * 0.01f; // Ρυθμός αλλαγής μεγέθους
                if (spawnedObject != null)
                {
                    spawnedObject.transform.localScale += Vector3.one * scaleChange;

                    // Περιορισμός μεγέθους
                    spawnedObject.transform.localScale = Vector3.Max(spawnedObject.transform.localScale, Vector3.one * 0.1f); // Ελάχιστο μέγεθος
                    spawnedObject.transform.localScale = Vector3.Min(spawnedObject.transform.localScale, Vector3.one * 3f);   // Μέγιστο μέγεθος
                }

                previousTouch1 = touch1.position;
                previousTouch2 = touch2.position;
            }
        }
        else
        {
            isPinching = false;
        }
    }

    // Μέθοδος που καλείται όταν αλλάζει το slider
    private void OnRotationSliderChanged(float value)
    {
        if (spawnedObject != null)
        {
            spawnedObject.transform.rotation = Quaternion.Euler(0, value, 0); // Περιστροφή γύρω από τον άξονα Y
        }
    }
}
