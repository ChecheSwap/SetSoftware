using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Management.Instrumentation;

/*
PrinterStatus by: @CheCheSwap {Custom Async table in Stmt32 Operating System capability}

Data type: uint16
Access type: Read-only
Qualifiers: MappingStrings ("MIB.IETF|Printer-MIB.hrPrinterStatus")
Status information for a printer that is different from information specified in the logical device Availability property.

This property is inherited from CIM_Printer.

Other (1)

Unknown (2)

Idle (3)

Printing (4)

Warmup (5)

Warming Up

Stopped Printing (6)

Offline (7)

*/

namespace MundoMusical.CONFIGURACION_DE_IMPRESION
{
    public static class printerControl
    {
        public static bool printIsSafe(string name){

            bool ready = false;

            if (name == null || name == string.Empty)
            {
                return ready;
            }
             
            string query =$"SELECT * from Win32_Printer WHERE Name LIKE '%{name}%'";

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))

                using (ManagementObjectCollection resquery = searcher.Get())
                {
                    try
                    {
                        foreach (ManagementObject printer in resquery)
                        {
                            if (!printer["WorkOffline"].ToString().ToUpper().Equals("TRUE"))
                            {
                                if (printer.Properties["PrinterStatus"].Value.ToString() == "3")
                                {
                                    ready = true;
                                }
                            }                    
                        }
                    }
                    catch (ManagementException ex)
                    {
                        genericDefinitions.error(ex.ToString());
                    }
                }

            return ready;
        }
    }
}
