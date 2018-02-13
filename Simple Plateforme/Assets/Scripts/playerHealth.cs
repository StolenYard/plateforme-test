using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour {

    public float fullHealth;
    public GameObject deathFX;

    float currentHealth;

    PlayerControler controlMovement;

    public Slider healthSlider;
    public Image damageScreen;

    bool damaged = false;
    Color damagedColour = new Color(1f, 0f, 0f, 0.5f);
    float smoothColour = 5f;

    void Start()
    {
        currentHealth = fullHealth;

        controlMovement = GetComponent<PlayerControler>();

        healthSlider.maxValue = fullHealth;
        healthSlider.value = fullHealth;

        damaged = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            damageScreen.color = damagedColour;
        } else
        {
            damageScreen.color = Color.Lerp(damageScreen.color, Color.clear, smoothColour * Time.deltaTime);
        }
        damaged = false;
    }

    public void addDamage(float damage)
        {
        if (damage <= 0) return;
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        damaged = true;

        if (currentHealth <= 0)
        {
            makeDead();
        }
        }

    void makeDead()
    {
        Instantiate(deathFX, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
