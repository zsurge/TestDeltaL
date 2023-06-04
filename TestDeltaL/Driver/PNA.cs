using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace TestDeltaL
{

    //public static class InstrumentCommands
    //{
    //    public const string Marker1Command = ":CALC1:MARKer1:Y?";
    //    public const string Marker2Command = ":CALC1:MARKer2:Y?";
    //    public const string Marker3Command = ":CALC1:MARKer3:Y?";
    //}


    //int result1 = RealTimeMeasure(nInstrumentHandle, InstrumentCommands.Marker1Command);
    //int result2 = RealTimeMeasure(nInstrumentHandle, InstrumentCommands.Marker2Command);
    //int result3 = RealTimeMeasure(nInstrumentHandle, InstrumentCommands.Marker3Command);


    public class PNA
    {
        private static int resourceMgr;
        public PNA()
        {
            resourceMgr = 0;
        }
        public static int Open(string resourceName, ref int nHandle)
        {
            int viError;

            viError = visa32.viOpenDefaultRM(out resourceMgr);
            if (viError != visa32.VI_SUCCESS)
            {
                return viError;
            }

            viError = visa32.viOpen(resourceMgr, resourceName, visa32.VI_NO_LOCK, visa32.VI_TMO_IMMEDIATE, out nHandle);
            if (viError != visa32.VI_SUCCESS)
            {
                return viError;
            }



            //StringBuilder attr = new StringBuilder();
            //viError = visa32.viGetAttribute(nHandle, visa32.VI_ATTR_RSRC_CLASS, attr);
            //viError = visa32.viSetAttribute(nHandle, visa32.VI_ATTR_TERMCHAR_EN, visa32.VI_TRUE);
            //viError = visa32.viSetAttribute(nHandle, visa32.VI_ATTR_TERMCHAR, 0x0A);
            //viError = visa32.viSetAttribute(nHandle, visa32.VI_ATTR_TMO_VALUE, 2000);

            return viError;
        }

        //设置属性
        public static int setAttribute(int nInstrumentHandle)
        {
            int viError = 0, count = 0;
            byte[] result = new byte[256];
            int attr;

            string str1 = ":SYSTem:BEEPer:WARNing:STATe OFF";

            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str1 + "\n"), str1.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str2 = ":FORMat:DATA ASCii";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str2 + "\n"), str2.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str3 = ":FORMat:BORDer NORMal";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str3 + "\n"), str3.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str4 = ":CALCulate1:SELected:TRANsform:TIME:STARt -5E-10";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str4 + "\n"), str4.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str5 = ":CALCulate1:SELected:TRANsform:TIME:STOP 9.5E-09";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str5 + "\n"), str5.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str6 = ":DISPlay:WINDow1:TRACe1:Y:SCALe:RLEVel 20";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str6 + "\n"), str6.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str7 = ":DISPlay:WINDow1:TRACe1:Y:SCALe:PDIVision 10";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str7 + "\n"), str7.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str8 = ":DISPlay:WINDow1:TRACe2:Y:SCALe:RLEVel 20";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str8 + "\n"), str8.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str9 = ":DISPlay:WINDow1:TRACe2:Y:SCALe:PDIVision 10";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str9 + "\n"), str9.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str10 = "*OPC?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str10 + "\n"), str10.Length, out count);
            visa32.viRead(nInstrumentHandle, result, 256, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str11 = ":DISPlay:UPDate";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str11 + "\n"), str11.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            visa32.viClear(nInstrumentHandle);

            return viError;
        }


        public static int ExecuteCommand(int nInstrumentHandle, string command)
        {
            int errorno, count;
            errorno = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(command + "\n"), command.Length, out count);
            string response;
            return QueryErrorStatus(nInstrumentHandle, out response);
        }

        public static int ExecuteCmd(int nInstrumentHandle, string command)
        {
            int count;
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(command + "\n"), command.Length, out count);
            return count;
        }

        public static int QueryCommand(int nInstrumentHandle, string command, out string response)
        {
            int errorno, count;
            byte[] result = new byte[256];
            errorno = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(command + "\n"), command.Length, out count);
            errorno = visa32.viRead(nInstrumentHandle, result, 256, out count);
            response = Encoding.ASCII.GetString(result, 0, count);
            return errorno;
        }

        public static int QueryCommand(int nInstrumentHandle, string command, out string response, int len)
        {
            int count;
            byte[] result = new byte[len];

            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(command + "\n"), command.Length, out count);
            visa32.viRead(nInstrumentHandle, result, len, out count);

            response = Encoding.ASCII.GetString(result, 0, count);

            return count;
        }


        /* *IDN? */
        public static int GetInstrumentIdentifier(int nInstrumentHandle, out string idn)
        {
            int viError;
            int count = 0;
            string command = "*IDN?\n";
            byte[] response = new byte[256];

            viError = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(command), command.Length, out count);
            if (viError != visa32.VI_SUCCESS)
            {
                idn = string.Empty;
                return viError;
            }
            viError = visa32.viRead(nInstrumentHandle, response, 256, out count);
            if (viError != visa32.VI_SUCCESS)
            {
                idn = string.Empty;
                return viError;
            }

            idn = Encoding.ASCII.GetString(response, 0, count);
            return viError;
        }

        /* :SYST:PRES */
        public static int Preset(int nInstrumentHandle)
        {
            int errorno;
            string command = ":SYSTem:PRESet\n";
            byte[] response = new byte[128];
            int count = 0;

            errorno = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(command), command.Length, out count);
            if (errorno != visa32.VI_SUCCESS)
            {
                return errorno;
            }

            command = "*CLS\n";
            errorno = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(command), command.Length, out count);

            command = "SYSTem:ERRor?\n";
            errorno = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(command), command.Length, out count);
            errorno = visa32.viRead(nInstrumentHandle, response, 128, out count);
            if (errorno != visa32.VI_SUCCESS)
            {
                return errorno;
            }

            string[] array = new string[2];
            array = Encoding.ASCII.GetString(response, 0, count).Split(',');
            errorno = Convert.ToInt32(array[0]);
            return errorno;
        }

        /* *CLS */
        public static int ClearAllErrorQueue(int nInstrumentHandle)
        {
            int error = 0, count = 0;
            string command = "*CLS\n", response;
            error = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(command), command.Length, out count);
            return QueryErrorStatus(nInstrumentHandle, out response);
        }

        /* *CLS */
        public static int ClearInstrument(int nInstrumentHandle)
        {
            int errorno, count;
            string command = "*CLS\n",     /* Clean up all of the instrument, including the entire error queue */
                   response;
            errorno = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(command), command.Length, out count);
            return QueryErrorStatus(nInstrumentHandle, out response);      /* Make sure all errors had been cleaned up. */
        }

        public static void viClear(int nInstrumentHandle)
        {
            visa32.viClear(nInstrumentHandle);
        }

        public static void viClose(int nInstrumentHandle)
        {
            visa32.viClose(nInstrumentHandle);
        }

        /* SYST:ERR? */
        public static int QueryErrorStatus(int nInstrumentHandle, out string errorMesg)
        {
            int errorno, count;
            string command = "SYSTem:ERRor?\n";
            byte[] response = new byte[256];

            errorno = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(command), command.Length, out count);
            errorno = visa32.viRead(nInstrumentHandle, response, 256, out count);
            if (errorno != visa32.VI_SUCCESS)
            {
                errorMesg = "VISA library error : " + errorno;
                return errorno;
            }

            string[] messages = new string[2];
            messages = Encoding.ASCII.GetString(response, 0, count).Split(',');
            errorno = Convert.ToInt32(messages[0]);
            errorMesg = messages[1];
            return errorno;
        }

        //public static int real_time_query(int nInstrumentHandle)
        //{
        //    int viError = 0, count = 0, attr = 0;
        //    byte[] result = new byte[256];
        //    byte[] ret = new byte[200100];

        //    string str1 = ":CALC1:MARK1 ON";
        //    visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str1 + "\n"), str1.Length, out count);
        //    viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

        //    string str2 = "CALC1:MARK1:X 4E9";
        //    visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str2 + "\n"), str2.Length, out count);
        //    viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

        //    string str3 = ":CALC1:MARK2 ON";
        //    visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str3 + "\n"), str3.Length, out count);
        //    viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

        //    string str4 = ":CALC1:MARK2:X 8E9";
        //    visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str4 + "\n"), str4.Length, out count);
        //    viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

        //    string str5 = ":CALC1:MARK3 ON";
        //    visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str5 + "\n"), str5.Length, out count);
        //    viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

        //    string str6 = ":CALC1:MARK3:X 12.89E9";
        //    visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str6 + "\n"), str6.Length, out count);
        //    viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

        //    string str7 = ":TRIG:SOUR IMM";
        //    visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str7 + "\n"), str7.Length, out count);
        //    viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);



        //    return viError;
        //}

        public static int real_time_query(int nInstrumentHandle)
        {
            int viError = 0, count = 0, attr = 0;
            byte[] result = new byte[256];
            byte[] ret = new byte[200100];

            string[] commands = {
            "FORM:DATA ASCII",
            "DISP:WIND4:TRAC:SEL",
            ":CALC1:MARK1 ON",
            ":CALC1:MARK1:X 4E9",
            ":CALC1:MARK2 ON",
            ":CALC1:MARK2:X 8E9",
            ":CALC1:MARK3 ON",
            ":CALC1:MARK3:X 12.89E9",
            ":TRIG:SOUR IMM"
            };

            foreach (string command in commands)
            {
                visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(command + "\n"), command.Length, out count);
                viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            }

            return viError;
        }


        //    public const string Marker1Command = ":CALC1:MARKer1:Y?";
        //    public const string Marker2Command = ":CALC1:MARKer2:Y?";
        //    public const string Marker3Command = ":CALC1:MARKer3:Y?";

        //int result1 = RealTimeMeasure(nInstrumentHandle, InstrumentCommands.Marker1Command);
        //int result2 = RealTimeMeasure(nInstrumentHandle, InstrumentCommands.Marker2Command);
        //int result3 = RealTimeMeasure(nInstrumentHandle, InstrumentCommands.Marker3Command);

        public static int RealTimeMeasure(int nInstrumentHandle, string command, out string msg)
        {
            int viError = 0, count = 0, attr = 0;
            byte[] result = new byte[256];
            byte[] ret = new byte[200100];

            Array.Clear(ret, 0, 200100);
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(command + "\n"), command.Length, out count);
            viError = visa32.viRead(nInstrumentHandle, ret, 200100, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            msg = Encoding.ASCII.GetString(ret, 0, count).Replace("+0.00000000000E+000,", "");

            return viError;
        }


        public static int calc_short(int nInstrumentHandle,out string msg)
        {
            int viError, count;
            int attr;
            byte[] result = new byte[256];
            byte[] ret = new byte[200100];

            string str1 = "DISP:WIND4:TRAC:SEL";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str1 + "\n"), str1.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str2 = ":INIT1:CONT ON";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str2 + "\n"), str2.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str3 = "DISP:WIND4:TRAC:SEL";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str3 + "\n"), str3.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str4 = ":TRIG:SOUR IMM";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str4 + "\n"), str4.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str5 = ":INIT1:CONT OFF";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str5 + "\n"), str5.Length, out count);
            visa32.viRead(nInstrumentHandle, result, 256, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str6 = "DISP:WIND4:TRAC:SEL";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str6 + "\n"), str6.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            visa32.viClear(nInstrumentHandle);

            string str7 = ":CALC1:DATA? FDAT";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str7 + "\n"), str7.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            viError = visa32.viRead(nInstrumentHandle, ret, 200100, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            msg = Encoding.ASCII.GetString(ret, 0, count).Replace("+0.00000000000E+000,", "");


            string str8 = "DISP:WIND4:TRAC2:SEL;*WAI";
            viError = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str8 + "\n"), str8.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);


            string str9 = "CALC:FSIM:BAL:PAR:STAT OFF";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str9 + "\n"), str9.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str10 = "CALC:DATA:SNP:PORT:SAVE '1,2,3,4','Y:MyData_Short.s4p';*WAI";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str10 + "\n"), str10.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str11 = "DISP:WIND4:TRAC:SEL;";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str11 + "\n"), str11.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str12 = ":TRIG:SOUR IMM";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str12 + "\n"), str12.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str13 = ":INIT1:CONT ON";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str13 + "\n"), str13.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str14 = "DISP:WIND1:TRAC:SEL;*WAI";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str14 + "\n"), str14.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            visa32.viClear(nInstrumentHandle);

            string str15 = ":CALCulate1:DATA? FDATa";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str15 + "\n"), str15.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            viError = visa32.viRead(nInstrumentHandle, ret, 200100, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            msg = Encoding.ASCII.GetString(ret, 0, count).Replace("+0.00000000000E+000,", "");

            string str16 = ":CALCulate1:TRANsform:TIME:STARt?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str16 + "\n"), str16.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            viError = visa32.viRead(nInstrumentHandle, ret, 200100, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            msg = Encoding.ASCII.GetString(ret, 0, count).Replace("+0.00000000000E+000,", "");

            string str17 = ":CALCulate1:TRANsform:TIME:STOp?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str17 + "\n"), str17.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            viError = visa32.viRead(nInstrumentHandle, ret, 200100, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            msg = Encoding.ASCII.GetString(ret, 0, count).Replace("+0.00000000000E+000,", "");

            string str18 = ":SENSe1:SWEep:POINts?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str18 + "\n"), str18.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            viError = visa32.viRead(nInstrumentHandle, ret, 200100, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            msg = Encoding.ASCII.GetString(ret, 0, count).Replace("+0.00000000000E+000,", "");


            return viError;
        }

        public static int calc_medium(int nInstrumentHandle, out string msg)
        {
            int viError, count;
            int attr;
            byte[] result = new byte[256];
            byte[] ret = new byte[200100];

            string str1 = "DISP:WIND4:TRAC:SEL";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str1 + "\n"), str1.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str2 = ":INIT1:CONT ON";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str2 + "\n"), str2.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str3 = "DISP:WIND4:TRAC:SEL";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str3 + "\n"), str3.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str4 = ":TRIG:SOUR IMM";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str4 + "\n"), str4.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str5 = ":INIT1:CONT OFF";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str5 + "\n"), str5.Length, out count);
            visa32.viRead(nInstrumentHandle, result, 256, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str6 = "DISP:WIND4:TRAC:SEL";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str6 + "\n"), str6.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            visa32.viClear(nInstrumentHandle);

            string str7 = ":CALC1:DATA? FDAT";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str7 + "\n"), str7.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            viError = visa32.viRead(nInstrumentHandle, ret, 200100, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            msg = Encoding.ASCII.GetString(ret, 0, count).Replace("+0.00000000000E+000,", "");


            string str8 = "DISP:WIND4:TRAC2:SEL;*WAI";
            viError = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str8 + "\n"), str8.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);


            string str9 = "CALC:FSIM:BAL:PAR:STAT OFF";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str9 + "\n"), str9.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str10 = "CALC:DATA:SNP:PORT:SAVE '1,2,3,4','Y:MyData_Medium.s4p';*WAI";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str10 + "\n"), str10.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str11 = "DISP:WIND4:TRAC:SEL;";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str11 + "\n"), str11.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str12 = ":TRIG:SOUR IMM";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str12 + "\n"), str12.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str13 = ":INIT1:CONT ON";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str13 + "\n"), str13.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str14 = "DISP:WIND1:TRAC:SEL;*WAI";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str14 + "\n"), str14.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            visa32.viClear(nInstrumentHandle);

            string str15 = ":CALCulate1:DATA? FDATa";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str15 + "\n"), str15.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            viError = visa32.viRead(nInstrumentHandle, ret, 200100, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            msg = Encoding.ASCII.GetString(ret, 0, count).Replace("+0.00000000000E+000,", "");

            string str16 = ":CALCulate1:TRANsform:TIME:STARt?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str16 + "\n"), str16.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            viError = visa32.viRead(nInstrumentHandle, ret, 200100, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            msg = Encoding.ASCII.GetString(ret, 0, count).Replace("+0.00000000000E+000,", "");

            string str17 = ":CALCulate1:TRANsform:TIME:STOp?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str17 + "\n"), str17.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            viError = visa32.viRead(nInstrumentHandle, ret, 200100, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            msg = Encoding.ASCII.GetString(ret, 0, count).Replace("+0.00000000000E+000,", "");

            string str18 = ":SENSe1:SWEep:POINts?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str18 + "\n"), str18.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            viError = visa32.viRead(nInstrumentHandle, ret, 200100, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            msg = Encoding.ASCII.GetString(ret, 0, count).Replace("+0.00000000000E+000,", "");


            return viError;
        }

        public static int calc_long(int nInstrumentHandle, out string msg)
        {
            int viError, count;
            int attr;
            byte[] result = new byte[256];
            byte[] ret = new byte[200100];

            string str1 = "DISP:WIND4:TRAC:SEL";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str1 + "\n"), str1.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str2 = ":INIT1:CONT ON";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str2 + "\n"), str2.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str3 = "DISP:WIND4:TRAC:SEL";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str3 + "\n"), str3.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str4 = ":TRIG:SOUR IMM";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str4 + "\n"), str4.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str5 = ":INIT1:CONT OFF";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str5 + "\n"), str5.Length, out count);
            visa32.viRead(nInstrumentHandle, result, 256, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str6 = "DISP:WIND4:TRAC:SEL";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str6 + "\n"), str6.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            visa32.viClear(nInstrumentHandle);

            string str7 = ":CALC1:DATA? FDAT";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str7 + "\n"), str7.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            viError = visa32.viRead(nInstrumentHandle, ret, 200100, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            msg = Encoding.ASCII.GetString(ret, 0, count).Replace("+0.00000000000E+000,", "");


            string str8 = "DISP:WIND4:TRAC2:SEL;*WAI";
            viError = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str8 + "\n"), str8.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);


            string str9 = "CALC:FSIM:BAL:PAR:STAT OFF";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str9 + "\n"), str9.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str10 = "CALC:DATA:SNP:PORT:SAVE '1,2,3,4','Y:MyData_Long.s4p';*WAI";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str10 + "\n"), str10.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str11 = "DISP:WIND4:TRAC:SEL;";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str11 + "\n"), str11.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str12 = ":TRIG:SOUR IMM";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str12 + "\n"), str12.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str13 = ":INIT1:CONT ON";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str13 + "\n"), str13.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str14 = "DISP:WIND4:TRAC:SEL";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str14 + "\n"), str14.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            visa32.viClear(nInstrumentHandle);


            string str19 = "CALC1:X?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str19 + "\n"), str19.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            viError = visa32.viRead(nInstrumentHandle, ret, 200100, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            msg = Encoding.ASCII.GetString(ret, 0, count).Replace("+0.00000000000E+000,", "");

            string str20 = "DISP:WIND1:TRAC:SEL;*WAI";
            viError = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str20 + "\n"), str20.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            visa32.viClear(nInstrumentHandle);

            string str15 = ":CALCulate1:DATA? FDATa";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str15 + "\n"), str15.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            viError = visa32.viRead(nInstrumentHandle, ret, 200100, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            msg = Encoding.ASCII.GetString(ret, 0, count).Replace("+0.00000000000E+000,", "");

            string str16 = ":CALCulate1:TRANsform:TIME:STARt?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str16 + "\n"), str16.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            viError = visa32.viRead(nInstrumentHandle, ret, 200100, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            msg = Encoding.ASCII.GetString(ret, 0, count).Replace("+0.00000000000E+000,", "");

            string str17 = ":CALCulate1:TRANsform:TIME:STOp?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str17 + "\n"), str17.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            viError = visa32.viRead(nInstrumentHandle, ret, 200100, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            msg = Encoding.ASCII.GetString(ret, 0, count).Replace("+0.00000000000E+000,", "");

            string str18 = ":SENSe1:SWEep:POINts?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str18 + "\n"), str18.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            viError = visa32.viRead(nInstrumentHandle, ret, 200100, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);
            msg = Encoding.ASCII.GetString(ret, 0, count).Replace("+0.00000000000E+000,", "");


            return viError;
        }

    }//end class

}
