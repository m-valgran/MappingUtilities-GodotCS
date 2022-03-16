using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// This class allows to transform any grid (List<Vector2>) to a Shape derived class.
/// </summary>
namespace MappingUtilities{

    sealed public class Any : Shape{
        
        public Any(List<Vector2> grid) : base (grid){ }
    }
}