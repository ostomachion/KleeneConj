﻿using System;
using System.Collections.Generic;

namespace KleeneConj
{
    public abstract class Expression
    {
        public abstract IEnumerable<ResultTree> Run();
    }
}
