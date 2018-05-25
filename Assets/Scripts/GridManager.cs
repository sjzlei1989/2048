using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject cellPrefab;
    public Cell[,] cells = new Cell[4, 4];
    List<Cell> nullCells = new List<Cell>();
    void Start() {
        Init();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            Move(Global.Direction.UP);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow)) {
            Move(Global.Direction.DOWN);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            Move(Global.Direction.LEFT);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow)) {
            Move(Global.Direction.RIGHT);
        }
    }

    void Init() {
        List<int> xs = new List<int> { 0, 1, 2, 3 };
        List<int> ys = new List<int> { 0, 1, 2, 3 };
        int x1 = xs[Random.Range(0, xs.Count)];
        xs.Remove(x1);
        int x2 = xs[Random.Range(0, xs.Count)];
        xs.Remove(x2);
        int y1 = ys[Random.Range(0, ys.Count)];
        ys.Remove(y1);
        int y2 = ys[Random.Range(0, ys.Count)];
        ys.Remove(y2);
        for(int x = 0; x < 4; x++) {
            for(int y = 0; y < 4; y++) {
                var tmp = Instantiate(cellPrefab, transform);
                tmp.GetComponent<RectTransform>().anchoredPosition = new Vector2(Global.BORDER + Global.CELL_SIZE / 2 + (Global.BORDER + Global.CELL_SIZE) * x, -(Global.BORDER + Global.CELL_SIZE / 2 + (Global.BORDER + Global.CELL_SIZE) * y));
                cells[x, y] = tmp.GetComponent<Cell>();
                cells[x, y].cellX = x;
                cells[x, y].cellY = y;
                if((x == x1 && y == y1) || (x == x2 && y == y2)) {
                    cells[x, y].cellValue = 2;
                }
                else {
                    nullCells.Add(cells[x, y]);
                }
            }
        }
    }

    Cell GetNullCell() {
        return null;
    }

    void SetNullCell(int _x, int _y) {
        cells[_x, _y].cellValue = 0;
        nullCells.Add(cells[_x, _y]);
    }

    void Move(Global.Direction _dir) {
        switch(_dir) {
            case Global.Direction.UP:
                for(int y = 1; y < 4; y++) {
                    for(int x = 0; x < 4; x++) {
                        if(cells[x, y].cellValue == 0) {
                            continue;
                        }
                        int tmpy = y - 1;
                        while(true) {
                            if(cells[x, tmpy].cellValue == 0) {
                                cells[x, tmpy].cellValue = cells[x, tmpy + 1].cellValue;
                                nullCells.Remove(cells[x, tmpy]);
                                SetNullCell(x, tmpy + 1);
                            }
                            else {
                                if(cells[x, tmpy].cellValue == cells[x, tmpy + 1].cellValue) {
                                    cells[x, tmpy].cellValue *= 2;
                                    SetNullCell(x, tmpy + 1);
                                }
                                break;
                            }
                            if(--tmpy < 0)
                                break;
                        }
                    }
                }
                break;
            case Global.Direction.DOWN:
                for(int y = 2; y >= 0; y--) {
                    for(int x = 0; x < 4; x++) {
                        if(cells[x, y].cellValue == 0) {
                            continue;
                        }
                        int tmpy = y + 1;
                        while(true) {
                            if(cells[x, tmpy].cellValue == 0) {
                                cells[x, tmpy].cellValue = cells[x, tmpy - 1].cellValue;
                                nullCells.Remove(cells[x, tmpy]);
                                SetNullCell(x, tmpy - 1);
                            }
                            else {
                                if(cells[x, tmpy].cellValue == cells[x, tmpy - 1].cellValue) {
                                    cells[x, tmpy].cellValue *= 2;
                                    SetNullCell(x, tmpy - 1);
                                }
                                break;
                            }
                            if(++tmpy > Global.NUM)
                                break;
                        }
                    }
                }
                break;
            case Global.Direction.LEFT:
                for(int x = 1; x < 4; x++) {
                    for(int y = 0; y < 4; y++) {
                        if(cells[x, y].cellValue == 0) {
                            continue;
                        }
                        int tmpx = x - 1;
                        while(true) {
                            if(cells[tmpx, y].cellValue == 0) {
                                cells[tmpx, y].cellValue = cells[tmpx + 1, y].cellValue;
                                nullCells.Remove(cells[tmpx, y]);
                                SetNullCell(tmpx + 1, y);
                            }
                            else {
                                if(cells[tmpx, y].cellValue == cells[tmpx + 1, y].cellValue) {
                                    cells[tmpx, y].cellValue *= 2;
                                    SetNullCell(tmpx + 1, y);
                                }
                                break;
                            }
                            if(--tmpx < 0)
                                break;
                        }
                    }
                }
                break;
            case Global.Direction.RIGHT:
                for(int x = 2; x >= 0; x--) {
                    for(int y = 0; y < 4; y++) {
                        if(cells[x, y].cellValue == 0) {
                            continue;
                        }
                        int tmpx = x + 1;
                        while(true) {
                            if(tmpx > Global.NUM)
                                break;
                            if(cells[tmpx, y].cellValue == 0) {
                                cells[tmpx, y].cellValue = cells[tmpx - 1, y].cellValue;
                                nullCells.Remove(cells[tmpx, y]);
                                SetNullCell(tmpx - 1, y);
                            }
                            else {
                                if(cells[tmpx, y].cellValue == cells[tmpx - 1, y].cellValue) {
                                    cells[tmpx, y].cellValue *= 2;
                                    SetNullCell(tmpx - 1, y);
                                }
                                break;
                            }
                            if(++tmpx > Global.NUM)
                                break;
                        }
                    }
                }
                break;
        }
        int r = Random.Range(0, nullCells.Count);
        nullCells[r].cellValue = 2;
        nullCells.RemoveAt(r);
    }
}
