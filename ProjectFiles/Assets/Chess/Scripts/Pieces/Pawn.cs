using Chess.Scripts.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour,ISelectable
{
    private int _row,_column;
    private bool _isPawnOnRank1;
    private ChessPlayerPlacementHandler _playerPlacementHandler;
    private GameObject enemyObject1;
    private GameObject enemyObject2;

    void Start()
    {
        _playerPlacementHandler =GetComponent<ChessPlayerPlacementHandler>();
        _row = _playerPlacementHandler.row; _column = _playerPlacementHandler.column;
        _playerPlacementHandler.OnThisSelected += _playerPlacementHandler_OnThisSelected;
        _playerPlacementHandler.OnThisDeselected += _playerPlacementHandler_OnThisDeselected;
        enemyObject1 = null;
        enemyObject2 = null;
    }

    private void _playerPlacementHandler_OnThisDeselected(object sender, EventArgs e) {
        OnDeselected();
    }

    private void _playerPlacementHandler_OnThisSelected(object sender, EventArgs e) {
        OnSelected();
    }

    void Update()
    {
        if (this.gameObject.tag == "black" && _row == 1) {
            _isPawnOnRank1 = true;
        }
        else if(this.gameObject.tag == "white" && _row ==6) { 
            _isPawnOnRank1 = true;
        }
    }
    public void OnSelected() {
        if (this.gameObject.tag == "black") { 
            if (PiecesHandler.instance.GetGameObjectOnTile(_row+1, _column)==null && _row<7) {
                ChessBoardPlacementHandler.Instance.Highlight(_row+1, _column);
                if (_isPawnOnRank1 && PiecesHandler.instance.GetGameObjectOnTile(_row + 2, _column) == null) {
                    ChessBoardPlacementHandler.Instance.Highlight(_row + 2, _column);
                }
            }
        }
        else {
            if (PiecesHandler.instance.GetGameObjectOnTile(_row - 1, _column) == null && _row < 7) {
                ChessBoardPlacementHandler.Instance.Highlight(_row - 1, _column);
                if (_isPawnOnRank1 && PiecesHandler.instance.GetGameObjectOnTile(_row - 2, _column) == null) {
                    ChessBoardPlacementHandler.Instance.Highlight(_row - 2, _column);
                }
            }
        }

        if (this.gameObject.tag=="black" && _row < 7) {
            try { 
                GameObject obj1 = PiecesHandler.instance.GetGameObjectOnTile(_row+1, _column+1);
                if (obj1 && obj1.tag == "white") {
                    enemyObject1 = obj1;
                    obj1.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
            catch (Exception) {}
            try { 
                GameObject obj2 = PiecesHandler.instance.GetGameObjectOnTile(_row + 1, _column - 1);
                if (obj2 && obj2.tag == "white") {
                    enemyObject2 = obj2;
                    obj2.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
            catch(Exception) { }
        }

        if (this.gameObject.tag == "white" && _row > 0) {
            try {
                GameObject obj1 = PiecesHandler.instance.GetGameObjectOnTile(_row + 1, _column + 1);
                if (obj1 && obj1.tag == "black") {
                    enemyObject1 = obj1;
                    obj1.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
            catch (Exception) { }
            try {
                GameObject obj2 = PiecesHandler.instance.GetGameObjectOnTile(_row + 1, _column - 1);
                if (obj2 && obj2.tag == "black") {
                    enemyObject2 = obj2;
                    obj2.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
            catch (Exception) { }
        }
    }

    public void OnDeselected() {
        ChessBoardPlacementHandler.Instance.ClearHighlights();
        if (this.gameObject.tag == "black") {
            if (enemyObject1) { enemyObject1.GetComponent<SpriteRenderer>().color = Color.white; }
            if (enemyObject2) { enemyObject2.GetComponent<SpriteRenderer>().color = Color.white; }
        }
        enemyObject1 = null;
        enemyObject2 = null;
    }


}
