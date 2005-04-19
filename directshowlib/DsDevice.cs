// $Id: DsDevice.cs,v 1.3 2005-04-19 14:48:48 kawaic Exp $
// $Author: kawaic $
// $Revision: 1.3 $

#region license
/* ====================================================================
 * The Apache Software License, Version 1.1
 *
 * Copyright (c) 2000 The Apache Software Foundation.  All rights
 * reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 *
 * 1. Redistributions of source code must retain the above copyright
 *    notice, this list of conditions and the following disclaimer.
 *
 * 2. Redistributions in binary form must reproduce the above copyright
 *    notice, this list of conditions and the following disclaimer in
 *    the documentation and/or other materials provided with the
 *    distribution.
 *
 * 3. The end-user documentation included with the redistribution,
 *    if any, must include the following acknowledgment:
 *       "This product includes software developed by the
 *        Apache Software Foundation (http://www.apache.org/)."
 *    Alternately, this acknowledgment may appear in the software itself,
 *    if and wherever such third-party acknowledgments normally appear.
 *
 * 4. The names "Apache" and "Apache Software Foundation" must
 *    not be used to endorse or promote products derived from this
 *    software without prior written permission. For written
 *    permission, please contact apache@apache.org.
 *
 * 5. Products derived from this software may not be called "Apache",
 *    nor may "Apache" appear in their name, without prior written
 *    permission of the Apache Software Foundation.
 *
 * THIS SOFTWARE IS PROVIDED ``AS IS'' AND ANY EXPRESSED OR IMPLIED
 * WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
 * OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED.  IN NO EVENT SHALL THE APACHE SOFTWARE FOUNDATION OR
 * ITS CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
 * LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF
 * USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
 * OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT
 * OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF
 * SUCH DAMAGE.
 * ====================================================================
 *
 * This software consists of voluntary contributions made by many
 * individuals on behalf of the Apache Software Foundation.  For more
 * information on the Apache Software Foundation, please see
 * <http://www.apache.org/>.
 *
 * Portions of this software are based upon public domain software
 * originally written at the National Center for Supercomputing Applications,
 * University of Illinois, Urbana-Champaign.
 */
#endregion


// ---------------------------------------------------------------------------------
// DsDevice
// Enumerate directshow devices and moniker handling
// Original work from DirectShow .NET by netmaster@swissonline.ch
// ---------------------------------------------------------------------------------


using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace DShowNET.Device
{
	[ComVisible(false)]
	public class DsDev
	{
		public static bool GetDevicesOfCat(Guid cat, out ArrayList devs)
		{
			devs = null;
			int hr;
			object comObj = null;
			ICreateDevEnum enumDev = null;
			UCOMIEnumMoniker enumMon = null;
			UCOMIMoniker[] mon = new UCOMIMoniker[1];
			try
			{
				Type srvType = Type.GetTypeFromCLSID(Clsid.SystemDeviceEnum);
				if (srvType == null)
					throw new NotImplementedException("System Device Enumerator");

				comObj = Activator.CreateInstance(srvType);
				enumDev = (ICreateDevEnum) comObj;
				hr = enumDev.CreateClassEnumerator(ref cat, out enumMon, 0);
				if (hr != 0)
					throw new NotSupportedException("No devices of the category");

				int f, count = 0;
				do
				{
					hr = enumMon.Next(1, mon, out f);
					if ((hr != 0) || (mon[0] == null))
						break;
					DsDevice dev = new DsDevice();
					dev.Name = GetFriendlyName(mon[0]);
					if (devs == null)
						devs = new ArrayList();
					dev.Mon = mon[0];
					mon[0] = null;
					devs.Add(dev);
					dev = null;
					count++;
				} while (true);

				return count > 0;
			}
			catch (Exception)
			{
				if (devs != null)
				{
					foreach (DsDevice d in devs)
						d.Dispose();
					devs = null;
				}
				return false;
			}
			finally
			{
				enumDev = null;
				if (mon[0] != null)
					Marshal.ReleaseComObject(mon[0]);
				mon[0] = null;
				if (enumMon != null)
					Marshal.ReleaseComObject(enumMon);
				enumMon = null;
				if (comObj != null)
					Marshal.ReleaseComObject(comObj);
				comObj = null;
			}

		}

		private static string GetFriendlyName(UCOMIMoniker mon)
		{
			object bagObj = null;
			IPropertyBag bag = null;
			try
			{
				Guid bagId = typeof (IPropertyBag).GUID;
				mon.BindToStorage(null, null, ref bagId, out bagObj);
				bag = (IPropertyBag) bagObj;
				object val = "";
				int hr = bag.Read("FriendlyName", ref val, IntPtr.Zero);
				if (hr != 0)
					Marshal.ThrowExceptionForHR(hr);
				string ret = val as string;
				if ((ret == null) || (ret.Length < 1))
					throw new NotImplementedException("Device FriendlyName");
				return ret;
			}
			catch (Exception)
			{
				return null;
			}
			finally
			{
				bag = null;
				if (bagObj != null)
					Marshal.ReleaseComObject(bagObj);
				bagObj = null;
			}
		}
	}


	[ComVisible(false)]
	public class DsDevice : IDisposable
	{
		public string Name;
		public UCOMIMoniker Mon;

		public void Dispose()
		{
			if (Mon != null)
				Marshal.ReleaseComObject(Mon);
			Mon = null;
		}
	}


	[ComVisible(true), ComImport,
		Guid("29840822-5B84-11D0-BD3B-00A0C911CE86"),
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface ICreateDevEnum
	{
		[PreserveSig]
		int CreateClassEnumerator(
			[In] ref Guid pType,
			[Out] out UCOMIEnumMoniker ppEnumMoniker,
			[In] int dwFlags);
	}


	[ComVisible(true), ComImport,
		Guid("55272A00-42CB-11CE-8135-00AA004BB851"),
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IPropertyBag
	{
		[PreserveSig]
		int Read(
			[In, MarshalAs(UnmanagedType.LPWStr)] string pszPropName,
			[In, Out, MarshalAs(UnmanagedType.Struct)] ref object pVar,
			IntPtr pErrorLog);

		[PreserveSig]
		int Write(
			[In, MarshalAs(UnmanagedType.LPWStr)] string pszPropName,
			[In, MarshalAs(UnmanagedType.Struct)] ref object pVar);
	}


}
