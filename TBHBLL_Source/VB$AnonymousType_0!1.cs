using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[DebuggerDisplay(@"\{{AtDebuggerDisplay,nq}\}", Type="<anonymous type>"), CompilerGenerated]
internal sealed class VB$AnonymousType_0<T0>
{
    private T0 $Votes;

    [DebuggerNonUserCode]
    public VB$AnonymousType_0(T0 Votes)
    {
        this.$Votes = Votes;
    }

    [DebuggerNonUserCode]
    private static string AtDebuggerDisplayFormatOutput(object p)
    {
        if (p == null)
        {
            return "Nothing";
        }
        Type c = p.GetType();
        Type type = typeof(IEnumerable<>);
        if (c.IsPrimitive)
        {
            return p.ToString();
        }
        if (c == typeof(string))
        {
            return ("\"" + ((string) p) + "\"");
        }
        if ((((type != null) && type.IsAssignableFrom(c)) ? 1 : 0) != 0)
        {
            return "<enumerable type>";
        }
        string name = c.Name;
        int num = 0;
        int length = name.Length;
        while (num < length)
        {
            char ch = name[num];
            if (((((Conversions.ToString(ch) == "[") || (Conversions.ToString(ch) == "]")) ? 1 : 0) == 0) && (((!char.IsLetterOrDigit(ch) && (ch != '_')) ? 1 : 0) != 0))
            {
                return "<generated type>";
            }
            num++;
        }
        return ("{" + c.Name + "}");
    }

    [DebuggerNonUserCode]
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("{ ");
        builder.AppendFormat("{0} = {1} ", "Votes", this.$Votes);
        builder.Append("}");
        return builder.ToString();
    }

    private string AtDebuggerDisplay
    {
        [DebuggerNonUserCode]
        get
        {
            return string.Format("Votes = {0}", VB$AnonymousType_0<T0>.AtDebuggerDisplayFormatOutput(this.$Votes));
        }
    }

    public T0 Votes
    {
        [DebuggerNonUserCode]
        get
        {
            return this.$Votes;
        }
        [DebuggerNonUserCode]
        set
        {
            this.$Votes = value;
        }
    }
}

