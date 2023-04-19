using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskConsoleApp
{
    // ValueTask; Task bir classtır ve referans tipindedir.ValueTask bir struct'tır ve değer tipindedir.(Structlar değer tipindedir.) ValueTask'in amacı memory'yi daha performanslı kullanmaktır.
    public class ValueTask
    {
        public static int cacheData { get; set; } = 150;
        public async static Task ValueTaskExam()
        {
            // Eğer bu datanın hemen alınacağı biliniyorsa, masraflı olan Task dönmek yerine, belleğin stack bölgesinde tutulan bir Task dönmek daha mantıklıdır.
            await GetData();
        }

        public static ValueTask<int> GetData()
        {
            return new ValueTask<int>(cacheData);
        }
    }
}
