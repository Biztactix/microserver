﻿using MicroServer.Net.Http.Exceptions;
using System;
using System.Collections;

namespace MicroServer.Net.Http.Messages
{
    /// <summary>
    ///     Collection of HTTP headers
    /// </summary>
    /// <remarks>The values are not encoded, you must encode them when serializing the message.</remarks>
    public class HeaderCollection : IHeaderCollection
    {
        //private readonly Action<string, string> _headerSetCallback;
        private readonly Hashtable _items = new Hashtable();

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderCollection"/> class.
        /// </summary>
        public HeaderCollection()
        {
            //_headerSetCallback = (s, s1) => { };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderCollection"/> class.
        /// </summary>
        /// <param name="headerSetCallback">Callback invoked every time a new header is set.</param>
        /// <exception cref="System.ArgumentNullException">headerSetCallback</exception>
        //public HeaderCollection(Action<string, string> headerSetCallback)
        //{
        //    if (headerSetCallback == null) throw new ArgumentNullException("headerSetCallback");
        //    _headerSetCallback = headerSetCallback;
        //}

        /// <summary>
        /// Number of headers
        /// </summary>
        public int Count { get { return _items.Count; } }

        /// <summary>
        /// Used to fetch headers
        /// </summary>
        /// <param name="name">Lower case name</param>
        /// <returns>header if found; otherwise <c>null</c>.</returns>
        public string this[string name]
        {
            get
            {
                if (_items.Contains(name))
                {
                    return (string)_items[name];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _items[name] = value;
                //_headerSetCallback(name, value);
            }
        }

   
        /// <summary>
        ///     Adds the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.ArgumentNullException">name</exception>
        /// <exception cref="System.FormatException">
        ///     Header name may not contain colon, CR or LF.
        ///     or
        ///     Header value may not contain colon, CR or LF.
        /// </exception>
        /// <remarks>
        ///     <para>If a client or a server receives repeated frame header entries, only the first header entry SHOULD be used as the value of header entry. Subsequent values are only used to maintain a history of state changes of the header and MAY be ignored. This implementation will IGNORE all subsequent headers</para>
        /// </remarks>
        public void Add(string name, string value)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (name.IndexOfAny(new[] {':', '\r', '\n'}) != -1)
                throw new FormatException("Header name may not contain colon, CR or LF; Name: " + name);

            if (_items.Contains(name))
                throw new ArgumentOutOfRangeException("name", "Header has already been added.");

            _items[name] = value;
            //_headerSetCallback(name, value);
        }

        /// <summary>
        /// Checks if the specified header exists in the collection
        /// </summary>
        /// <param name="name">Name, case insensitive</param>
        /// <returns>
        ///   <c>true</c> if found; otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">name</exception>
        public bool Contains(string name)
        {
            if (name == null) throw new ArgumentNullException("name");

            return _items.Contains(name);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}