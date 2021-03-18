using ORM.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Contracts
{
    public interface IConstraintTranslator
    {
        string Translate(IConstraint constraint);
    }
}
