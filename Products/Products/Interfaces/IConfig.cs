namespace Products.Interfaces
{
    using SQLite.Net.Interop;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IConfig
    {
        string DirectoryDB { get; }

        ISQLitePlatform Platform { get; }

    }
}
