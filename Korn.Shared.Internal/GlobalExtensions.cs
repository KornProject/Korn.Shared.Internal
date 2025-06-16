using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

public static class KornSharedGlobalExtensions
{
    public static string[] SplitEx(this string self, char character, StringSplitOptions options)
    {
#if NET8_0
        return self.Split(character, options);
#elif NET472
        return self.Split(new char[] { character }, options);
#endif
    }

    public static string[] SplitEx(this string self, string splitter, StringSplitOptions options = StringSplitOptions.None)
    {
#if NET8_0
        return self.Split(splitter, options);
#elif NET472
        return self.Split(new string[] { splitter }, options);
#endif
    }

    public static byte[] GetBytesEx(this Encoding encoding, string input, int index, int length)
    {
#if NET8_0
        return encoding.GetBytes(input, index, length);
#elif NET472
        return encoding.GetBytes(input);
#endif
    }

    public static MethodInfo GetMethodEx(this Type type, string name)
    {
        var methods = type.GetRuntimeMethods();
        foreach (var method in methods)
            if (method.Name == name)
                return method;
        return null;
    }

    public static MethodInfo GetMethodEx(this Type type, string name, params Type[] arguments)
    {
        var methods = type.GetRuntimeMethods();
        foreach (var method in methods)
            if (method.Name == name)
            {
                var parameters = method.GetParameters();
                if (arguments.Length != parameters.Length)
                    continue;

                for (var argIndex = 0; argIndex < arguments.Length; argIndex++)
                    if (arguments[argIndex] != parameters[argIndex].ParameterType)
                        continue;

                return method;
            }
        return null;
    }
        
    // Unlike the original MethodInfo.GetParameters this method adds to the parameters of this.
    public static Type[] GetArgumentsEx(this MethodInfo method)
    {
        var parameters = method.GetParameters();
        if (method.IsStatic)
        {
            var arguments = new Type[parameters.Length];
            for (var i = 0; i < parameters.Length; i++)
                arguments[i] = parameters[i].ParameterType;

            return arguments;
        }
        else
        {
            var arguments = new Type[parameters.Length + 1];
            arguments[0] = method.DeclaringType;
            for (var i = 1; i < arguments.Length; i++)
                arguments[i] = parameters[i - 1].ParameterType;

            return arguments;
        }
    }

    public static Type[] GetParametersEx(this MethodInfo method)
    {
        var parameters = GetArgumentsEx(method);
        var returnType = method.ReturnType;
        if (returnType != typeof(void))
        {
            Array.Resize(ref parameters, parameters.Length + 1);
            parameters[parameters.Length - 1] = returnType;
        }

        return parameters;
    }

    public static string ToHexString(this IntPtr nativeInteger) => Convert.ToString((long)nativeInteger, 16);
}