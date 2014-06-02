using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web;

public class ObjectDumper
{
    private int _level;
    private readonly int _indentSize;
    private readonly StringBuilder _stringBuilder;
    private readonly List<int> _hashListOfFoundElements;
    private int maxElementsInList = 3;
    private int maxCharsInString = 100;

    private Stack<string> _stack;

    private ObjectDumper(int indentSize)    
    {
        _indentSize = indentSize;
        _stringBuilder = new StringBuilder();
        _hashListOfFoundElements = new List<int>();
        _stack = new Stack<string>();
    }

    public static string Dump(object element)
    {
        return Dump(element, 2);
    }

    public static string Dump(object element, int indentSize)
    {
        var instance = new ObjectDumper(indentSize);
        return instance.DumpElement(element);
    }

    private string DumpElement(object element)
    {
        
        if (element == null || element is ValueType || element is string)
        {
            _stack.Push("Model");
            Write(FormatValue(element));
        }
        else
        {
            var objectType = element.GetType();
            if (!typeof(IEnumerable).IsAssignableFrom(objectType))
            {
                _stack.Push(objectType.FullName);
                Write("{{{0}}}", objectType.FullName);
                
                _hashListOfFoundElements.Add(element.GetHashCode());
                _level++;
            }
            else if (_level == 0)
            {
                Write("[collection]");
            }

            var enumerableElement = element as IEnumerable;
            if (enumerableElement != null)
            {
                int ElemInList = 0;
                foreach (object item in enumerableElement)
                {
                    
                    if (item is IEnumerable && !(item is string))
                    {
                        _level++;
                        DumpElement(item);
                        _level--;
                    }
                    else
                    {
                        if (!AlreadyTouched(item))
                            DumpElement(item);
                        else
                            Write("{{{0}}} <-- bidirectional reference found", item.GetType().FullName);
                    }
                    ElemInList++;
                    if (ElemInList >= maxElementsInList)
                    {
                        Write("{{...}}");
                        break;
                    }
                    
                }
            }
            else
            {
                MemberInfo[] members = element.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance);
                foreach (var memberInfo in members)
                {
                    var fieldInfo = memberInfo as FieldInfo;
                    var propertyInfo = memberInfo as PropertyInfo;

                    if (fieldInfo == null && propertyInfo == null)
                        continue;

                    if (propertyInfo != null && propertyInfo.GetIndexParameters().Length > 0)
                        continue;

                    if (propertyInfo != null && propertyInfo.DeclaringType.FullName == "DotLiquid.DropBase")
                        continue;

                    var type = fieldInfo != null ? fieldInfo.FieldType : propertyInfo.PropertyType;
                    object value = fieldInfo != null
                                       ? fieldInfo.GetValue(element)
                                       : propertyInfo.GetValue(element, null);

                    _stack.Push(memberInfo.Name);
                    if (type.IsValueType || type == typeof(string) || value == null)
                    {                        
                        Write("<b>{0}</b>: {1}", memberInfo.Name, FormatValue(value));
                    }
                    else
                    {
                        var isEnumerable = typeof(IEnumerable).IsAssignableFrom(type);
                        Write("<b>{0}</b>: {1}", memberInfo.Name, isEnumerable ? "[collection]" : "{ }");

                        var alreadyTouched = !isEnumerable && AlreadyTouched(value);
                        _level++;
                        if (!alreadyTouched)
                            DumpElement(value);
                        else
                            Write("{{{0}}} <-- bidirectional reference found", value.GetType().FullName);
                        _stack.Pop();
                        _level--;
                    }
                }
            }

            if (!typeof(IEnumerable).IsAssignableFrom(objectType))
            {
                _stack.Pop();
                _level--;
            }
        }

        return _stringBuilder.ToString();
    }

    private bool AlreadyTouched(object value)
    {
        var hash = value.GetHashCode();
        for (var i = 0; i < _hashListOfFoundElements.Count; i++)
        {
            if (_hashListOfFoundElements[i] == hash)
                return true;
        }
        return false;
    }

    private void Write(string value, params object[] args)
    {
        var space = HttpUtility.HtmlEncode(new string(' ', _level * _indentSize)).Replace(" ", "&nbsp;");

        if (args != null)
            value = string.Format(value, args);

        _stringBuilder.Append(space + value)/*.Append( string.Join(", ",_stack.ToArray()))*/.Append("<br />");
    }

    private string FormatValue(object o)
    {
        if (o == null)
            return ("null");

        if (o is DateTime)
            return (((DateTime)o).ToShortDateString());

        if (o is string) {
            string s = o as string;
            if (s.Length > maxCharsInString)
                s = s.Substring(0, maxCharsInString) + "...";
            return string.Format("\"{0}\"", HttpUtility.HtmlEncode(s));
        }
        if (o is ValueType)
            return (o.ToString());

        if (o is IEnumerable)
            return ("[collection]");

        return ("[object]");
    }
}