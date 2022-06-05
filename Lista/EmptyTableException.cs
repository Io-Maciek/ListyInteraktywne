using System;
using System.Collections.Generic;
using System.Text;

namespace Listy
{
    /// <summary>
    /// Reprezentuje błąd, który wystąpi, gdy tabela z wyborami jest pusta
    /// </summary>
    public class EmptyTableException : Exception
    {
        /// <summary>
        /// Tworzy nowe wystąpienie klasy <c>EmptyTableException</c>
        /// </summary>
        public EmptyTableException() : base("Tabela z wyborami jest pusta") { }
    }
}
