using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;

namespace Open.Core.Common.Test.Core.Extensions
{
    [TestClass]
    public class SerializationExtensionsTest
    {
        #region Tests - DataContract (XML | JSON)
        [TestMethod]
        public void ShouldSerializeAndDeserializeObjectAsXml()
        {
            var childCar = new Car { Brand = "MX5" };
            var car = new Car { Type = Car.CarType.Sport, IsFast = true, ChildCar = childCar };

            var xml = car.ToSerializedXml();
            xml.Length.ShouldNotBe(0);

            var rebuild = xml.FromSerializedXml<Car>();

            rebuild.Type.ShouldBe(Car.CarType.Sport);
            rebuild.IsFast.ShouldBe(true);
            rebuild.ChildCar.ShouldNotBe(null);
            rebuild.ChildCar.Brand.ShouldBe("MX5");
        }

        [TestMethod]
        public void ShouldSerializeAndDeserializeObjectAsJson()
        {
            var childCar = new Car { Brand = "MX5" };
            var car = new Car { Type = Car.CarType.Sport, IsFast = true, ChildCar = childCar };

            var json = car.ToSerializedJson();
            json.Length.ShouldNotBe(0);

            var rebuild = json.FromSerializedJson<Car>();

            rebuild.Type.ShouldBe(Car.CarType.Sport);
            rebuild.IsFast.ShouldBe(true);
            rebuild.ChildCar.ShouldNotBe(null);
            rebuild.ChildCar.Brand.ShouldBe("MX5");
        }

        [TestMethod]
        public void ShouldSerializeListToJson()
        {
            var list = new List<string> { "one", "two", "three" };
            list.ToSerializedJson();
        }
        #endregion

        #region Tests - Simple Value Type Seralization
        [TestMethod]
        public void ShouldSeralizeColorAsString()
        {
            var color = Color.FromArgb(0, 1, 2, 3);
            color.A.ShouldBe(Convert.ToByte(0));
            color.R.ShouldBe(Convert.ToByte(1));
            color.G.ShouldBe(Convert.ToByte(2));
            color.B.ShouldBe(Convert.ToByte(3));

            var stringColor = color.ToColorString();
            var aColor = stringColor.Split(",".ToCharArray());

            aColor[0].ShouldBe("0");
            aColor[1].ShouldBe("1");
            aColor[2].ShouldBe("2");
            aColor[3].ShouldBe("3");
        }

        [TestMethod]
        public void ShouldDeseralizeStringToColor()
        {
            var stringColor = Color.FromArgb(0, 1, 2, 3).ToColorString();

            var color = stringColor.FromColorString();
            color.A.ShouldBe(Convert.ToByte(0));
            color.R.ShouldBe(Convert.ToByte(1));
            color.G.ShouldBe(Convert.ToByte(2));
            color.B.ShouldBe(Convert.ToByte(3));

            "   ".FromColorString().ShouldBe(default(Color));

            color = "   0, 1, 2, 3   ".FromColorString();
            color.A.ShouldBe(Convert.ToByte(0));
            color.R.ShouldBe(Convert.ToByte(1));
            color.G.ShouldBe(Convert.ToByte(2));
            color.B.ShouldBe(Convert.ToByte(3));
        }

        [TestMethod]
        public void ShouldThrowWhenIncompleteColorPassed()
        {
            Should.Throw<ArgumentOutOfRangeException>(() => "   1, 2, 3   ".FromColorString());
        }

        [TestMethod]
        public void ShouldSeralizeThicknessAsString()
        {
            var margin = new Thickness(1, 2, 3, 4);
            var marginString = margin.ToThicknessString();
            marginString.ShouldBe("1,2,3,4");

            margin = new Thickness(3);
            marginString = margin.ToThicknessString();
            marginString.ShouldBe("3");
        }

        [TestMethod]
        public void ShouldDesializeThicknessFromString()
        {
            var margin = "   ".FromThicknessString();
            margin.Left.ShouldBe(0d);
            margin.Top.ShouldBe(0d);
            margin.Right.ShouldBe(0d);
            margin.Bottom.ShouldBe(0d);

            const string sNull = null;
            margin = sNull.FromThicknessString();
            margin.Left.ShouldBe(0d);
            margin.Top.ShouldBe(0d);
            margin.Right.ShouldBe(0d);
            margin.Bottom.ShouldBe(0d);

            margin = "1,2,3,4".FromThicknessString();
            margin.Left.ShouldBe(1d);
            margin.Top.ShouldBe(2d);
            margin.Right.ShouldBe(3d);
            margin.Bottom.ShouldBe(4d);

            margin = "1".FromThicknessString();
            margin.Left.ShouldBe(1d);
            margin.Top.ShouldBe(1d);
            margin.Right.ShouldBe(1d);
            margin.Bottom.ShouldBe(1d);

            margin = "   1 , 2   , 3   ,    4     ".FromThicknessString();
            margin.Left.ShouldBe(1d);
            margin.Top.ShouldBe(2d);
            margin.Right.ShouldBe(3d);
            margin.Bottom.ShouldBe(4d);

            margin = "1.2. 3  . 4".FromThicknessString();
            margin.Left.ShouldBe(1d);
            margin.Top.ShouldBe(2d);
            margin.Right.ShouldBe(3d);
            margin.Bottom.ShouldBe(4d);

            margin = "1.2.".FromThicknessString();
            margin.Left.ShouldBe(1d);
            margin.Top.ShouldBe(2d);
            margin.Right.ShouldBe(0d);
            margin.Bottom.ShouldBe(0d);

            margin = "1,2,3,4,5,6,7,8,9".FromThicknessString();
            margin.Left.ShouldBe(1d);
            margin.Top.ShouldBe(2d);
            margin.Right.ShouldBe(3d);
            margin.Bottom.ShouldBe(4d);

            margin = "1..,".FromThicknessString();
            margin.Left.ShouldBe(1d);
            margin.Top.ShouldBe(0d);
            margin.Right.ShouldBe(0d);
            margin.Bottom.ShouldBe(0d);
        }
        #endregion

        #region Mocks
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
        #endregion
    }
}
