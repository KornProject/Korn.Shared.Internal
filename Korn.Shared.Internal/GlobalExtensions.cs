using System;
using System.Reflection;

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
            for (var i = 0; i < parameters.Length; i++)
                arguments[i + 1] = parameters[i].ParameterType;

            arguments[0] = method.DeclaringType;

            return arguments;
        }
    }
}