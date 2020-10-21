using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChopShop : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI carNameText;
    public Text carCostText;

    public Vector3 startPosition;
    public Vector3 endPosition;

    public float scrollSpeed;

    private bool scrollCar;

    private Vector3 start;
    private float direction;

    [SerializeField] private List<Car> availableCars;

    int carIndex = 0; 

    Car currentCar;

    private void Start()
    {
        currentCar = availableCars[0];
        transform.position = startPosition;
    }

    private void Update()
    {
        if (scrollCar)
        {
            MoveCar(direction);
        }

        UpdateUI();
    }

    void MoveCar(float direction)
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, start + new Vector3(0f, 0f, direction), scrollSpeed * Time.deltaTime);

        if(this.transform.position == start + new Vector3(0f, 0f, direction))
        {
            scrollCar = false;
        }
    }

    void UpdateUI()
    {
        carNameText.text = currentCar.carName;
        carCostText.text = currentCar.carCost.ToString();
    }

    public void NextCar()
    {
        if(carIndex < availableCars.Count - 1)
        {
            start = transform.position;
            direction = 6.12f;
            carIndex++;
            currentCar = availableCars[carIndex];
            scrollCar = true;
        }
    }

    public void PreviousCar()
    {
        if (carIndex > 0)
        {
            start = transform.position;
            direction = -6.12f;
            carIndex--;
            currentCar = availableCars[carIndex];
            scrollCar = true;
        }
    }
}
