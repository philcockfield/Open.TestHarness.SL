//------------------------------------------------------
//    Copyright (c) 2010 TestHarness.org
//
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the 'Software'), to deal
//    in the Software without restriction, including without limitation the rights
//    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//    copies of the Software, and to permit persons to whom the Software is
//    furnished to do so, subject to the following conditions:
//
//    The above copyright notice and this permission notice shall be included in
//    all copies or substantial portions of the Software.
//
//    THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//    THE SOFTWARE.
//------------------------------------------------------

namespace Open.Core.Common
{
    /// <summary>Index of shapes.</summary>
    public class Shapes
    {
        #region Head
        public static readonly Shapes Instance = new Shapes();

        /// <summary>Constructor (force singleton).</summary>
        private Shapes(){}
        #endregion

        #region Properties
        // Triangles.
        public static string TriangleRight { get { return GetShapeString("Shape.Triangle.Right"); } }
        public static string TriangleLeft { get { return GetShapeString("Shape.Triangle.Left"); } }
        public static string TriangleUp { get { return GetShapeString("Shape.Triangle.Up"); } }
        public static string TriangleDown { get { return GetShapeString("Shape.Triangle.Down"); } }

        // Icons.
        public static string Cross { get { return GetShapeString("Icon.Cross"); } }
        public static string EncircledCross { get { return GetShapeString("Icon.EncircledCross"); } }

        // Icons: Rounded Pointers.
        public static string RoundedPointerLeft { get { return GetShapeString("Icon.RoundedPointer.Left"); } }
        public static string RoundedPointerRight { get { return GetShapeString("Icon.RoundedPointer.Right"); } }
        public static string RoundedPointerUp { get { return GetShapeString("Icon.RoundedPointer.Up"); } }
        public static string RoundedPointerDown { get { return GetShapeString("Icon.RoundedPointer.Down"); } }
        #endregion

        #region Internal
        private static string GetShapeString(string key)
        {
            return StyleResources.Shapes[key] as string;
        }
        #endregion
    }
}
