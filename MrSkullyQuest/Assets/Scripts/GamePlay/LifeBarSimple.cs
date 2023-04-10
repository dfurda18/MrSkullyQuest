using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBarSimple : MonoBehaviour
{
    public GameObject lifeBar;
    public SkullyController skullyController;
    Image barImage;
    private void Awake()
    {
        barImage = GameObject.Find("LBLife").GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        barImage.fillAmount = skullyController.percentageLife / 100f;
    }
}
