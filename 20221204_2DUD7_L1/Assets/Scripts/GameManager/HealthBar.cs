
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float playerHealth = 100f;
    public float PlayerHealth
    {
        get => playerHealth; set => playerHealth = (value >= 100f) ? 100f : value;
    }
    private Image healthBarImage;
    private float redColorImageValue = 0.3f;
    private float orangeColorImageValue = 0.7f;
    [SerializeField] internal float HP;
    void Start()
    {
        healthBarImage = GetComponent<Image>();
        HP = PlayerHealth;
    }

    void Update()
    {
        ChangeHealthBarValue();
        HealthBariMageColor();
    }
    private void ChangeHealthBarValue() => healthBarImage.fillAmount = HP / PlayerHealth;
    private void HealthBariMageColor()
    {
        if (healthBarImage.fillAmount < redColorImageValue) healthBarImage.color = Color.red;
        else if (healthBarImage.fillAmount < orangeColorImageValue && healthBarImage.fillAmount >= redColorImageValue) healthBarImage.color = Color.Lerp(Color.red, Color.yellow, 0.5f);
        else healthBarImage.color = new Color(0.5176471f, 0.6784314f, 0.5294118f, 1);
    }

}
