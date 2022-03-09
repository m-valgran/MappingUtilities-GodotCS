using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MappingUtilities{

    /// <summary>
    /// All shapes start at 0;0
    /// </summary>
    abstract class Shape{
        private List<Vector2> _grid;
        private List<Vector2> _originalGrid;
        public Vector2 _offset = new Vector2(0,0);

        protected Shape(List<Vector2> grid){
            this._grid = grid;
            this._originalGrid = grid.ToList();
        }

        public List<Vector2> Grid {
            get{ return this._grid; }
        }

        /// <summary>
        /// Performs a Venn-like subtraction with a given grid.
        /// </summary>
        public void Subtract(Shape shape){
            shape._grid.ForEach(cell => this._grid.Remove(cell));
        }

        /// <summary>
        /// Offsets this shape to the given position
        /// </summary>
        public void Offset(Vector2 position){
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
            if(this._offset != Vector2.Zero){ GD.Print("asd"); this.Offset(this._offset); }
        }

        /// <summary>
        /// Generates an outline around the shape and returns it as a grid.
        /// </summary>
        public List<Vector2> GetOutline(uint thickness = 1){
            List<Vector2> outline = new List<Vector2>();
            this._grid.ForEach(cell => {
                for (int y = -(int)thickness; y <= thickness; y++){
                    for (int x = -(int)thickness; x <= thickness; x++){
                        Vector2 outlineCell = new Vector2(cell.x+x,cell.y+y);
                        if(this._grid.IndexOf(outlineCell) == -1 && outline.IndexOf(outlineCell) == -1){
                            outline.Add(outlineCell);
                        }
                    }
                }
            });
            return outline;
        }

        /// <summary>
        /// Returns a cell from this shape's grid. If not found, returns an infinite Vector2.
        /// </summary>
        public Vector2 GetCell(int xPos, int yPos){
            int i = this._grid.IndexOf(new Vector2(xPos,yPos));
            return i != -1 ? this._grid[i] : Vector2.Inf;
        }
    }
}

