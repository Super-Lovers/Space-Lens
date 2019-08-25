using UnityEngine;

public class StarController : MonoBehaviour
{
    [SerializeField]
    private StarClassification _starClassification;

    [SerializeField]
    private int _temperatureInKelvin;
    private const int MinTemperature = 2400;
    private const int MaxTemperature = 12500;

    #region Game Object Components
    private SpriteRenderer _spriteRenderer;
    #endregion

    public void Initialize()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        int randomKelvin = Random.Range(MinTemperature, MaxTemperature + 2500);
        _temperatureInKelvin = randomKelvin;

        if (randomKelvin >= MinTemperature && randomKelvin <= 3700)
        {
            _starClassification = StarClassification.M;
            _spriteRenderer.color = Color.yellow;
        }
        else if (randomKelvin > 3700 && randomKelvin <= 5200)
        {
            _starClassification = StarClassification.K;
            _spriteRenderer.color = Color.red;
        }
        else if (randomKelvin > 5200 && randomKelvin <= 6000)
        {
            _starClassification = StarClassification.G;
            _spriteRenderer.color = Color.magenta;
        }
        else if (randomKelvin > 6000 && randomKelvin <= 7500)
        {
            _starClassification = StarClassification.F;
            _spriteRenderer.color = Color.white;
        }
        else if (randomKelvin > 7500 && randomKelvin <= 10000)
        {
            _starClassification = StarClassification.A;
            _spriteRenderer.color = Color.gray;
        }
        else if (randomKelvin > 10000 && randomKelvin <= 12500)
        {
            _starClassification = StarClassification.B;
            _spriteRenderer.color = Color.cyan;
        }
        else if (randomKelvin > MaxTemperature)
        {
            _starClassification = StarClassification.O;
            _spriteRenderer.color = Color.blue;
        }
    }
}
