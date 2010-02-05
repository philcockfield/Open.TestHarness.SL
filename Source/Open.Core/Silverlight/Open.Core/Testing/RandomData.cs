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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Open.Core.Common.Testing
{
    /// <summary>Provides random data.</summary>
    public static class RandomData
    {
        #region Head
        public static Random Random { get; private set; }
        private static readonly string[] loremIpsumWords = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin consectetur ultrices dui. Nulla tempus cursus mi. Praesent cursus nibh. Nullam pede mauris, adipiscing mattis, ultrices in, fermentum sed, quam. Suspendisse varius. Cras libero velit, consectetur non, faucibus id, feugiat dapibus, ante. Nulla nec diam. Donec leo enim, condimentum id, pulvinar vel, auctor id, libero. Praesent consectetur tortor sed dolor. Fusce felis eros, aliquam a, cursus in, egestas vel, dui. Integer massa. Mauris id est et lacus tincidunt pretium. Maecenas eu quam. In consequat, arcu non ornare adipiscing, nisl risus eleifend leo, consequat volutpat leo justo vitae urna. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Sed molestie dignissim lacus. Suspendisse id erat at sapien pharetra viverra. Vestibulum condimentum, ante a cursus sodales, enim metus eleifend augue, gravida sollicitudin est dui sit amet enim. Aliquam tristique ipsum non elit.".Replace(",", "").Replace(".", "").Split(' ');
        private static readonly List<string> fileExtensions = new List<string>
                                                                  {
                                                                      "mp3", "mp4", "zip", "exe", "htm", "html", "gif", "wmf", "xml", "txt", 
                                                                      "doc", "docx", "xls", "xlsx", "pdf", "png", "cdr", "tif", "tiff", "pub", "psd", "jpg", "jpeg", 
                                                                      "ppt", "pptx", "rtf", "wsd", "pst"
                                                                  };

        private static readonly List<string> topLevelDomains = new List<string>
                                                                   {
                                                                       ".com", ".net", ".org", ".co.nz", ".net.nz", ".co.uk", ".com.au", ".us", ".ca", ".jp", ".ru", ".me"
                                                                   };

        private static readonly List<string> firstNamesMale = new List<string>
                                                                  {
                                                                      "John", "Paul", "Harry", "Gorden", "Dmitry", "Chris", "Nikolay", "Mikhail", "Michael", "Mike", "Jeremy", "Richard", "Reg", "Stuwart", "Chuck", "Stanley", "Ken", "Kevin", "Tom", "Josh", "David", "Fred",
                                                                      "Johhny", "Albert", "Allen", "Alan", "Danny", "Stephen"
                                                                  };

        private static readonly List<string> firstNamesFemale = new List<string>
                                                                    {
                                                                        "Mary", "Diane", "Ava", "Emma", "Elizabeth", "Liz", "Abigail", "Sophia", "Chloe", "Ella", "Emily", "Arianna", "Natalie", "Paige", "Aliexis", "Kylie", "Katherine", "Sienna", "Aslyn", "Amber", "Evelyn", "Ruby", "Julia", 
                                                                        "Helena", "Tracey", "Joanna", "Jane"
                                                                    };

        private static readonly List<string> lastNames = new List<string>
                                                             {
                                                                 "Peele", "Peney", "Romanno", "Safford", "Scarret", "Seaford", "Seaton", "Thorn", "Torry", 
                                                                 "Bagley", "Smith", "Brenin", "Bristow", "Falkland", "Fry", "Beckworth", "Bedord", "Bellamy", "Chadwick", 
                                                                 "Lester", "Leigh", "Merton", "Mead", "Jobs", "Middleton", "Mills", "Paris", "Orton", 
                                                                 "Chester", "Chilton", "Cummings", "Eton", "Fales", "Gill", "Harding", "Harcourt", "Hatwell", "Kinloch",
                                                                 "Vesey", "Waters", "Wells", "Memyss", "York", "Saunders", "Eads", "Guilfoyle", "Brass", "Sanders",
                                                                 "Fox", "Berman", "Mazar", "Tunney", "LaPaglia", "Whitwhorth", "Tyler", "Bode", "Bolen", "Sissons",
                                                                 "Watson", "Ullman", "Lumley", "Finney", "Lee", "Gough", "Horrocks", "Reitel", "Elfman", "Ballantyne"
                                                             };

        static RandomData()
        {
            Random = new Random((int)DateTime.Now.Ticks);
        }
        #endregion

        #region Methods - Boolean
        /// <summary>Retreives a randomly generated boolean value.</summary>
        /// <returns>True or False.</returns>
        public static bool GetBoolean()
        {
            return Convert.ToBoolean(Random.Next(0, 2));
        }

        /// <summary>Retrieves a boolean value that is randomly within the probability specified.</summary>
        /// <param name="probability">The probablility (0-1).  Set this high for a higher chance of being true, low for a lower chance of being true</param>
        /// <returns>True or False.</returns>
        public static bool GetBoolean(double probability)
        {
            return Random.NextDouble() < probability;
        }
        #endregion

        #region Methods - Enum
        /// <summary>Gets a random enum value.</summary>
        /// <typeparam name="TEnum">The type of the Enum.</typeparam>
        public static TEnum GetRandomEnum<TEnum>()
        {
            // Setup initial conditions.
            var type = typeof(TEnum);
            if (!type.IsA(typeof(Enum))) throw new ArgumentException("Must specify an Enum type");

            // Get the values and a random index.
            var values = type.GetEnumValues().Cast<TEnum>().ToList();
            var index = Random.Next(0, values.Count());

            // Finish up.
            return values[index];
        }

        /// <summary>Gets a random icon.</summary>
        public static IconImage GetRandomIconEnum()
        {
            return GetRandomEnum<IconImage>();
        }
        #endregion

        #region Methods - File / Domain Name / Email Address
        /// <summary>Retrieves a random file extension.</summary>
        /// <returns>A file extension (without the preceeding period).</returns>
        public static string GetFileExtension()
        {
            return fileExtensions.RandomItem();
        }

        /// <summary>Generates a random file name.</summary>
        /// <returns>A random file name.</returns>
        public static string GetFileName()
        {
            var name = LoremIpsum(Random.Next(1, 3));
            name = string.Format("{0}.{1}", name.Replace(" ", "_"), GetFileExtension());
            return name;
        }

        /// <summary>Generates a random domain name.</summary>
        /// <returns>A domain name.</returns>
        public static string GetDomainName()
        {
            var name = LoremIpsum(Random.Next(1, 3));
            return string.Format("{0}{1}", name.Replace(" ", "-"), topLevelDomains.RandomItem());
        }

        /// <summary>Generates a random http:// address.</summary>
        /// <returns>A domain name.</returns>
        public static Uri GetUri()
        {
            return new Uri("http://" + GetDomainName());
        }

        /// <summary>Generates a random email address.</summary>
        /// <returns>An email address.</returns>
        public static string GetEmail()
        {
            return GetEmail(GetFirstName(), GetLastName());
        }

        /// <summary>Generates a random email address.</summary>
        /// <param name="firstName">The first name of the user.</param>
        /// <param name="lastName">The last name of the user.</param>
        /// <returns>An email address.</returns>
        public static string GetEmail(string firstName, string lastName)
        {
            var name = string.Format("{0}.{1}", firstName, lastName).ToLower();
            return string.Format("{0}@{1}", name.Replace(" ", "."), GetDomainName());
        }
        #endregion

        #region Methods - Person Name
        /// <summary>Generates a random display name.</summary>
        /// <returns>A display name.</returns>
        public static string GetDisplayName()
        {
            var name = GetFirstName();
            if (GetBoolean(8)) name = string.Format("{0} {1}", name, GetFirstName()); // Middle name.
            name = string.Format("{0} {1}", name, GetLastName());
            return name;
        }

        /// <summary>Retrieves a random first name</summary>
        /// <returns>A first name name (of random gender).</returns>
        public static string GetFirstName()
        {
            return GetFirstName(GetGender());
        }

        /// <summary>Retrieves a random first name</summary>
        /// <param name="probabilityMale">The probability that the name is male</param>
        /// <returns>A first name name (of random gender).</returns>
        public static string GetFirstName(double probabilityMale)
        {
            return GetFirstName(GetGender(probabilityMale));
        }

        /// <summary>Retrieves a random first name</summary>
        /// <param name="gender">The gender of the name.</param>
        /// <returns>A name.</returns>
        public static string GetFirstName(GenderFlag gender)
        {
            if ((gender & GenderFlag.Male) == GenderFlag.Male) return firstNamesMale.RandomItem();
            if ((gender & GenderFlag.Female) == GenderFlag.Female) return firstNamesFemale.RandomItem();
            return GetFirstName(); // Male or female.
        }

        /// <summary>Retrieves a random last name</summary>
        /// <returns>A name.</returns>
        public static string GetLastName()
        {
            return lastNames.RandomItem();
        }
        #endregion

        #region Methods - Gender
        /// <summary>Retrieves a random Male/Female gender flag.</summary>
        /// <returns>A gender flag</returns>
        public static GenderFlag GetGender()
        {
            return GetBoolean() ? GenderFlag.Male : GenderFlag.Female;
        }

        /// <summary>Retrieves a random Male/Female gender flag.</summary>
        /// <param name="probabilityMale">The probability that the return value is male.</param>
        /// <returns>A gender flag</returns>
        public static GenderFlag GetGender(double probabilityMale)
        {
            return GetBoolean(probabilityMale) ? GenderFlag.Male : GenderFlag.Female;
        }
        #endregion

        #region Methods - Dates
        /// <summary>Retrieves a random timespan within the given number of days.</summary>
        /// <returns>A random time span.</returns>
        public static TimeSpan GetTimeSpan()
        {
            return GetTimeSpan(Random.Next());
        }

        /// <summary>Retrieves a random timespan within the given number of days.</summary>
        /// <param name="days">The max number of days to create the timespan within.</param>
        /// <returns>A random time span.</returns>
        public static TimeSpan GetTimeSpan(int days)
        {
            return new TimeSpan(Random.Next(days), Random.Next(12), Random.Next(60), Random.Next(60));
        }

        /// <summary>Gets a random date before the current date-time.</summary>
        /// <param name="daysWithin">The number of days to generate the random date within.</param>
        /// <returns>A random date-time.</returns>
        public static DateTime GetDateBeforeNow(int daysWithin)
        {
            return GetDateBefore(DateTime.Now, daysWithin);
        }

        /// <summary>Gets a random date before the current date-time.</summary>
        /// <param name="date">The starting date to generate the date before.</param>
        /// <param name="daysWithin">The number of days to generate the random date within.</param>
        /// <returns>A random date-time.</returns>
        public static DateTime GetDateBefore(DateTime date, int daysWithin)
        {
            return date.Subtract(GetTimeSpan(daysWithin));
        }

        /// <summary>Gets a random date between the to values.</summary>
        /// <param name="start">The starting date.</param>
        /// <param name="end">The ending date.</param>
        /// <returns>A random date-time.</returns>
        public static DateTime GetDateBetween(DateTime start, DateTime end)
        {
            // Setup initial conditions.
            var totalMinutes = end.Subtract(start).TotalMinutes;
            var endRange = (int)totalMinutes - 1;
            if (endRange < 1) endRange = 1;
            var randomMinute = Random.Next(1, endRange);

            // Construct the start date.
            var date = start.Add(new TimeSpan(0, 0, randomMinute, Random.Next(1, 60)));

            // Ensure that the range is correct.
            if (date > end) return end;

            // Finish up.
            return date;
        }

        /// <summary>Retrieves a this time yesterday.</summary>
        /// <returns>A date-time set at yesterday.</returns>
        public static DateTime GetYesterday()
        {
            var now = DateTime.Now;
            return now.Subtract(new TimeSpan(1, 0, 0, 0));
        }
        #endregion

        #region Methods - Lorem Ipsum
        /// <summary>Generates a number of lorem ipsum words without any sentence like structure </summary>
        /// <param name="minWords">The minimum number of words.</param>
        /// <param name="maxWords">The maximum number of words.</param>
        /// <returns>A string of lorem ipsum.</returns>
        public static string LoremIpsum(int minWords, int maxWords)
        {
            return LoremIpsum(Random.Next(minWords, maxWords));
        }

        /// <summary>Generates a number of lorem ipsum words with the option for sentence formatting. (capitalize first letter, ends with a period.)</summary>
        /// <param name="numberOfWords">The number of words to generate.</param>
        public static string LoremIpsum(int numberOfWords)
        {
            // Setup initial conditions.
            if (numberOfWords <= 0) return null;

            // Build the string.
            var builder = new StringBuilder();
            for (var i = 0; i < numberOfWords; i++)
            {
                builder.Append(NextLoremIpsumWord().ToLower());
                if (i < numberOfWords - 1) builder.Append(" ");
            }

            // Finish up.
            return builder.ToString().ToSentenceCase();
        }

        private static int loremIpsumIndex;
        private static string NextLoremIpsumWord()
        {
            return loremIpsumWords[(loremIpsumIndex++ % loremIpsumWords.Length)];
        }
        #endregion
    }
}
