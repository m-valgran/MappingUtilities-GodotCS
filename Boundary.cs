using Godot;
using System;
using System.Collections.Generic;

namespace MappingUtilities{
    
    sealed class Boundary : Shape{

        public Boundary(Vector2 area) : base (Generate(area)){ }

        private static List<Vector2> Generate(Vector2 area){
            List<Vector2> grid = new List<Vector2>();
            area = new Vector2(area.x-Math.Sign(area.x),area.y-Math.Sign(area.y));
            grid.Add(Vector2.Zero);
            grid.Add(area);
            grid.Add(new Vector2(area.x,0));
            grid.Add(new Vector2(0,area.y));
            return grid;
        }
    }
}