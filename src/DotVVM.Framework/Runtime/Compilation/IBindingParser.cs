﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.Runtime.ControlTree;

namespace DotVVM.Framework.Runtime.Compilation
{
    public interface IBindingParser
    {
        Expression Parse(string expression, DataContextStack dataContexts, BindingParserOptions options);
    }
}
