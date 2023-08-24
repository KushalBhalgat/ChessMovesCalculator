    using Chess.Scripts.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Rook : MonoBehaviour, ISelectable {
    private int _row, _column;
    private ChessPlayerPlacementHandler _playerPlacementHandler;
    [SerializeField]private List<GameObject> enemyObjects;

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

        for (int i = _row+1; i < 8; i++) {
            GameObject obj = PiecesHandler.instance.GetGameObjectOnTile(i, _column);
            if (obj) {
                if (obj.tag == opponentTag) { enemyObjects.Add(obj); }
                break;
            }
            ChessBoardPlacementHandler.Instance.Highlight(i, _column);
        }

        for (int i = _row - 1; i >= 0; i--) {
            GameObject obj = PiecesHandler.instance.GetGameObjectOnTile(i, _column);
            if (obj) {
                if (obj.tag == opponentTag) { enemyObjects.Add(obj); }
                break;
            }
            ChessBoardPlacementHandler.Instance.Highlight(i, _column);
        }

        for (int i = _column + 1; i < 8; i++) {
            GameObject obj = PiecesHandler.instance.GetGameObjectOnTile(_row, i);
            if (obj) {
                if (obj.tag == opponentTag) { enemyObjects.Add(obj); }
                break;
            }
            ChessBoardPlacementHandler.Instance.Highlight(_row, i);
        }

        for (int i = _column - 1; i >= 0; i--) {
            GameObject obj = PiecesHandler.instance.GetGameObjectOnTile(_row, i);
            if (obj) {
                if (obj.tag == opponentTag) { enemyObjects.Add(obj); }
                break;
            }
            ChessBoardPlacementHandler.Instance.Highlight(_row, i);
        }
        for (int i = 0; i < enemyObjects.Count; i++) {
            enemyObjects[i].GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public void OnDeselected() {
        ChessBoardPlacementHandler.Instance.ClearHighlights();
        for (int i = 0; i < enemyObjects.Count;i++) {
            enemyObjects[i].GetComponent<SpriteRenderer>().color = Color.white;
        }
        enemyObjects.Clear();
    }


}