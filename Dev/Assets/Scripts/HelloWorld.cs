using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class HelloWorld : MonoBehaviour
{
    public GameObject tilePrefab;
    public Button btn;
    public GameObject mapGo;

    public Text txtComplete;
    private int tileWidth = 64;
    private int tileHeight = 64;
    private int tileCol = 0;
    private int tileRow = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.txtComplete.gameObject.SetActive(false);
            
        Debug.LogFormat("{0} {1}", Screen.width, Screen.height);

        
        this.btn.onClick.AddListener(() => {
            if (this.tileRow == 0)
            {
                this.CreteTile(new Vector2(this.tileCol, this.tileRow), true);
                this.tileCol++;
                if (this.tileCol > 3)
                {
                    this.tileCol = 0;
                    this.tileRow++;
                }
            }
            else if (this.tileRow == 1)
            {
                this.CreteTile(new Vector2(this.tileCol, this.tileRow), true);
                this.tileCol++;
                if (this.tileCol > 3)
                {
                    this.tileCol = 0;
                    this.tileRow++;
                }
            }
            else if (this.tileRow == 2)
            {
                this.CreteTile(new Vector2(this.tileCol, this.tileRow), true);
                this.tileCol++;
                if (this.tileCol > 3)
                {
                    this.tileCol = 0;
                    this.tileRow = 0;
                    this.btn.gameObject.SetActive(false);
                    this.txtComplete.gameObject.SetActive(true);
                }
            }

        });

        Debug.Log("Hello World!");

        StringBuilder sb = new StringBuilder();

        var col = 5;
        var row = 3;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                var str = string.Format("({0},{1})", j, i);
                sb.Append(str);
                sb.Append(" ");
            }
            sb.Append("\n");
        }

        Debug.Log(sb.ToString());
    }

    /*
     * http://clintbellanger.net/articles/isometric_math/
     *  screen.x = map.x * TILE_WIDTH;
        screen.y = map.y * TILE_HEIGHT;

        screen.x = 2 * 64; // equals 128 px
        screen.y = 1 * 64; // equals 64 px
    */
    public void CreteTile(Vector2 mapPos, bool isRelative = false)
    {
        var tileGo = Instantiate(this.tilePrefab);

        if (!isRelative)
        {
            var screenPos = this.MapToScreen(new Vector2(mapPos.x, mapPos.y + Screen.height));
            var worldPos = Camera.main.ScreenToWorldPoint(new Vector2(screenPos.x, screenPos.y));
            Debug.Log(worldPos);
            tileGo.transform.position = new Vector3(worldPos.x, worldPos.y, 0);
            tileGo.GetComponent<Tile>().textMesh.text = string.Format("({0},{1})", mapPos.x, mapPos.y);
        }
        else
        {
            Debug.LogFormat("mapPos: {0}", mapPos);
            var screenPos = this.MapToScreen(new Vector2(mapPos.x, mapPos.y));
            Debug.LogFormat("screenPos: {0}", screenPos);
            var worldPos = Camera.main.ScreenToWorldPoint(new Vector2(screenPos.x + Screen.width/2, screenPos.y + Screen.height/2));
            Debug.LogFormat("worldPos: {0}", worldPos);
            tileGo.transform.SetParent(this.mapGo.transform, false);
            tileGo.transform.localPosition = worldPos;
            
        }
    }

    public Vector2 MapToScreen(Vector2 mapPos)
    {
        var screenX = mapPos.x * this.tileWidth;
        var screenY = -mapPos.y * this.tileHeight;
        return new Vector2(screenX, screenY);
    }

    public Vector2 ScreenToMap(Vector2 screenPos)
    {
        var mapX = (int)screenPos.x / this.tileWidth;
        var mapY = (int)screenPos.y / this.tileHeight;
        return new Vector2(mapX, mapY);
    }
}
