﻿using Imya.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModManager_Devplayground
{
    internal static class InstanceSearchTester
    {
        internal static void Test_InstanceSearch()
        {
            GameScanner scanner = new GameScanner();
            var x = scanner.GetInstallDirFromRegistry();
        }
    }
}
