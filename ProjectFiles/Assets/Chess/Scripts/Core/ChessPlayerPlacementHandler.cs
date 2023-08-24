using System;
using UnityEngine;

namespace Chess.Scripts.Core {
    public class ChessPlayerPlacementHandler : MonoBehaviour {
        [SerializeField] public int row, column;
        public bool isSelected;
        public event EventHandler OnThisSelected;
        public event EventHandler OnThisDeselected;
        [SerializeField] private GameObject _selectedCircleObjectPrefab;
        private GameObject _currSelectedCircle;
        private void Start() {
            _currSelectedCircle = null;
            isSelected = false;
            transform.position = ChessBoardPlacementHandler.Instance.GetTile(row, column).transform.position;
            PiecesHandler.instance.SetPosition(this.gameObject, row, column);
        }

        public void ThisSelected() {
            _currSelectedCircle = Instantiate(_selectedCircleObjectPrefab, ChessBoardPlacementHandler.Instance.GetTile(row, column).transform);
            OnThisSelected?.Invoke(this, EventArgs.Empty);
        }
        public void ThisDeselected() {
            if (_currSelectedCircle) { 
                Destroy(_currSelectedCircle);
            }
            _currSelectedCircle = null;
            OnThisDeselected?.Invoke(this, EventArgs.Empty);
        }

    }
}