using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GunFunc : MonoBehaviour
{
    private Animator anim;
    public Text magText;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) anim.Play("ReloadOutAmmo", 0, 0f);

        if (Input.GetMouseButton(0))
            anim.Play("Fire", 0, 0f);

        if (Input.GetKeyDown(KeyCode.L))
        {
            anim.SetTrigger("Inspect");
        }


        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.T))
            magText.gameObject.SetActive(true);
        else
            magText.gameObject.SetActive(false);
    }
}
