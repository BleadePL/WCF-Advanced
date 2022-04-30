using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfWcfClient.MangaReference;

namespace WpfWcfClient
{
    public class CallBack : IMangaCallback
    {

        MainWindow MainWindowHandle { get; }

        public CallBack(MainWindow window)
        {
            MainWindowHandle = window;
        }

        public void ContainsResult(bool result)
        {
            MainWindowHandle.ContainsString = result.ToString();
        }
    }
}
