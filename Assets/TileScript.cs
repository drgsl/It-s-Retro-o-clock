using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    Color randColor;
    MaterialPropertyBlock block;
    Renderer render;
    //Rigidbody rb;
    Animator anim;
    //AudioSource source; 

    public bool hasBeenTouched = false;

    float xRot = 0;
    float yRot = 0;



    float AnimAddingValue = 0.01f;
    float AnimClampValue = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        block = new MaterialPropertyBlock();
        //rb = GetComponent<Rigidbody>();
        anim = transform.GetComponentInParent<Animator>();
        //source = GameObject.FindGameObjectWithTag("Hexagone/ClickSound").GetComponent<AudioSource>();

        randColor = Random.ColorHSV(0, 1, 0, 1, 0.8f, 1f);

        block.SetColor("_BaseColor", randColor);
        block.SetColor("_EmissionColor", randColor * -1f);
        //block.SetFloat("_EmissiveExposureWeight", -50f);
        render = GetComponent<Renderer>();
        render.SetPropertyBlock(block);

        InvokeRepeating("updateRot", 5f, 0.005f);
    }

    void FixedUpdate()
    {
        if (hasBeenTouched)
        {
            //source.PlayOneShot(source.clip);
            randColor = Color.Lerp(randColor, Color.red, 0.01f);
            block.SetColor("_BaseColor", randColor);
            block.SetColor("_EmissionColor", randColor);
            render.SetPropertyBlock(block);
            AnimAddingValue = 0.05f;
            AnimClampValue = 0.2f;
            //if (randColor == Color.red)
            if (Mathf.Round(randColor.r) == 1 &&
                Mathf.Round(randColor.g) == 0 &&
                Mathf.Round(randColor.b) == 0)
            {
                //Destroy(gameObject);
                anim.SetTrigger("Falling");
            }
        }

        transform.localRotation = Quaternion.Euler(xRot,yRot,0);
    }

    private void updateRot()
    {

        xRot += Random.Range(-AnimAddingValue, AnimAddingValue);
        yRot += Random.Range(-AnimAddingValue, AnimAddingValue);

        Mathf.Clamp(xRot, -AnimClampValue, AnimClampValue);
        Mathf.Clamp(yRot, -AnimClampValue, AnimClampValue);
    }
}
