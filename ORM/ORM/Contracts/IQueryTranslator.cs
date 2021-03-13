using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Contracts
{
    public interface IQueryTranslator<T, K>
    {
        K Translate(T query);
    }
}
