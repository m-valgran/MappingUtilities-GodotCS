using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MappingUtilities{

    /// <summary>
    /// All shapes start at 0,0. Offset them if needed.
    /// "Grid" will be referred to as a synonym of List<Vector2>.
    /// </summary>
    abstract public class Shape{
        private List<Vector2> _grid;                //The current form of the shape.
        private List<Vector2> _originalGrid;        //Memory field that remembers the original shape form.
        private Vector2 _offset = new Vector2(0,0); //Control field that keeps track of the shape's offset.

        protected Shape(List<Vector2> grid){
            this._grid = grid;
            this._originalGrid = grid.ToList();
        }

        public List<Vector2> Grid {
            get{ return this._grid; }
        }

        public Vector2 Offset {
            set{
                this.OffsetToPos(value);
            }
        }

        /// <summary>
        /// Performs a Venn-like subtraction with a given grid.
        /// </summary>
        public void Subtract(List<Vector2> grid){
            grid.ForEach(cell => this._grid.Remove(cell));
        }

        /// <summary>
        /// Offsets the current shape to the given position.
        /// </summary>
        private void OffsetToPos(Vector2 position){
            List<Vector2> offsettedGrid = new List<Vector2>();
            this._grid.ForEach(cell =>{
                offsettedGrid.Add(new Vector2(cell.x+position.x,cell.y + position.y));
            });
            this._grid = offsettedGrid;
            this._offset = position;
        }

        /// <summary>
        /// Resets the shape to its original form.
        /// </summary>
        public void Reset(){
            this._grid = this._originalGrid;
            if(this._offset != Vector2.Zero){ this.Offset = this._offset; }
        }

        /// <summary>
        /// Generates an outline around the shape and with the given thickness and returns it as a grid.
        /// </summary>
        public List<Vector2> GenerateOutline(uint thickness = 1){
            List<Vector2> outlineGrid = new List<Vector2>();
            this._grid.ForEach(cell => {
                for (int y = -(int)thickness; y <= thickness; y++){
                    for (int x = -(int)thickness; x <= thickness; x++){
                        Vector2 outlineCell = new Vector2(cell.x+x,cell.y+y);
                        if(this._grid.IndexOf(outlineCell) == -1 && outlineGrid.IndexOf(outlineCell) == -1){
                            outlineGrid.Add(outlineCell);
                        }
                    }
                }
            });
            return outlineGrid;
        }

        /// <summary>
        /// Returns a cell from this shape's grid. If not found, returns an infinite Vector2.
        /// </summary>
        public Vector2 GetCell(Vector2 cell){
            int i = this._grid.IndexOf(new Vector2(cell.x,cell.y));
            return i != -1 ? this._grid[i] : Vector2.Inf;
        }

        /// <summary>
        /// Spreads each cell of the grid by the given ammount.
        /// </summary>
        public void Spread(uint ratio = 1){
            if(ratio != 0){
                List<Vector2> spreadedGrid = new List<Vector2>();
                this._grid.ForEach(cell => {
                    spreadedGrid.Add(new Vector2(cell.x*(ratio+1),cell.y*(ratio+1)));
                });
                this._grid = spreadedGrid;
            }
         }
    }
}

