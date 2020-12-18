using System;
using System.Reflection;

/// <summary>
/// A static class for reflection type functions
/// </summary>
public static class Reflection
{
    /// <summary>
    /// Extension for 'Object' that copies the properties to a destination object.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="destination">The destination.</param>
    public static void CopyProperties(this object source, object destination)
    {
        // If any this null throw an exception
        if (source == null || destination == null)
        {
            throw new Exception("Source or/and Destination Objects are null");
        }
        // Getting the Types of the objects
        Type typeDest = destination.GetType();
        Type typeSrc = source.GetType();

        // Iterate the Properties of the source instance and
        // populate them from their desination counterparts
        PropertyInfo[] srcProps = typeSrc.GetProperties();

        for (int i = 0; i < srcProps.Length; i++)
        {
            if (!srcProps[i].CanRead)
            {
                continue;
            }
            PropertyInfo targetProperty = typeDest.GetProperty(srcProps[i].Name);
            if (targetProperty == null)
            {
                continue;
            }
            if (!targetProperty.CanWrite)
            {
                continue;
            }
            if (targetProperty.GetSetMethod(true) != null && targetProperty.GetSetMethod(true).IsPrivate)
            {
                continue;
            }
            if ((targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) != 0)
            {
                continue;
            }
            if (!targetProperty.PropertyType.IsAssignableFrom(srcProps[i].PropertyType))
            {
                continue;
            }
            // Passed all tests, lets set the value
            targetProperty.SetValue(destination, srcProps[i].GetValue(source, null), null);
        }
    }
}