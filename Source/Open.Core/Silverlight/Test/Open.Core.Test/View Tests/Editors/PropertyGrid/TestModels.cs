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

using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Media;
using Open.Core.Common;
using Open.Core.Common.Controls.Editors;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Editors
{
    public class Person : ModelBase
    {
        #region Head
        private double @double;
        private decimal @decimal;
        private int @int;
        private short @short;
        private long @long;
        private uint @uint;
        private ushort @ushort;
        private ulong @ulong;
        private string @string;
        private char @char;
        private DateTime dateTime;
        private TimeSpan timeSpan;
        private bool boolean;
        private Car car;
        private DayOfWeek @enum;
        private Thickness thickness;
        #endregion

        #region Properties - Numeric Float
        [Category("Numeric Float")]
        public double Double
        {
            get { return @double; }
            set
            {
                if (@double == value) return;
                @double = value;
                OnPropertyChanged("Double");
            }
        }

        [Category("Numeric Float")]
        public decimal Decimal
        {
            get { return @decimal; }
            set
            {
                if (@decimal == value) return;
                @decimal = value;
                OnPropertyChanged("Decimal");
            }
        }
        #endregion

        #region Properties - Numeric Signed
        [Category("Numeric Signed")]
        public int Int
        {
            get { return @int; }
            set
            {
                if (@int == value) return;
                @int = value;
                OnPropertyChanged("Int");
            }
        }

        [Category("Numeric Signed")]
        public short Short
        {
            get { return @short; }
            set
            {
                if (@short == value) return;
                @short = value;
                OnPropertyChanged("Short");
            }
        }

        [Category("Numeric Signed")]
        public long Long
        {
            get { return @long; }
            set
            {
                if (@long == value) return;
                @long = value;
                OnPropertyChanged("Long");
            }
        }
        #endregion

        #region Properties - Numeric Unsigned
        [Category("Numeric Unsigned")]
        public uint Uint
        {
            get { return @uint; }
            set
            {
                if (@uint == value) return;
                @uint = value;
                OnPropertyChanged("Uint");
            }
        }

        [Category("Numeric Unsigned")]
        public ushort Ushort
        {
            get { return @ushort; }
            set
            {
                if (@ushort == value) return;
                @ushort = value;
                OnPropertyChanged("Ushort");
            }
        }

        [Category("Numeric Unsigned")]
        public ulong Ulong
        {
            get { return @ulong; }
            set
            {
                if (@ulong == value) return;
                @ulong = value;
                OnPropertyChanged("Ulong");
            }
        }
        #endregion

        #region Properties - Read Only
        [Category("Read Only")]
        public double Answer
        {
            get { return 42; }
        }

        [Category("Read Only")]
        public DateTime Now
        {
            get { return DateTime.Now; }
        }

        [Category("Read Only")]
        public bool True
        {
            get { return true; }
        }

        [Category("Read Only")]
        public DayOfWeek Today
        {
            get { return DateTime.Now.DayOfWeek; }
        }
        #endregion

        #region Properties - Strings
        [Category("Strings")]
        public string String
        {
            get { return @string; }
            set
            {
                if (@string == value) return;
                @string = value;
                OnPropertyChanged("String");
            }
        }

        [Category("Strings")]
        public char Char
        {
            get { return @char; }
            set
            {
                if (@char == value) return;
                @char = value;
                OnPropertyChanged("Char");
            }
        }
        #endregion

        #region Properties - Date & Time
        [Category("Date & Time")]
        public DateTime Datetime
        {
            get { return dateTime; }
            set
            {
                if (dateTime == value) return;
                dateTime = value;
                OnPropertyChanged("DateTime");
            }
        }

        [Category("Date & Time")]
        public TimeSpan TimeSpan
        {
            get { return timeSpan; }
            set
            {
                if (timeSpan == value) return;
                timeSpan = value;
                OnPropertyChanged("TimeSpan");
            }
        }
        #endregion

        #region Properties - Others
        [Category("Others")]
        public bool Boolean
        {
            get { return boolean; }
            set
            {
                if (boolean == value) return;
                boolean = value;
                OnPropertyChanged("Boolean");
            }
        }

        [Category("Others")]
        public DayOfWeek Enum
        {
            get { return @enum; }
            set
            {
                if (@enum == value) return;
                @enum = value;
                OnPropertyChanged("Enum");
            }
        }

        public Car Car
        {
            get { return car; }
            set
            {
                if (car == value) return;
                car = value;
                OnPropertyChanged("Car");
            }
        }

        public Thickness Thickness
        {
            get { return thickness; }
            set
            {
                if (value == Thickness) return;
                thickness = value;
                OnPropertyChanged("Thickness");
            }
        }
        #endregion
    }

    public class BoolModel:ModelBase
    {
        public bool Bool1 { get; set; }
        public bool Bool2 { get; set; }
    }

    public class StringModel : ModelBase
    {
        public string String1 { get; set; }
        public string String2 { get; set; }
    }


    public class Car : ModelBase
    {
        #region Head
        private string brand;
        private CarType carType;
        private Car childCar;
        private bool isFast;
        private bool? nullBool;
        private string readOnly = "Read Only String with a Long Value That is very Wide";
        private Brush background;
        private Color color;
        private Stream stream;
        private Thickness margin;

        public enum CarType
        {
            Sport,
            StationWagon,
            SUV
        }
        #endregion

        #region Properties
        [Category("Car Info")]
        public string Brand
        {
            get { return brand; }
            set
            {
                if (brand == value) return;
                brand = value;
                OnPropertyChanged("Brand");
            }
        }

        [Category("Car Info")]
        [PropertyGrid(Name = "Type of Car")]
        public CarType Type
        {
            get { return carType; }
            set
            {
                if (carType == value) return;
                carType = value;
                OnPropertyChanged("CarType");
            }
        }

        [PropertyGrid(Name = "Child Car")]
        public Car ChildCar
        {
            get { return childCar; }
            set
            {
                if (value == ChildCar) return;
                childCar = value;
                OnPropertyChanged("ChildCar");
            }
        }

        public bool IsFast
        {
            get { return isFast; }
            set
            {
                isFast = value;
                OnPropertyChanged("IsFast");
            }
        }

        private bool isFast2;
        public bool IsFast2
        {
            get { return isFast2; }
            set
            {
                isFast2 = value;
                OnPropertyChanged("IsFast2");
            }
        }

        public bool? NullBool
        {
            get { return nullBool; }
            set { nullBool = value; OnPropertyChanged("NullBool"); }
        }

        public string ReadOnly { get { return readOnly; } }

        public Brush Background
        {
            get { return background; }
            set
            {
                if (value == Background) return;
                background = value;
                OnPropertyChanged("Background");
            }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; OnPropertyChanged("Color"); }
        }

        [PropertyGrid(Name = "File Stream")]
        public Stream Stream
        {
            get { return stream; }
            set
            {
                stream = value;
                OnPropertyChanged("Stream");
            }
        }

        public Thickness Margin
        {
            get { return margin; }
            set { margin = value; OnPropertyChanged("Margin"); }
        }
        #endregion
    }

}
