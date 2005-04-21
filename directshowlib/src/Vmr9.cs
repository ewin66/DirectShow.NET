#region license
/* ====================================================================
 * The Apache Software License, Version 1.1
 *
 * Copyright (c) 2005 The Apache Software Foundation.  All rights
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

#define ALLOW_UNTESTED_STRUCTS
#define ALLOW_UNTESTED_INTERFACES

using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace DirectShowLib.VMR
{
    #region Declarations

#if ALLOW_UNTESTED_STRUCTS
 
    /// <summary>
    /// From VMR9PresentationFlags
    /// </summary>
    [ComVisible(false), Flags]
    public enum VMR9PresentationFlags
    {
        SyncPoint         = 0x00000001,
        Preroll           = 0x00000002,
        Discontinuity     = 0x00000004,
        TimeValid         = 0x00000008,
        SrcDstRectsValid  = 0x00000010
    }
  
    /// <summary>
    /// From VMR9SurfaceAllocationFlags
    /// </summary>
    [ComVisible(false), Flags]
    public enum VMR9SurfaceAllocationFlags
    {
        _3DRenderTarget       = 0x0001,
        DXVATarget            = 0x0002,
        TextureSurface        = 0x0004,
        OffscreenSurface      = 0x0008,
        UsageReserved         = 0x00F0,
        UsageMask             = 0x00FF
    }

    /// <summary>
    /// From VMR9AspectRatioMode
    /// </summary>
    [ComVisible(false)]
    public enum VMR9AspectRatioMode
    {
        None,
        LetterBox,
    }

    /// <summary>
    /// From VMR9MixerPrefs
    /// </summary>
    [ComVisible(false), Flags]
    public enum VMR9MixerPrefs
    {
        NoDecimation             = 0x00000001, // No decimation - full size
        DecimateOutput           = 0x00000002, // decimate output by 2 in x & y
        ARAdjustXorY             = 0x00000004, // adjust the aspect ratio in x or y
        NonSquareMixing          = 0x00000008, // assume AP can handle non-square mixing, avoids intermediate scales
        DecimateMask             = 0x0000000F,

        BiLinearFiltering        = 0x00000010, // use bi-linear filtering
        PointFiltering           = 0x00000020, // use point filtering
        AnisotropicFiltering     = 0x00000040, //
        PyramidalQuadFiltering   = 0x00000080, // 4-sample tent
        GaussianQuadFiltering    = 0x00000100, // 4-sample gaussian
        FilteringReserved        = 0x00000E00, // bits reserved for future use.
        FilteringMask            = 0x00000FF0, // OR of all above flags

        RenderTargetRGB          = 0x00001000,
        RenderTargetYUV          = 0x00002000, // Uses DXVA to perform mixing
        RenderTargetReserved     = 0x000FC000, // bits reserved for future use.
        RenderTargetMask         = 0x000FF000, // OR of all above flags

        DynamicSwitchToBOB       = 0x00100000,
        DynamicDecimateBy2       = 0x00200000,

        DynamicReserved          = 0x00C00000,
        DynamicMask              = 0x00F00000
    }

    /// <summary>
    /// From VMR9ProcAmpControlFlags
    /// </summary>
    [ComVisible(false), Flags]
    public enum VMR9ProcAmpControlFlags
    {
        Brightness  = 0x00000001,
        Contrast    = 0x00000002,
        Hue         = 0x00000004,
        Saturation  = 0x00000008,
        Mask        = 0x0000000F
    }

    /// <summary>
    /// From VMR9AlphaBitmapFlags
    /// </summary>
    [ComVisible(false), Flags]
    public enum VMR9AlphaBitmapFlags
    {
        Disable     = 0x00000001,
        hDC         = 0x00000002,
        EntireDDS   = 0x00000004,
        SrcColorKey = 0x00000008,
        SrcRect     = 0x00000010,
        FilterMode  = 0x00000020
    }

    /// <summary>
    /// From VMR9RenderPrefs
    /// </summary>
    [ComVisible(false), Flags]
    public enum VMR9RenderPrefs
    {
        DoNotRenderBorder = 0x00000001, // app paints color keys
        Mask              = 0x00000001, // OR of all above flags
    }

    /// <summary>
    /// From VMR9Mode
    /// </summary>
    [ComVisible(false), Flags]
    public enum VMR9Mode
    {
        Windowed   = 0x00000001,
        Windowless = 0x00000002,
        Renderless = 0x00000004,
        Mask       = 0x00000007
    }

    /// <summary>
    /// From VMR9DeinterlacePrefs
    /// </summary>
    [ComVisible(false), Flags]
    public enum VMR9DeinterlacePrefs
    {
        NextBest = 0x01,
        BOB = 0x02,
        Weave = 0x04,
        Mask = 0x07
    }

    /// <summary>
    /// From VMR9DeinterlaceTech
    /// </summary>
    [ComVisible(false), Flags]
    public enum VMR9DeinterlaceTech
    {
        Unknown              = 0x0000,
        BOBLineReplicate     = 0x0001,
        BOBVerticalStretch   = 0x0002,
        MedianFiltering      = 0x0004,
        EdgeFiltering        = 0x0010,
        FieldAdaptive        = 0x0020,
        PixelAdaptive        = 0x0040,
        MotionVectorSteered  = 0x0080
    }

    /// <summary>
    /// From VMR9_SampleFormat
    /// </summary>
    [ComVisible(false)]
    public enum VMR9_SampleFormat
    {
        Reserved                  = 1,
        ProgressiveFrame          = 2,
        FieldInterleavedEvenFirst = 3,
        FieldInterleavedOddFirst  = 4,
        FieldSingleEven           = 5,
        FieldSingleOdd            = 6
    }


    /// <summary>
    /// From VMR9PresentationInfo
    /// </summary>
    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    public struct VMR9PresentationInfo
    {
        public VMR9PresentationFlags dwFlags;
        [MarshalAs(UnmanagedType.Interface)] 
        public object                lpSurf;     //IDirect3DSurface9
        public long                  rtStart;
        public long                  rtEnd;
        public Size                  szAspectRatio;
        public Rectangle             rcSrc;
        public Rectangle             rcDst;
        public int                   dwReserved1;
        public int                   dwReserved2;
    }


    /// <summary>
    /// From VMR9AllocationInfo
    /// </summary>
    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    public struct VMR9AllocationInfo
    {
        public VMR9SurfaceAllocationFlags  dwFlags;
        public int                         dwWidth;
        public int                         dwHeight;
        public int                         Format;   // D3DFORMAT
        public int                         Pool;     // D3DPOOL
        public int                         MinBuffers;
        public Size                        szAspectRatio;
        public Size                        szNativeSize;
    }
    

    /// <summary>
    /// From VMR9NormalizedRect
    /// </summary>
 
    // TODO : Try to substitate with System.Drawing.RectangleF
    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    public struct VMR9NormalizedRect
    {
        public float left;
        public float top;
        public float right;
        public float bottom;    
    }

    /// <summary>
    /// From VMR9ProcAmpControl
    /// </summary>
    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    public struct VMR9ProcAmpControl
    {
        public int dwSize; // should be 24
        public VMR9ProcAmpControlFlags dwFlags;
        public float Brightness;
        public float Contrast;
        public float Hue;
        public float Saturation;
    }

    /// <summary>
    /// From VMR9ProcAmpControlRange
    /// </summary>
    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    public struct VMR9ProcAmpControlRange
    {
        public int dwSize; // should be 24
        public VMR9ProcAmpControlFlags dwFlags;
        public float MinValue;
        public float MaxValue;
        public float DefaultValue;
        public float StepSize;
    }

    /// <summary>
    /// From VMR9AlphaBitmap
    /// </summary>
    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    public struct VMR9AlphaBitmap
    {
        public VMR9AlphaBitmapFlags  dwFlags;
        public IntPtr                hdc; // HDC
        [MarshalAs(UnmanagedType.IUnknown)]
        public object                pDDS; // IDirect3DSurface9
        public Rectangle             rSrc;
        public VMR9NormalizedRect    rDest;
        public float                 fAlpha;
        public int                   clrSrcKey;
        public int                   dwFilterMode;
    }

    /// <summary>
    /// From VMR9MonitorInfo
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode), ComVisible(false)]
    public struct VMR9MonitorInfo
    {
        public int       uDevID;
        public Rectangle rcMonitor;
        public int       hMon;
        public int       dwFlags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst=32)]
        public string    szDevice;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst=512)]
        public string    szDescription;
        public int       dwVendorId;
        public int       dwDeviceId;
        public int       dwSubSysId;
        public int       dwRevision;
    }

    /// <summary>
    /// From VMR9Frequency
    /// </summary>
    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    public struct VMR9Frequency
    {
        public int dwNumerator;
        public int dwDenominator;
    }

    /// <summary>
    /// From VMR9VideoDesc
    /// </summary>
    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    public struct VMR9VideoDesc
    {
        public int               dwSize;
        public int               dwSampleWidth;
        public int               dwSampleHeight;
        public VMR9_SampleFormat SampleFormat;
        public int               dwFourCC;
        public VMR9Frequency     InputSampleFreq;
        public VMR9Frequency     OutputFrameFreq;
    }

    /// <summary>
    /// From VMR9DeinterlaceCaps
    /// </summary>
    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    public struct VMR9DeinterlaceCaps
    {
        public int                 dwSize;
        public int                 dwNumPreviousOutputFrames;
        public int                 dwNumForwardRefSamples;
        public int                 dwNumBackwardRefSamples;
        public VMR9DeinterlaceTech DeinterlaceTechnology;
    }

    /// <summary>
    /// From VMR9VideoStreamInfo
    /// </summary>
    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    public struct VMR9VideoStreamInfo
    {
        [MarshalAs(UnmanagedType.IUnknown)] 
        public object              pddsVideoSurface; // IDirect3DSurface9
        public int                 dwWidth;
        public int                 dwHeight;
        public int                 dwStrmID;
        public float               fAlpha;
        public VMR9NormalizedRect  rNormal;
        public long                rtStart;
        public long                rtEnd;
        public VMR9_SampleFormat   SampleFormat;
    }
#endif

    #endregion

    #region Interfaces

#if ALLOW_UNTESTED_INTERFACES

    [ComVisible(true), ComImport,
    Guid("69188c61-12a3-40f0-8ffc-342e7b433fd7"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRImagePresenter9
    {
        [PreserveSig]
        int StartPresenting([In] ref int dwUserID);

        [PreserveSig]
        int StopPresenting([In] ref int dwUserID);

        [PreserveSig]
        int PresentImage([In] ref int dwUserID, [In] ref VMR9PresentationInfo lpPresInfo);

    }

    [ComVisible(true), ComImport,
    Guid("8d5148ea-3f5d-46cf-9df1-d1b896eedb1f"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRSurfaceAllocator9
    {
        [PreserveSig]
        int InitializeDevice(
            [In] ref int dwUserID, 
            [In] ref VMR9AllocationInfo lpAllocInfo,
            [In, Out] ref int lpNumBuffers
            );

        [PreserveSig]
        int TerminateDevice([In] ref int dwID);

        [PreserveSig]
        int GetSurface(
            [In] ref int dwUserID,
            [In] int SurfaceIndex,
            [In] int SurfaceFlags,
            [Out, MarshalAs(UnmanagedType.IUnknown)] object lplpSurface
            );

        [PreserveSig]
        int AdviseNotify([In] IVMRSurfaceAllocatorNotify9 lpIVMRSurfAllocNotify);
    }

    [ComVisible(true), ComImport,
    Guid("dca3f5df-bb3a-4d03-bd81-84614bfbfa0c"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRSurfaceAllocatorNotify9
    {
        [PreserveSig]
        int AdviseSurfaceAllocator(
            [In] ref int dwUserID,
            [In] IVMRSurfaceAllocator9 lpIVRMSurfaceAllocator
            );

        [PreserveSig]
        int SetD3DDevice(
            [In, MarshalAs(UnmanagedType.IUnknown)] object lpD3DDevice,
            [In] int hMonitor
            );

        [PreserveSig]
        int ChangeD3DDevice(
            [In, MarshalAs(UnmanagedType.IUnknown)] object lpD3DDevice,
            [In] int hMonitor
            );

        [PreserveSig]
        int AllocateSurfaceHelper(
            [In] VMR9AllocationInfo lpAllocInfo,
            [In, Out] ref int lpNumBuffers,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object lplpSurface
            );

        [PreserveSig]
        int NotifyEvent(
            [In] int EventCode,
            [In] ref int Param1,
            [In] ref int Param2
            );
    }
    
    [ComVisible(true), ComImport,
    Guid("8f537d09-f85e-4414-b23b-502e54c79927"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRWindowlessControl9
    {
        int GetNativeVideoSize(
            [Out] out int	lpWidth,
            [Out] out int	lpHeight,
            [Out] out int	lpARWidth,
            [Out] out int	lpARHeight
            );

        int GetMinIdealVideoSize(
            [Out] out int	lpWidth,
            [Out] out int	lpHeight
            );

        int GetMaxIdealVideoSize(
            [Out] out int lpWidth,
            [Out] out int lpHeight
            );

        int SetVideoPosition(
            [In, MarshalAs(UnmanagedType.Struct)] ref Rectangle lpSRCRect,
            [In, MarshalAs(UnmanagedType.Struct)] ref Rectangle lpDSTRect
            );

        int GetVideoPosition(
            [Out, MarshalAs(UnmanagedType.Struct)] out Rectangle lpSRCRect,
            [Out, MarshalAs(UnmanagedType.Struct)] out Rectangle lpDSTRect
            );

        int GetAspectRatioMode( [Out] out VMR9AspectRatioMode lpAspectRatioMode );

        int SetAspectRatioMode( [In] VMR9AspectRatioMode AspectRatioMode );

        int SetVideoClippingWindow( [In] IntPtr	hwnd ); // HWND

        int RepaintVideo(
            [In] IntPtr hwnd, // HWND
            [In] IntPtr hdc // HDC
            );

        int DisplayModeChanged();

        int GetCurrentImage([Out] out IntPtr lpDib); // BYTE**

        int SetBorderColor([In] int Clr);

        int GetBorderColor([Out] out int lpClr);
    }

    [ComVisible(true), ComImport,
    Guid("1a777eaa-47c8-4930-b2c9-8fee1c1b0f3b"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRMixerControl9
    {
        [PreserveSig]
        int SetAlpha([In] int dwStreamID, [In] float Alpha);

        [PreserveSig]
        int GetAlpha([In] int dwStreamID, [Out] out float Alpha);

        [PreserveSig]
        int SetZOrder([In] int dwStreamID, [In] int dwZ);
    
        [PreserveSig]
        int GetZOrder([In] int dwStreamID, [Out] int dwZ);

        [PreserveSig]
        int SetOutputRect([In] int dwStreamID, [In] ref Rectangle pRect);

        [PreserveSig]
        int GetOutputRect([In] int dwStreamID, [Out] out Rectangle pRect);

        [PreserveSig]
        int SetBackgroundClr([In] int ClrBkg);

        [PreserveSig]
        int GetBackgroundClr([Out] out int ClrBkg);

        [PreserveSig]
        int SetMixingPrefs([In] VMR9MixerPrefs dwMixerPrefs);

        [PreserveSig]
        int GetMixingPrefs([Out] out VMR9MixerPrefs dwMixerPrefs);

        [PreserveSig]
        int SetProcAmpControl([In] int dwStreamID, [In] ref VMR9ProcAmpControl lpClrControl);

        [PreserveSig]
        int GetProcAmpControl([In] int dwStreamID, [In, Out] ref VMR9ProcAmpControl lpClrControl);

        [PreserveSig]
        int GetProcAmpControlRange([In] int dwStreamID, [In, Out] ref VMR9ProcAmpControlRange lpClrControl);
    }

    [ComVisible(true), ComImport,
    Guid("ced175e5-1935-4820-81bd-ff6ad00c9108"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRMixerBitmap9
    {
        [PreserveSig]
        int SetAlphaBitmap([In] ref VMR9AlphaBitmap pBmpParms);

        [PreserveSig]
        int UpdateAlphaBitmapParameters([In] ref VMR9AlphaBitmap pBmpParms);

        [PreserveSig]
        int GetAlphaBitmapParameters([Out] out VMR9AlphaBitmap pBmpParms);
    }
         
    [ComVisible(true), ComImport,
    Guid("dfc581a1-6e1f-4c3a-8d0a-5e9792ea2afc"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRSurface9
    {
        [PreserveSig]
        int IsSurfaceLocked();

        [PreserveSig]
        int LockSurface([Out] out IntPtr lpSurface); // BYTE**

        [PreserveSig]
        int UnlockSurface();

        [PreserveSig]
        int GetSurface([Out, MarshalAs(UnmanagedType.IUnknown)] out object lplpSurface);
    }
  
    [ComVisible(true), ComImport,
    Guid("45c15cab-6e22-420a-8043-ae1f0ac02c7d"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRImagePresenterConfig9
    {
        [PreserveSig]
        int SetRenderingPrefs([In] VMR9RenderPrefs dwRenderFlags);

        [PreserveSig]
        int GetRenderingPrefs([Out] out VMR9RenderPrefs dwRenderFlags);
    }
     
    [ComVisible(true), ComImport,
    Guid("d0cfe38b-93e7-4772-8957-0400c49a4485"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRVideoStreamControl9
    {
        [PreserveSig]
        int SetStreamActiveState([In, MarshalAs(UnmanagedType.Bool)] bool fActive);

        [PreserveSig]
        int GetStreamActiveState([Out, MarshalAs(UnmanagedType.Bool)] out bool fActive);
    }
        
    [ComVisible(true), ComImport,
    Guid("5a804648-4f66-4867-9c43-4f5c822cf1b8"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRFilterConfig9
    {
        [PreserveSig]
        int SetImageCompositor([In] IVMRImageCompositor9 lpVMRImgCompositor);

        [PreserveSig]
        int SetNumberOfStreams([In] int dwMaxStreams);

        [PreserveSig]
        int GetNumberOfStreams([Out] out int pdwMaxStreams);

        [PreserveSig]
        int SetRenderingPrefs([In] int dwRenderFlags);

        [PreserveSig]
        int GetRenderingPrefs([Out] out int pdwRenderFlags);

        [PreserveSig]
        int SetRenderingMode([In] VMR9Mode Mode);

        [PreserveSig]
        int GetRenderingMode([Out] out VMR9Mode Mode);
    }

    [ComVisible(true), ComImport,
    Guid("00d96c29-bbde-4efc-9901-bb5036392146"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRAspectRatioControl9
    {
        [PreserveSig]
        int GetAspectRatioMode([Out] out VMR9AspectRatioMode lpdwARMode);

        [PreserveSig]
        int SetAspectRatioMode([In] VMR9AspectRatioMode lpdwARMode);
    }

    [ComVisible(true), ComImport,
    Guid("46c2e457-8ba0-4eef-b80b-0680f0978749"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRMonitorConfig9
    {
        [PreserveSig]
        int SetMonitor([In] int uDev);

        [PreserveSig]
        int GetMonitor([Out] out int uDev);

        [PreserveSig]
        int SetDefaultMonitor([In] int uDev);

        [PreserveSig]
        int GetDefaultMonitor([Out] out int uDev);

        [PreserveSig]
        int GetAvailableMonitors(
            [In, Out] ref VMR9MonitorInfo[] pInfo,
            [In] int dwMaxInfoArraySize,
            [Out] out int pdwNumDevices
            );
    }
     
    [ComVisible(true), ComImport,
    Guid("a215fb8d-13c2-4f7f-993c-003d6271a459"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRDeinterlaceControl9
    {
        [PreserveSig]
        int GetNumberOfDeinterlaceModes(
            [In] ref VMR9VideoDesc lpVideoDescription,
            [In, Out] ref int lpdwNumDeinterlaceModes,
            [Out] IntPtr lpDeinterlaceModes // LPGUID
            );

        [PreserveSig]
        int GetDeinterlaceModeCaps(
            [In] ref Guid lpDeinterlaceMode,
            [In] ref VMR9VideoDesc lpVideoDescription,
            [Out] out VMR9DeinterlaceCaps lpDeinterlaceCaps
            );

        [PreserveSig]
        int GetDeinterlaceMode(
            [In] int dwStreamID,
            [Out] out Guid lpDeinterlaceMode
            );

        [PreserveSig]
        int SetDeinterlaceMode(
            [In] int dwStreamID,
            [In] ref Guid lpDeinterlaceMode
            );

        [PreserveSig]
        int GetDeinterlacePrefs([Out] out VMR9DeinterlacePrefs lpdwDeinterlacePrefs);

        [PreserveSig]
        int SetDeinterlacePrefs([In] VMR9DeinterlacePrefs lpdwDeinterlacePrefs);

        [PreserveSig]
        int GetActualDeinterlaceMode(
            [In] int dwStreamID,
            [Out] out Guid lpDeinterlaceMode
            );
    }
    
    [ComVisible(true), ComImport,
    Guid("4a5c89eb-df51-4654-ac2a-e48e02bbabf6"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVMRImageCompositor9
    {
        [PreserveSig]
        int InitCompositionDevice([In, MarshalAs(UnmanagedType.IUnknown)] object pD3DDevice);
      
        [PreserveSig]
        int TermCompositionDevice([In, MarshalAs(UnmanagedType.IUnknown)] object pD3DDevice);
      
        [PreserveSig]
        int SetStreamMediaType(
            [In] int dwStrmID,
            [In] AMMediaType pmt,
            [In, MarshalAs(UnmanagedType.Bool)] bool fTexture
            );

        [PreserveSig]
        int CompositeImage(
            [In, MarshalAs(UnmanagedType.IUnknown)] object pD3DDevice,
            [In, MarshalAs(UnmanagedType.IUnknown)] object pddsRenderTarget, // IDirect3DSurface9
            [In] ref AMMediaType pmtRenderTarget,
            [In] long rtStart,
            [In] long rtEnd,
            [In] int dwClrBkGnd,
            [In] ref VMR9VideoStreamInfo pVideoStreamInfo,
            [In] int cStreams
            );
    }
#endif
    #endregion
}
