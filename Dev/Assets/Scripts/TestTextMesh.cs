using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTextMesh : MonoBehaviour
{
    public Tile tile;

    // Start is called before the first frame update
    void Start()
    {
        tile.textMesh.text = "hello world!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
