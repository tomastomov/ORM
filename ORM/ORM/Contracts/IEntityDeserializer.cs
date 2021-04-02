﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Contracts
{
    public interface IEntityDeserializer
    {
        T Deserialize<T>(IDatabaseDataReader reader);
    }
}
