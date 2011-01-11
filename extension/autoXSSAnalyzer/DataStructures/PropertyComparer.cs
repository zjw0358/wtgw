using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

public class PropertyComparer<T> : System.Collections.Generic.IComparer<T> {

    // The following code contains code implemented by Rockford Lhotka:
    // http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnadvnet/html/vbnet01272004.asp

    private PropertyDescriptor _property;
    private ListSortDirection _direction;

    public PropertyComparer(PropertyDescriptor property, ListSortDirection direction) {
        _property = property;
        _direction = direction;
    }

    #region IComparer<T>

    public int Compare(T xWord, T yWord) {
        // Get property values
        object xValue = GetPropertyValue(xWord, _property.Name);
        object yValue = GetPropertyValue(yWord, _property.Name);

        // Determine sort order
        if (_direction == ListSortDirection.Ascending) {
            return CompareAscending(xValue, yValue);
        } else {
            return CompareDescending(xValue, yValue);
        }
    }

    public bool Equals(T xWord, T yWord) {
        return xWord.Equals(yWord);
    }

    public int GetHashCode(T obj) {
        return obj.GetHashCode();
    }

    #endregion

    // Compare two property values of any type
    private int CompareAscending(object xValue, object yValue) {
        int result;

        // If values implement IComparer
        if (xValue is IComparable) {
            result = ((IComparable)xValue).CompareTo(yValue);
        }
            // If values don't implement IComparer but are equivalent
          else if (xValue.Equals(yValue)) {
            result = 0;
        }
            // Values don't implement IComparer and are not equivalent, so compare as string values
          else result = xValue.ToString().CompareTo(yValue.ToString());

        // Return result
        return result;
    }

    private int CompareDescending(object xValue, object yValue) {
        // Return result adjusted for ascending or descending sort order ie
        // multiplied by 1 for ascending or -1 for descending
        return CompareAscending(xValue, yValue) * -1;
    }

    private object GetPropertyValue(T value, string property) {
        // Get property
        PropertyInfo propertyInfo = value.GetType().GetProperty(property);

        // Return value
        return propertyInfo.GetValue(value, null);
    }
}