using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

using DirectShowLib.BDA;

namespace DirectShowLib.Test
{
    public class IBDA_IPV4FilterTest
    {
        private IBDA_IPV4Filter m_ef = null;

        public IBDA_IPV4FilterTest()
        {
        }

        public void DoTests()
        {
            Config();

            try
            {
                TestMode();
                TestList();
            }
            finally
            {
                Marshal.ReleaseComObject(m_ef);
            }
        }

        private void TestMode()
        {
            int hr;
            MulticastMode mm;

            hr = m_ef.PutMulticastMode(MulticastMode.FilteredMulticast);
            DsError.ThrowExceptionForHR(hr);

            hr = m_ef.GetMulticastMode(out mm);
            DsError.ThrowExceptionForHR(hr);

            Debug.Assert(mm == MulticastMode.FilteredMulticast);
        }

        private void TestList()
        {
            int hr;
            int i;
            int iSize = 3 * 4;
            IntPtr ip2 = Marshal.AllocCoTaskMem(iSize);
            IntPtr ip = Marshal.AllocCoTaskMem(iSize);
            Marshal.WriteInt32(ip, 1234);
            Marshal.WriteInt32(ip, 4, 2345);
            Marshal.WriteInt32(ip, 8, 3456);

            Marshal.WriteInt32(ip2, 0, -1);
            Marshal.WriteInt32(ip2, 4, -1);
            Marshal.WriteInt32(ip2, 8, -1);

            hr = m_ef.PutMulticastList(iSize, ip);
            DsError.ThrowExceptionForHR(hr);

            // While it doesn't error, it doesn't return
            // the correct list size either
            hr = m_ef.GetMulticastListSize(out i);
            DsError.ThrowExceptionForHR(hr);

            hr = m_ef.GetMulticastList(ref iSize, ip2);
            DsError.ThrowExceptionForHR(hr);

            Debug.Assert(Marshal.ReadInt32(ip2, 4) == 2345, "MulticastList");
            Marshal.FreeCoTaskMem(ip);
            Marshal.FreeCoTaskMem(ip2);
        }

        private void Config()
        {
            m_ef = (IBDA_IPV4Filter) new ATSCNetworkProvider();
        }
    }
}
