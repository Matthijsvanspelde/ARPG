using TMPro;

public class EnemyHealthBar : HealthBar
{
    public TMP_Text nameTag;

    private void Awake()
    {
        SetActive(false);
    }
    public void SetNameTag(string nameTag)
    {
        this.nameTag.text = nameTag;
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

}
