﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValueTypeExplainer
{
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
            return aValue.GetType().GetField(fieldName).GetValue(aValue.GetType().GetField(fieldName));
        }

        private bool IsValidFieldNameForThisDataType(string fieldName)
        {
            return fieldName?.Length > 0 && aValue.GetType().GetFields().Where(f => f.Name == fieldName).FirstOrDefault() != null;
        }

        private bool IsValueType()
        {
            return aValue.GetType().IsValueType;
        }
    }
}
