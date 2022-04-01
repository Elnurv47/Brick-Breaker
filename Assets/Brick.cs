using UnityEngine;
using Random = UnityEngine.Random;

public class Brick : MonoBehaviour
{
    private int _amount;

    [SerializeField] private TextMesh _amountText;

    private void Start()
    {
        SetRandomAmount();
    }

    private void SetRandomAmount()
    {
        _amount = Random.Range(50, 100);
        _amountText.text = _amount.ToString();
    }
}
