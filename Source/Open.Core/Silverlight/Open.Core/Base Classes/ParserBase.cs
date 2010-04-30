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
using System.IO;
using System.Linq;

namespace Open.Core.Common
{
    /// <summary>Provides common base functionality for parsing files.</summary>
    public abstract class ParserBase<TModel>
    {
        #region Head
        private IEnumerable<TModel> models;
        private string delimiter;
        private static readonly string defaultDelimiter = string.Format("\t");

        /// <summary>Constructor.</summary>
        /// <param name="stream">Stream containing the CSV file (NB: This will cause the stream to be closed).</param>
        protected ParserBase(Stream stream) : this(ReadStream(stream)) { }

        /// <summary>Constructor.</summary>
        /// <param name="rawText">The raw CSV text.</param>
        protected ParserBase(string rawText)
        {
            RawText = rawText.AsNullWhenEmpty();
            Lines = RawText == null 
                                ? new string[] {}
                                : RawText.Split(Environment.NewLine.ToCharArray()).Where(m => !m.IsNullOrEmpty(true));

            Delimiter = defaultDelimiter;
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the CSV delimiter character.</summary>
        public string Delimiter
        {
            get { return delimiter; }
            set
            {
                delimiter = value.AsNullWhenEmpty();
                models = null;
            }
        }

        /// <summary>Gets the raw CSV text.</summary>
        public string RawText { get; private set; }

        /// <summary>Gets the collection of lines that make up the file.</summary>
        public IEnumerable<string> Lines { get; private set; }

        /// <summary>Gets the collection of models parsed out of the CSV file.</summary>
        public IEnumerable<TModel> Models { get { return models ?? (models = Parse()); } }
        #endregion

        #region Methods - Protected
        /// <summary>Implemented in the deriving class to convert the fields from the given line into it's corresonding model.</summary>
        /// <param name="fields">The collection of fields split from a single line within the CSV file.</param>
        protected abstract TModel CreateModel(string[] fields);




        #endregion

        #region Internal
        private static string ReadStream(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            using (stream)
            {
                var reader = new StreamReader(stream);
                var value = reader.ReadToEnd();
                reader.Close();
                return value;
            }
        }

        private IEnumerable<TModel> Parse()
        {
            // Setup initial conditions.
            if (Lines.Count() == 0) return new TModel[] { };
            var list = new List<TModel>();

            // Enumerate the collection of lines within the CSV file.
            foreach (var line in Lines)
            {
                var fields = line.Split((Delimiter ?? defaultDelimiter).ToCharArray());
                list.Add(CreateModel(fields));
            }

            // Finish up.
            return list;
        }
        #endregion
    }
}
