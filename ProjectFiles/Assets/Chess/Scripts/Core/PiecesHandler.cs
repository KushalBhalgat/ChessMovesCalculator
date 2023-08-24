using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesHandler : MonoBehaviour
{
    private GameObject[,] _board;
    internal static PiecesHandler instance;
    private void Awake() {
        instance = this;
        _board = new GameObject[8, 8];
        for(int i=0; i < 8; i++) {
            for(int j=0; j < 8; j++) {
                _board[i, j] = null;
            }
        }
    }

    internal void SetPosition(GameObject obj,int row,int column) {
        _board[row, column] = obj;
    }

    internal GameObject GetGameObjectOnTile(int row,int column) {
        return _board[row, column];
    }
}
