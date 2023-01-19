using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollspeed = 0.2f;
    Material myMaterial;

    private void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
    }
    // Update is called once per frame
    void Update()
    {
        //float newoffsetx = myMaterial.main
        float newOffSetY = myMaterial.mainTextureOffset.y - scrollspeed * Time.deltaTime;
        Vector2 newOffset = new Vector2(0, newOffSetY);

        myMaterial.mainTextureOffset = newOffset;
    }
}
