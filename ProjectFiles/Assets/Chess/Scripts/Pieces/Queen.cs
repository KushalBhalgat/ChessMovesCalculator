using Chess.Scripts.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : MonoBehaviour, ISelectable {
    private int _row, _column;
    private ChessPlayerPlacementHandler _playerPlacementHandler;
    [SerializeField] private List<GameObject> enemyObjects;

    void Start() {
        _playerPlacementHandler = GetComponent<ChessPlayerPlacementHandler>();
        _row = _playerPlacementHandler.row; _column = _playerPlacementHandler.column;
        _playerPlacementHandler.OnThisSelected += _playerPlacementHandler_OnThisSelected;
        _playerPlacementHandler.OnThisDeselected += _playerPlacementHandler_OnThisDeselected;
    }

    private void _playerPlacementHandler_OnThisDeselected(object sender, EventArgs e) {
        OnDeselected();
    }

    private void _playerPlacementHandler_OnThisSelected(object sender, EventArgs e) {
        OnSelected();
    }

    void Update() {
    }
    public void OnSelected() {
        QueenCheck(enemyObjects,_row,_column);
    }

    public void OnDeselected() {
        ChessBoardPlacementHandler.Instance.ClearHighlights();
        for (int i = 0; i < enemyObjects.Count; i++) {
            enemyObjects[i].GetComponent<SpriteRenderer>().color = Color.white;
        }
        enemyObjects.Clear();
    }

    public void QueenCheck(List<GameObject>l1,int r,int c) {
        string opponentTag = "";
        if (this.gameObject.tag == "white") { opponentTag = "black"; }
        else { opponentTag = "white"; }
        int tempRow = r + 1; int tempCol = c + 1;

        // Top Right
        while (tempRow <= 7 && tempCol <= 7) {
            GameObject obj = PiecesHandler.instance.GetGameObjectOnTile(tempRow, tempCol);
            if (obj) { if (obj.tag == opponentTag) { l1.Add(obj); } break; }
            ChessBoardPlacementHandler.Instance.Highlight(tempRow, tempCol);
            tempRow++; tempCol++;
        }


        // Top Left
        tempRow = r + 1; tempCol = c - 1;
        while (tempRow <= 7 && tempCol >= 0) {
            GameObject obj = PiecesHandler.instance.GetGameObjectOnTile(tempRow, tempCol);
            if (obj) { if (obj.tag == opponentTag) { l1.Add(obj); } break; }
            ChessBoardPlacementHandler.Instance.Highlight(tempRow, tempCol);
            tempRow++; tempCol--;
        }


        // Bottom Left
        tempRow = r - 1; tempCol = c + 1;
        while (tempRow >= 0 && tempCol <= 7) {
            GameObject obj = PiecesHandler.instance.GetGameObjectOnTile(tempRow, tempCol);
            if (obj) { if (obj.tag == opponentTag) { l1.Add(obj); } break; }
            ChessBoardPlacementHandler.Instance.Highlight(tempRow, tempCol);
            tempRow--; tempCol++;
        }


        // Bottom Right
        tempRow = r - 1; tempCol = c - 1;
        while (tempRow >= 0 && tempCol >= 0) {
            GameObject obj = PiecesHandler.instance.GetGameObjectOnTile(tempRow, tempCol);
            if (obj) { if (obj.tag == opponentTag) { l1.Add(obj); } break; }
            ChessBoardPlacementHandler.Instance.Highlight(tempRow, tempCol);
            tempRow--; tempCol--;
        }


        //Right
        tempRow = r; tempCol = c+1;
        while (tempCol <= 7) {
            GameObject obj = PiecesHandler.instance.GetGameObjectOnTile(tempRow, tempCol);
            if (obj) { if (obj.tag == opponentTag) { l1.Add(obj); } break; }
            ChessBoardPlacementHandler.Instance.Highlight(tempRow, tempCol);
            tempCol++;
        }


        //Left
        tempRow = r; tempCol = c - 1;
        while (tempCol >= 0) {
            GameObject obj = PiecesHandler.instance.GetGameObjectOnTile(tempRow, tempCol);
            if (obj) { if (obj.tag == opponentTag) { l1.Add(obj); } break; }
            ChessBoardPlacementHandler.Instance.Highlight(tempRow, tempCol);
            tempCol--;
        }


        //Bottom
        tempRow = r-1; tempCol = c ;
        while (tempRow >= 0) {
            GameObject obj = PiecesHandler.instance.GetGameObjectOnTile(tempRow, tempCol);
            if (obj) { if (obj.tag == opponentTag) { l1.Add(obj); } break; }
            ChessBoardPlacementHandler.Instance.Highlight(tempRow, tempCol);
            tempRow--;
        }

        //Top
        tempRow = r+1; tempCol = c;
        while (tempRow <= 7) {
            GameObject obj = PiecesHandler.instance.GetGameObjectOnTile(tempRow, tempCol);
            if (obj) { if (obj.tag == opponentTag) { l1.Add(obj); } break; }
            ChessBoardPlacementHandler.Instance.Highlight(tempRow, tempCol);
            tempRow++;
        }


        for (int i = 0; i < enemyObjects.Count; i++) {
            enemyObjects[i].GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}