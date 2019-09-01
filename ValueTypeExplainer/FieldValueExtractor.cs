using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ValueTypeExplainer
{
    /// <summary>
    /// Ermöglicht das Auslesen der Werte einiger Konstanten von Werttypen.
    /// </summary>
    class FieldValueExtractor<T>
    {
        // Wird benötigt um den Datentyp von T zur Laufzeit zubestimmen!
        private readonly T aValue;

        /// <summary>
        /// Gibt den Wert einer Konstanten (z.B. MaxValue) des Werttyp (T) zurück
        /// </summary>
        public T GetFieldInfoForValueType(string fieldName)
        {
            T result;

            if (IsValueType())
            {
                if (IsValidFieldNameForThisDataType(fieldName))
                {
                    result = GetValueOfFieldName(fieldName);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(string.Format("Kann fieldName {0} in Typ {1} nicht finden.", fieldName, typeof(T)));
                }
            }
            else
            {
                throw new InvalidOperationException(string.Format("Typ = {0} ist kein WertType.", typeof(T)));
            }

            return result;
        }

        private T GetValueOfFieldName(string fieldName)
        {
            T result;
            object tmpResult = GetValue(fieldName);

            // Können wir den Wert aus dieser Methode zurück geben?
            if (tmpResult.GetType() == typeof(T))
            {
                result = (T)tmpResult;
            }
            else
            {
                throw new Exception(string.Format("Typ = {0} passt nicht zum Rückgabewert des geforderten Feldes {1} vom Typ {2}.", typeof(T), fieldName, tmpResult.GetType()));
            }

            return result;
        }

        private object GetValue(string fieldName)
        {
            FieldInfo fieldOfaValue = aValue.GetType().GetField(fieldName);

            // Da fieldName eine Konstante ist (die static ist), können wir hier null übergeben
            return fieldOfaValue.GetValue(null);
        }

        private bool IsValidFieldNameForThisDataType(string fieldName)
        {
            FieldInfo fieldOfaValue;

            try
            {
                fieldOfaValue = aValue.GetType().GetField(fieldName);
            }
            catch (ArgumentNullException)
            {
                // Für null gibt es kein Field
                return false;
            }

            return fieldOfaValue != null;
        }

        private bool IsValueType()
        {
            return aValue.GetType().IsValueType;
        }
    }
}
