using Chess.Scripts.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour, ISelectable {
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

    public void OnSelected() {
        string opponentTag = "";
        if (this.gameObject.tag == "white") { opponentTag = "black"; }
        else { opponentTag = "white"; }
        CheckAndAdd(enemyObjects,_row + 2, _column + 1, opponentTag);
        CheckAndAdd(enemyObjects,_row + 2, _column - 1, opponentTag);
        CheckAndAdd(enemyObjects,_row + 1, _column + 2, opponentTag);
        CheckAndAdd(enemyObjects, _row + 1, _column - 2, opponentTag);
        CheckAndAdd(enemyObjects, _row - 1, _column + 2, opponentTag);
        CheckAndAdd(enemyObjects, _row - 1, _column - 2, opponentTag);
        CheckAndAdd(enemyObjects, _row - 2, _column + 1, opponentTag);
        CheckAndAdd(enemyObjects, _row - 2, _column - 1, opponentTag);
        for (int i = 0; i < enemyObjects.Count; i++) {
            enemyObjects[i].GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public void OnDeselected() {
        ChessBoardPlacementHandler.Instance.ClearHighlights();
        for (int i = 0; i < enemyObjects.Count; i++) {
            enemyObjects[i].GetComponent<SpriteRenderer>().color = Color.white;
        }
        enemyObjects.Clear();
    }

    public void CheckAndAdd(List<GameObject> l1,int r,int c,string opponentTag) {
        if((r > 7 || r < 0) || (c > 7 || c < 0)) { return; }
        GameObject obj = PiecesHandler.instance.GetGameObjectOnTile(r, c);
        if (obj) {if (obj.tag == opponentTag) {enemyObjects.Add(obj); } return; }
        ChessBoardPlacementHandler.Instance.Highlight(r, c);
    }


}