using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIndicator : MonoBehaviour
{
    [SerializeField]
    private float hp;

    [SerializeField]
    private float maxHp = 40.0f;

    private Renderer texture;
    private Color color;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        texture = GetComponent<Renderer>();
        color = texture.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
            Destroy(gameObject);
        texture.material.color = new Color(color.r, color.g, color.b, color.a);
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        color.a -= damage / 50;
    }
}
