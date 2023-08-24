using Chess.Scripts.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InputHandler : MonoBehaviour
{
    private Camera _mainCamera;
    private GameObject pieceSelected;
    private Sprite blackAttackSprite; 
    void Start()
    {
        _mainCamera = Camera.main;
        pieceSelected = null;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            Vector2 square = GetClickedSquare();
            int row = (int)square.x; int col= (int)square.y;
            if (pieceSelected) { pieceSelected.GetComponent<ChessPlayerPlacementHandler>().ThisDeselected(); }
            pieceSelected = PiecesHandler.instance.GetGameObjectOnTile(row, col);
            if(pieceSelected && (pieceSelected.tag=="black" || pieceSelected.tag == "white")) { pieceSelected.GetComponent<ChessPlayerPlacementHandler>().ThisSelected();}
            if (!pieceSelected){
                ChessBoardPlacementHandler.Instance.ClearHighlights();
            }
        }
        //For Testing Purposes :
        /*
        else if (Input.GetMouseButtonDown(2)) {
            Vector2 square = GetClickedSquare();
            int row = (int)square.x; int col = (int)square.y;
            pieceSelected = PiecesHandler.instance.GetGameObjectOnTile(row, col);
            if (pieceSelected) { Destroy(pieceSelected); }
        }
        */

    }

    Vector2 GetClickedSquare() {
        RaycastHit2D rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Input.mousePosition));
        try { GameObject hitObject = rayHit.collider.gameObject;
            if (hitObject.TryGetComponent<ChessPlayerPlacementHandler>(out ChessPlayerPlacementHandler playerPlacement)) {
                return new Vector2(playerPlacement.row, playerPlacement.column);
            }
            string rowNoStr = hitObject.transform.parent.gameObject.name;
            int rowNo = int.Parse(rowNoStr.Substring(rowNoStr.Length - 2, 1)) - 1;
            int colNo = int.Parse(hitObject.name);
            return new Vector2(rowNo, colNo);
        }
        catch(Exception){ }
        return new Vector2(0,0);
    }
}
