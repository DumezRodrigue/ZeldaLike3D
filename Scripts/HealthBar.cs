using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Slider health;
    public Player player;

    //Mise à jour du slider pour la vie
    void Update() {
        health.value = player.HP;
    }
}
